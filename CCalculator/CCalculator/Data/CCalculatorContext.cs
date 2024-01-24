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

        public DbSet<DataInner> DataInner { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataInner>()
                .Property(e => e.LoanSum)
                .HasColumnType("decimal(18,9)");
            modelBuilder.Entity<DataInner>()
                .Property(e => e.LoanRate)
                .HasColumnType("decimal(18, 9)");
            //base.OnModelCreating(modelBuilder);
        }
    }
}
