﻿using Microsoft.EntityFrameworkCore;
using VolvoCash.CrossCutting.NetFramework.Identity;
using VolvoCash.Data.Seedwork.UnitOfWork;
using VolvoCash.Domain.MainContext.Aggregates.BankAccountAgg;
using VolvoCash.Domain.MainContext.Aggregates.BankAgg;
using VolvoCash.Domain.MainContext.Aggregates.BatchAgg;
using VolvoCash.Domain.MainContext.Aggregates.BusinessAreaAgg;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.ClientAgg;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;
using VolvoCash.Domain.MainContext.Aggregates.CurrencyAgg;
using VolvoCash.Domain.MainContext.Aggregates.DealerAgg;
using VolvoCash.Domain.MainContext.Aggregates.DocumentTypeAgg;
using VolvoCash.Domain.MainContext.Aggregates.LiquidationAgg;
using VolvoCash.Domain.MainContext.Aggregates.MappingAgg;
using VolvoCash.Domain.MainContext.Aggregates.MenuAgg;
using VolvoCash.Domain.MainContext.Aggregates.RechargeTypeAgg;
using VolvoCash.Domain.MainContext.Aggregates.RefundAgg;
using VolvoCash.Domain.MainContext.Aggregates.RoleAgg;
using VolvoCash.Domain.MainContext.Aggregates.SectorAgg;
using VolvoCash.Domain.MainContext.Aggregates.SMSCodeAgg;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;

namespace VolvoCash.Data.MainContext
{
    public class MainDbContext : BaseContext
    {
        #region Members
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<BatchMovement> BatchMovements { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardBatch> CardBatches { get; set; }
        public DbSet<CardType> CardTypes { get; set; }
        public DbSet<Cashier> Cashiers { get; set; }
        public DbSet<Charge> Charges { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<Movement> Movements { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SMSCode> SMSCodes { get; set; }
        public DbSet<BatchError> BatchErrors { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<ResetPasswordToken> ResetPasswordTokens { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Liquidation> Liquidations { get; set; }
        public DbSet<Refund> Refunds { get; set; }
        public DbSet<RechargeType> RechargeTypes { get; set; }
        public DbSet<BusinessArea> BusinessAreas { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<BankAccountType> BankAccountTypes { get; set; }
        public DbSet<BankDocumentType> BankDocumentTypes { get; set; }
        public DbSet<BankBankAccountType> BankBankAccountTypes { get; set; }
        public DbSet<BankCurrency> BankCurrencies { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<RoleMenu> RoleMenus { get; set; }
        public DbSet<RoleAdmin> RoleAdmins { get; set; }
        public DbSet<Mapping> Mappings { get; set; }
        public DbSet<MappingHeader> MappingHeaders { get; set; }
        public DbSet<MappingDetail> MappingDetails { get; set; }
        #endregion

        #region Constructor
        public MainDbContext(IApplicationUser _applicationUser) : base(_applicationUser)
        {
        }

        public MainDbContext(DbContextOptions<MainDbContext> options,
                             IApplicationUser _applicationUser) : base(options, _applicationUser)
        {
        }
        #endregion

        #region DBContext Override Methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>()
                .HasMany(c => c.CardBatches)
                .WithOne(cb => cb.Card)
                .HasForeignKey(cb => cb.CardId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Batch>()
                .HasMany(b => b.CardBatches)
                .WithOne(cb => cb.Batch)
                .HasForeignKey(cb => cb.BatchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Card>()
                .HasMany(c => c.OriginTransfers)
                .WithOne(t => t.OriginCard)
                .HasForeignKey(o => o.OriginCardId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Card>()
                .HasMany(c => c.DestinyTransfers)
                .WithOne(t => t.DestinyCard)
                .HasForeignKey(d => d.DestinyCardId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Batch>()
                .HasMany(b => b.BatchMovements)
                .WithOne(bm => bm.Batch)
                .HasForeignKey(bm => bm.BatchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Movement>()
                .HasMany(m => m.BatchMovements)
                .WithOne(bm => bm.Movement)
                .HasForeignKey(bm => bm.MovementId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Card>()
                .HasMany(c => c.Batches)
                .WithOne(b => b.Card)
                .HasForeignKey(b => b.CardId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contact>()
                .HasMany(c => c.Batches)
                .WithOne(b => b.Contact)
                .HasForeignKey(b => b.ContactId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cashier>()
                .HasMany(ca => ca.Charges)
                .WithOne(ch => ch.Cashier)
                .HasForeignKey(ch => ch.CashierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Liquidation>()
                .HasMany(l => l.Charges)
                .WithOne(c => c.Liquidation)
                .HasForeignKey(c => c.LiquidationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bank>()
                .HasMany(b => b.BankDocumentTypes)
                .WithOne(bdt => bdt.Bank)
                .HasForeignKey(bdt => bdt.BankId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DocumentType>()
                .HasMany(d => d.BankDocumentTypes)
                .WithOne(bdt => bdt.DocumentType)
                .HasForeignKey(bdt => bdt.DocumentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bank>()
                .HasMany(b => b.BankBankAccountTypes)
                .WithOne(bbat => bbat.Bank)
                .HasForeignKey(bbat => bbat.BankId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BankAccountType>()
                .HasMany(bat => bat.BankBankAccountTypes)
                .WithOne(bbat => bbat.BankAccountType)
                .HasForeignKey(bbat => bbat.BankAccountTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bank>()
                .HasMany(b => b.BankCurrencies)
                .WithOne(bc => bc.Bank)
                .HasForeignKey(bc => bc.BankId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Currency>()
                .HasMany(c => c.BankCurrencies)
                .WithOne(bc => bc.Currency)
                .HasForeignKey(bc => bc.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>()
                .HasMany(r => r.RoleMenus)
                .WithOne(rm => rm.Role)
                .HasForeignKey(rm => rm.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Menu>()
                .HasMany(m => m.RoleMenus)
                .WithOne(rm => rm.Menu)
                .HasForeignKey(rm => rm.MenuId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>()
                .HasMany(r => r.RoleAdmins)
                .WithOne(ra => ra.Role)
                .HasForeignKey(ra => ra.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Admin>()
                .HasMany(m => m.RoleAdmins)
                .WithOne(ra => ra.Admin)
                .HasForeignKey(ra => ra.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Movement>()
                .OwnsOne(m => m.Amount);

            modelBuilder.Entity<Card>()
                .OwnsOne(c => c.Balance);

            modelBuilder.Entity<Batch>()
                .OwnsOne(b => b.Amount);

            modelBuilder.Entity<CardBatch>()
                .OwnsOne(cb => cb.Balance);

            modelBuilder.Entity<BatchMovement>()
                .OwnsOne(bm => bm.Amount);

            modelBuilder.Entity<Transfer>()
                .OwnsOne(t => t.Amount);

            modelBuilder.Entity<Charge>()
                .OwnsOne(t => t.Amount);

            modelBuilder.Entity<Batch>()
                .OwnsOne(b => b.Balance);

            modelBuilder.Entity<Liquidation>()
                .OwnsOne(m => m.Amount);

            modelBuilder.Entity<Refund>()
                .OwnsOne(m => m.Amount);

            modelBuilder.Entity<Mapping>()
             .HasMany(m => m.MappingHeaders)
             .WithOne(ra => ra.Mapping)
             .HasForeignKey(ra => ra.MappingId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MappingHeader>()
            .HasMany(m => m.MappingDetails)
            .WithOne(ra => ra.MappingHeader)
            .HasForeignKey(ra => ra.MappingHeaderId)
            .OnDelete(DeleteBehavior.Restrict);

            //var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            //                            v => v.ToUniversalTime(),
            //                            v => DateTime.SpecifyKind(v, DateTimeKind.Local));

            //var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
            //    v => v.HasValue ? v.Value.ToUniversalTime() : v,
            //    v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Local) : v);

            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    if (entityType.IsKeyless)
            //    {
            //        continue;
            //    }

            //    foreach (var property in entityType.GetProperties())
            //    {
            //        if (property.ClrType == typeof(DateTime))
            //        {
            //            property.SetValueConverter(dateTimeConverter);
            //        }
            //        else if (property.ClrType == typeof(DateTime?))
            //        {
            //            property.SetValueConverter(nullableDateTimeConverter);
            //        }
            //    }
            //}
        }
        #endregion
    }
}
