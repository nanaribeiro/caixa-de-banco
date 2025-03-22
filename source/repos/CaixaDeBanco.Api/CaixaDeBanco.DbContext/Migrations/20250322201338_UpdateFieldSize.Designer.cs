﻿// <auto-generated />
using System;
using CaixaDeBanco.Database.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CaixaDeBanco.Database.Migrations
{
    [DbContext(typeof(BancoDbContext))]
    [Migration("20250322201338_UpdateFieldSize")]
    partial class UpdateFieldSize
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CaixaDeBanco.Database.Models.AccountHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountIdId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Action")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("User")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("AccountIdId");

                    b.ToTable("AccountHistory");
                });

            modelBuilder.Entity("CaixaDeBanco.Database.Models.BankingAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccountNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountNumber"));

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("OpenedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("CaixaDeBanco.Database.Models.TransactionHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountIdId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int");

                    b.Property<decimal?>("TransactionValue")
                        .HasColumnType("decimal(5, 2)");

                    b.HasKey("Id");

                    b.HasIndex("AccountIdId");

                    b.ToTable("TransactionHistory");
                });

            modelBuilder.Entity("CaixaDeBanco.Database.Models.AccountHistory", b =>
                {
                    b.HasOne("CaixaDeBanco.Database.Models.BankingAccount", "AccountId")
                        .WithMany("AccountHistories")
                        .HasForeignKey("AccountIdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountId");
                });

            modelBuilder.Entity("CaixaDeBanco.Database.Models.TransactionHistory", b =>
                {
                    b.HasOne("CaixaDeBanco.Database.Models.BankingAccount", "AccountId")
                        .WithMany("TransactionHistories")
                        .HasForeignKey("AccountIdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountId");
                });

            modelBuilder.Entity("CaixaDeBanco.Database.Models.BankingAccount", b =>
                {
                    b.Navigation("AccountHistories");

                    b.Navigation("TransactionHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
