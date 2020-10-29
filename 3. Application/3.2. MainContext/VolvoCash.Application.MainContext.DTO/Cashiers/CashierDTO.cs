using VolvoCash.Application.MainContext.DTO.Dealers;

namespace VolvoCash.Application.MainContext.DTO.Cashiers
{
    public class CashierDTO
    {
        #region Properties
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string TPCode { get; set; }

        public int DealerId { get; set; }

        public virtual DealerDTO Dealer { get; set; }

        public int UserId { get; set; }
        #endregion
    }
}
