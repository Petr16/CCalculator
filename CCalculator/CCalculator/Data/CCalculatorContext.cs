using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CCalculator.Models;

namespace CCalculator.Data
{
    public class CCalculatorContext : DbContext
    {
        public CCalculatorContext (DbContextOptions<CCalculatorContext> options)
            : base(options)
        {
        }

        public DbSet<DataInner> DataInners { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataInner>()
                .Property(e => e.LoanSum)
                .HasColumnType("decimal(18,9)");
            modelBuilder.Entity<DataInner>()
                .Property(e => e.LoanRate)
                .HasColumnType("decimal(18,9)");
            modelBuilder.Entity<Payment>()
                .Property(e => e.PaymentByBody)
                .HasColumnType("decimal(18,9)");
            modelBuilder.Entity<Payment>()
                .Property(e => e.PamentByPercent)
                .HasColumnType("decimal(18,9)");
            modelBuilder.Entity<Payment>()
                .Property(e => e.BalanceOwed)
                .HasColumnType("decimal(18,9)");
            modelBuilder.Entity<DataInner>()
                .HasOne(a => a.Pay)
                .WithOne(b => b.DataInner)
                .HasForeignKey<Payment>(f => f.DataInnerId);




            //base.OnModelCreating(modelBuilder);
        }
    }
}
