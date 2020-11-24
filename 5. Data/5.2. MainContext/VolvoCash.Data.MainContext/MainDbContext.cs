﻿using Microsoft.EntityFrameworkCore;
using VolvoCash.CrossCutting.NetFramework.Identity;
using VolvoCash.Data.Seedwork.UnitOfWork;
using VolvoCash.Domain.MainContext.Aggregates.BankAgg;
using VolvoCash.Domain.MainContext.Aggregates.BatchAgg;
using VolvoCash.Domain.MainContext.Aggregates.BusinessAreaAgg;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.ClientAgg;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;
using VolvoCash.Domain.MainContext.Aggregates.DealerAgg;
using VolvoCash.Domain.MainContext.Aggregates.DocumentTypeAgg;
using VolvoCash.Domain.MainContext.Aggregates.RechargeTypeAgg;
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
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<RechargeType> RechargeTypes { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<BankAccountType> BankAccountTypes { get; set; }
        public DbSet<BusinessArea> BusinessAreas { get; set; }
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
                .HasMany(cashier => cashier.Charges)
                .WithOne(charge => charge.Cashier)
                .HasForeignKey(charge => charge.CashierId)
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
        }
        #endregion
    }
}
