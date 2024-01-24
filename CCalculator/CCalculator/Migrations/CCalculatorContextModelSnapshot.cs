﻿// <auto-generated />
using CCalculator.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CCalculator.Migrations
{
    [DbContext(typeof(CCalculatorContext))]
    partial class CCalculatorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CCalculator.Models.DataInner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("LoanRate")
                        .HasColumnType("decimal(18, 9)");

                    b.Property<decimal>("LoanSum")
                        .HasColumnType("decimal(18,9)");

                    b.Property<int>("LoanTerm")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DataInner");
                });
#pragma warning restore 612, 618
        }
    }
}
