using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.DTO.Admins;
using VolvoCash.Application.MainContext.DTO.Cashiers;
using VolvoCash.Application.MainContext.DTO.Contacts;
using VolvoCash.Application.MainContext.DTO.Users;
using VolvoCash.Application.Seedwork;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.CrossCutting.NetFramework.Utils;
using VolvoCash.CrossCutting.Utils;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;
using VolvoCash.Domain.MainContext.Aggregates.DealerAgg;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;
using VolvoCash.Domain.MainContext.Enums;
using VolvoCash.Domain.MainContext.Services.CardService;

namespace VolvoCash.Application.MainContext.Users.Services
{
    public class UserAppService : IUserAppService
    {
        #region Members
        private readonly IUserRepository _userRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly ICashierRepository _cashierRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IDealerRepository _dealerRepository;
        private readonly ICardRepository _cardRepository;
        private readonly ITransferRepository _transferRepository;
        private readonly IEmailManager _emailManager;
        private readonly IConfiguration _configuration;
        private readonly ICardTransferService _cardTransferService;
        private readonly ILocalization _resources;
        #endregion

        #region Constructor
        public UserAppService(IUserRepository userRepository,
                              IAdminRepository adminRepository,
                              ICashierRepository cashierRepository,
                              IContactRepository contactRepository,
                              IDealerRepository dealerRepository,
                              ICardRepository cardRepository,
                              ITransferRepository transferRepository,
                              ICardTransferService cardTransferService,                            
                              IEmailManager emailManager,
                              IConfiguration configuration                     
                             )
        {
            _userRepository = userRepository;
            _adminRepository = adminRepository;
            _cashierRepository = cashierRepository;
            _contactRepository = contactRepository;
            _dealerRepository = dealerRepository;
            _cardRepository = cardRepository;
            _transferRepository = transferRepository;
            _cardTransferService = cardTransferService;
            _emailManager = emailManager;
            _configuration = configuration;           
            _resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

        #region Private Methods
        private void SendResetPasswordEmailToCashier(Cashier cashier, string generatedPassword)
        {
            var subject = _resources.GetStringResource(LocalizationKeys.Application.messages_ResetCashierPasswordEmailSubject);
            var body = _resources.GetStringResource(LocalizationKeys.Application.messages_ResetCashierPasswordEmailBody);
            body = string.Format(body, generatedPassword);
            _emailManager.SendEmail(cashier.Email, subject, body);
        }

        private void SendResetPasswordEmailToAdmin(Admin admin, string generatedPassword)
        {
            var subject = _resources.GetStringResource(LocalizationKeys.Application.messages_ResetAdminPasswordEmailSubject);
            var body = _resources.GetStringResource(LocalizationKeys.Application.messages_ResetAdminPasswordEmailBody);
            var webAdminUrl = _configuration["Application:BaseWebAdminUrl"];
            body = string.Format(body, webAdminUrl, generatedPassword);
            _emailManager.SendEmail(admin.Email, subject, body);
        }

        private void SendCreateUserEmailToAdmin(AdminDTO adminDTO)
        {
            var subject = _resources.GetStringResource(LocalizationKeys.Application.messages_NewAdminEmailSubject);
            var body = _resources.GetStringResource(LocalizationKeys.Application.messages_NewAdminEmailBody);
            var webAdminUrl = _configuration["Application:BaseWebAdminUrl"];
            body = string.Format(body, webAdminUrl,adminDTO.Password);
            _emailManager.SendEmail(adminDTO.Email, subject, body);
        }

        private async Task<List<Transfer>> GetTransfersAboutAllFounds(Contact contact, int? contactToTransferId)
        {
            var cards = contact.Cards;
            var contactToTransfer = await _contactRepository.GetContactById(contactToTransferId.GetValueOrDefault());
            var transfers = new List<Transfer>();
            foreach (var _card in cards)
            {
                if (_card.HasBalance)
                {
                    var card = await _cardRepository.GetCardByIdWithBatchesAsync(_card.Id);
                    if (contactToTransfer == null)
                    {
                        throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_NullContactToTransferFounds));
                    }
                    var transfer = await _cardTransferService.PerformTransfer(card, contactToTransfer, card.CalculatedBalance);
                    transfers.Add(transfer);
                }
                _card.Status = Status.Inactive;
            }
            return transfers;
        }
        #endregion        

        #region ApiWeb Public Methods       
        public async Task<IList<UserDTO>> GetAllDTOAsync()
        {
            var admins = (await _adminRepository.GetAllAsync()).ProjectedAsCollection<AdminDTO>() ;
            var cashiers = (await _cashierRepository.GetAllAsync()).ProjectedAsCollection<CashierDTO>();
            var contacts = (await _contactRepository.GetAllAsync()).ProjectedAsCollection < ContactListDTO>();
            var users = new List<UserDTO>();
            users.AddRange(admins.Select(a => new UserDTO() { Admin = a, Id = a.UserId, Type = UserType.WebAdmin }));
            users.AddRange(cashiers.Select(c => new UserDTO() { Cashier = c, Id = c.UserId, Type = UserType.Cashier }));
            users.AddRange(contacts.Select(c => new UserDTO() { Contact = c, Id = c.UserId, Type = UserType.Contact }));
            return users;
        }

        public async Task<AdminDTO> AddAdminAsync(AdminDTO adminDTO)
        {
            var existingAdmin = _adminRepository.Filter(a => (a.Email == adminDTO.Email) && a.ArchiveAt == null).FirstOrDefault();
            if (existingAdmin != null)
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_AdminAlreadyExists));
            }
            if (string.IsNullOrEmpty(adminDTO.Password))
            {
                adminDTO.Password = RandomGenerator.RandomDigits(6);
            }
            var dealer = await _dealerRepository.GetAsync(adminDTO.DealerId);
            var admin = new Admin(adminDTO.FirstName, adminDTO.LastName, adminDTO.Password, adminDTO.Phone, adminDTO.Email, dealer);
            _adminRepository.Add(admin);
            await _adminRepository.UnitOfWork.CommitAsync();
            SendCreateUserEmailToAdmin(adminDTO);
            return admin.ProjectedAs<AdminDTO>();
        }

        public async Task<AdminDTO> ModifyAdminAsync(AdminDTO adminDTO)
        {
            var admin = await _adminRepository.GetAsync(adminDTO.Id);
            if (admin == null)
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_AdminNotFound));
            }

            var existingAdmin = _adminRepository.Filter(a => (a.Email == adminDTO.Email) && a.ArchiveAt == null).FirstOrDefault();
            if (existingAdmin != null && existingAdmin.Id != adminDTO.Id)
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_AdminAlreadyExists));
            }
            admin.FirstName = adminDTO.FirstName;
            admin.LastName = adminDTO.LastName;
            admin.Phone = adminDTO.Phone;
            admin.Email = adminDTO.Email;
            admin.DealerId = adminDTO.DealerId;
            _adminRepository.Modify(admin);
            await _adminRepository.UnitOfWork.CommitAsync();
            return admin.ProjectedAs<AdminDTO>();
        }

        public async Task ResetUserPasswordAsync(int id)
        {
            var user = _userRepository.Filter(filter: u => u.Id == id, includeProperties: "Contacts,Admins,Cashiers").FirstOrDefault();
            var password = RandomGenerator.RandomDigits(6);
            switch (user.Type)
            {
                case UserType.Cashier:
                    var cashier = user.Cashier;
                    cashier.SetPasswordHash(password);
                    await _cashierRepository.UnitOfWork.CommitAsync();
                    SendResetPasswordEmailToCashier(cashier, password);
                    break;
                case UserType.Contact:
                    break;
                case UserType.WebAdmin:
                    var admin = user.Admin;
                    admin.SetPasswordHash(password);
                    await _adminRepository.UnitOfWork.CommitAsync();
                    SendResetPasswordEmailToAdmin(admin, password);
                    break;
                default:
                    break;
            }
        }

        public async Task DeleteUserAsync(int id, int? contactToTransferId)
        {
            var user = _userRepository.Filter(filter: u => u.Id == id, includeProperties: "Contacts,Admins,Cashiers").FirstOrDefault();
            switch (user.Type)
            {
                case UserType.Cashier:
                    var cashier = user.Cashier;
                    cashier.Delete();
                    _cashierRepository.Modify(cashier);
                    await _cashierRepository.UnitOfWork.CommitAsync();
                    break;
                case UserType.Contact:
                    var contact = user.Contact;
                    var cards = await _cardRepository.GetCardsByContactId(contact.Id);
                    var transfers = await GetTransfersAboutAllFounds(contact, contactToTransferId);
                    _transferRepository.Add(transfers);
                    contact.Delete();                    
                    await _transferRepository.UnitOfWork.CommitAsync();
                    break;
                case UserType.WebAdmin:
                    var admin = user.Admin;
                    admin.Delete();
                    _adminRepository.Modify(admin);
                    await _adminRepository.UnitOfWork.CommitAsync();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Common Public Methods
        public async Task ChangePassword(int id,string password, string confirmPassword)
        {
            if (password!= confirmPassword)
            {
                throw new InvalidOperationException(_resources.GetStringResource(LocalizationKeys.Application.exception_PasswordAndConfirmNotMatch));
            }
            var user = _userRepository.Filter(filter: u => u.Id == id, includeProperties: "Contacts,Admins,Cashiers").FirstOrDefault();
            switch (user.Type)
            {
                case UserType.Cashier:
                    var cashier = user.Cashier;
                    cashier.SetPasswordHash(password);
                    await _cashierRepository.UnitOfWork.CommitAsync();
                    break;
                case UserType.Contact:
                    break;
                case UserType.WebAdmin:
                    var admin = user.Admin;
                    admin.SetPasswordHash(password);
                    await _adminRepository.UnitOfWork.CommitAsync();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            //dispose all resources
            _userRepository.Dispose();
            _adminRepository.Dispose();
            _cashierRepository.Dispose();
            _contactRepository.Dispose();
            _dealerRepository.Dispose();
            _cardRepository.Dispose();
            _transferRepository.Dispose();
        }
        #endregion
    }
}
