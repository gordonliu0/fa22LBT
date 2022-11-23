﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using fa22LBT.DAL;

#nullable disable

namespace fa22LBT.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221122214740_blah2")]
    partial class blah2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);
            SqlServerModelBuilderExtensions.HasServiceTierSql(modelBuilder, "'Basic'");
            SqlServerModelBuilderExtensions.HasPerformanceLevelSql(modelBuilder, "'Basic'");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("fa22LBT.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("MiddleInitial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("fa22LBT.Models.BankAccount", b =>
                {
                    b.Property<string>("AccountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("AccountBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("AccountName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("AccountNo")
                        .HasColumnType("bigint");

                    b.Property<int>("AccountType")
                        .HasColumnType("int");

                    b.Property<decimal>("Contribution")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.HasKey("AccountID");

                    b.HasIndex("CustomerId");

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("fa22LBT.Models.Dispute", b =>
                {
                    b.Property<int>("DisputeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DisputeID"));

                    b.Property<decimal>("CorrectAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("DisputeDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DisputeStatus")
                        .HasColumnType("int");

                    b.Property<int>("DisputeTransactionTransactionID")
                        .HasColumnType("int");

                    b.HasKey("DisputeID");

                    b.HasIndex("DisputeTransactionTransactionID");

                    b.ToTable("Disputes");
                });

            modelBuilder.Entity("fa22LBT.Models.Stock", b =>
                {
                    b.Property<int>("StockID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StockID"));

                    b.Property<string>("StockName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("StockPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("StockTypeID")
                        .HasColumnType("int");

                    b.Property<string>("TickerSymbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StockID");

                    b.HasIndex("StockTypeID");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("fa22LBT.Models.StockPortfolio", b =>
                {
                    b.Property<string>("AccountID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AccountNo")
                        .HasColumnType("int");

                    b.Property<string>("AppUserForeignKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("CashBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<bool>("IsBalanced")
                        .HasColumnType("bit");

                    b.HasKey("AccountID");

                    b.HasIndex("AppUserForeignKey")
                        .IsUnique()
                        .HasFilter("[AppUserForeignKey] IS NOT NULL");

                    b.ToTable("StockPortfolios");
                });

            modelBuilder.Entity("fa22LBT.Models.StockTransaction", b =>
                {
                    b.Property<int>("StockTransactionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StockTransactionID"));

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PricePerShare")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("QuantityShares")
                        .HasColumnType("int");

                    b.Property<int>("StockID")
                        .HasColumnType("int");

                    b.Property<string>("StockPortfolioAccountID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("StockTransactionID");

                    b.HasIndex("StockID");

                    b.HasIndex("StockPortfolioAccountID");

                    b.ToTable("StockTransactions");
                });

            modelBuilder.Entity("fa22LBT.Models.StockType", b =>
                {
                    b.Property<int>("StockTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StockTypeID"));

                    b.Property<string>("StockTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StockTypeID");

                    b.ToTable("StockTypes");
                });

            modelBuilder.Entity("fa22LBT.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionID"));

                    b.Property<string>("BankAccountAccountID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("FromAccount")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("StockPortfolioAccountID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("ToAccount")
                        .HasColumnType("bigint");

                    b.Property<decimal>("TransactionAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("TransactionApproved")
                        .HasColumnType("bit");

                    b.Property<string>("TransactionComments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransactionNumber")
                        .HasColumnType("int");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("TransactionID");

                    b.HasIndex("BankAccountAccountID");

                    b.HasIndex("StockPortfolioAccountID");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("fa22LBT.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("fa22LBT.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fa22LBT.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("fa22LBT.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("fa22LBT.Models.BankAccount", b =>
                {
                    b.HasOne("fa22LBT.Models.AppUser", "Customer")
                        .WithMany("BankAccounts")
                        .HasForeignKey("CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("fa22LBT.Models.Dispute", b =>
                {
                    b.HasOne("fa22LBT.Models.Transaction", "DisputeTransaction")
                        .WithMany("Dispute")
                        .HasForeignKey("DisputeTransactionTransactionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DisputeTransaction");
                });

            modelBuilder.Entity("fa22LBT.Models.Stock", b =>
                {
                    b.HasOne("fa22LBT.Models.StockType", "StockType")
                        .WithMany("Stocks")
                        .HasForeignKey("StockTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StockType");
                });

            modelBuilder.Entity("fa22LBT.Models.StockPortfolio", b =>
                {
                    b.HasOne("fa22LBT.Models.AppUser", "AppUser")
                        .WithOne("StockPortfolio")
                        .HasForeignKey("fa22LBT.Models.StockPortfolio", "AppUserForeignKey");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("fa22LBT.Models.StockTransaction", b =>
                {
                    b.HasOne("fa22LBT.Models.Stock", "Stock")
                        .WithMany("StockTransactions")
                        .HasForeignKey("StockID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fa22LBT.Models.StockPortfolio", "StockPortfolio")
                        .WithMany("StockTransactions")
                        .HasForeignKey("StockPortfolioAccountID");

                    b.Navigation("Stock");

                    b.Navigation("StockPortfolio");
                });

            modelBuilder.Entity("fa22LBT.Models.Transaction", b =>
                {
                    b.HasOne("fa22LBT.Models.BankAccount", "BankAccount")
                        .WithMany("Transactions")
                        .HasForeignKey("BankAccountAccountID");

                    b.HasOne("fa22LBT.Models.StockPortfolio", null)
                        .WithMany("Transactions")
                        .HasForeignKey("StockPortfolioAccountID");

                    b.HasOne("fa22LBT.Models.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("BankAccount");

                    b.Navigation("User");
                });

            modelBuilder.Entity("fa22LBT.Models.AppUser", b =>
                {
                    b.Navigation("BankAccounts");

                    b.Navigation("StockPortfolio")
                        .IsRequired();
                });

            modelBuilder.Entity("fa22LBT.Models.BankAccount", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("fa22LBT.Models.Stock", b =>
                {
                    b.Navigation("StockTransactions");
                });

            modelBuilder.Entity("fa22LBT.Models.StockPortfolio", b =>
                {
                    b.Navigation("StockTransactions");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("fa22LBT.Models.StockType", b =>
                {
                    b.Navigation("Stocks");
                });

            modelBuilder.Entity("fa22LBT.Models.Transaction", b =>
                {
                    b.Navigation("Dispute");
                });
#pragma warning restore 612, 618
        }
    }
}
