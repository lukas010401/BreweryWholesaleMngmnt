﻿// <auto-generated />
using System;
using BreweryWholesaleMngmnt.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BreweryWholesaleMngmnt.Migrations
{
    [DbContext(typeof(BreweryContext))]
    partial class BreweryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BreweryWholesaleMngmnt.Models.Beer", b =>
                {
                    b.Property<int>("BeerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BeerID"));

                    b.Property<decimal>("AlcoholContent")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<int>("BreweryID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("BeerID");

                    b.HasIndex("BreweryID");

                    b.ToTable("Beers");
                });

            modelBuilder.Entity("BreweryWholesaleMngmnt.Models.Brewery", b =>
                {
                    b.Property<int>("BreweryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BreweryID"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BreweryID");

                    b.ToTable("Breweries");
                });

            modelBuilder.Entity("BreweryWholesaleMngmnt.Models.Client", b =>
                {
                    b.Property<int>("ClientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientID"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClientID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("BreweryWholesaleMngmnt.Models.Quote", b =>
                {
                    b.Property<int>("QuoteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuoteID"));

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequestedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int>("WholesalerID")
                        .HasColumnType("int");

                    b.HasKey("QuoteID");

                    b.HasIndex("ClientID");

                    b.HasIndex("WholesalerID");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("BreweryWholesaleMngmnt.Models.QuoteItem", b =>
                {
                    b.Property<int>("QuoteItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuoteItemID"));

                    b.Property<int>("BeerID")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("QuoteID")
                        .HasColumnType("int");

                    b.HasKey("QuoteItemID");

                    b.HasIndex("BeerID");

                    b.HasIndex("QuoteID");

                    b.ToTable("QuoteItems");
                });

            modelBuilder.Entity("BreweryWholesaleMngmnt.Models.Sale", b =>
                {
                    b.Property<int>("SaleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SaleID"));

                    b.Property<int>("BeerID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("WholesalerID")
                        .HasColumnType("int");

                    b.HasKey("SaleID");

                    b.HasIndex("BeerID");

                    b.HasIndex("WholesalerID");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("BreweryWholesaleMngmnt.Models.Wholesaler", b =>
                {
                    b.Property<int>("WholesalerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WholesalerID"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WholesalerID");

                    b.ToTable("Wholesalers");
                });

            modelBuilder.Entity("BreweryWholesaleMngmnt.Models.WholesalerStock", b =>
                {
                    b.Property<int>("WholesalerID")
                        .HasColumnType("int");

                    b.Property<int>("BeerID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("WholesalerStockID")
                        .HasColumnType("int");

                    b.HasKey("WholesalerID", "BeerID");

                    b.HasIndex("BeerID");

                    b.ToTable("WholesalerStocks");
                });

            modelBuilder.Entity("BreweryWholesaleMngmnt.Models.Beer", b =>
                {
                    b.HasOne("BreweryWholesaleMngmnt.Models.Brewery", "Brewery")
                        .WithMany("Beers")
                        .HasForeignKey("BreweryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brewery");
                });

            modelBuilder.Entity("BreweryWholesaleMngmnt.Models.Quote", b =>
                {
                    b.HasOne("BreweryWholesaleMngmnt.Models.Client", "Client")
                        .WithMany("Quotes")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BreweryWholesaleMngmnt.Models.Wholesaler", "Wholesaler")
                        .WithMany()
                        .HasForeignKey("WholesalerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Wholesaler");
                });

            modelBuilder.Entity("BreweryWholesaleMngmnt.Models.QuoteItem", b =>
                {
                    b.HasOne("BreweryWholesaleMngmnt.Models.Beer", "Beer")
                        .WithMany()
                        .HasForeignKey("BeerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BreweryWholesaleMngmnt.Models.Quote", "Quote")
                        .WithMany("Items")
                        .HasForeignKey("QuoteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beer");

                    b.Navigation("Quote");
                });

            modelBuilder.Entity("BreweryWholesaleMngmnt.Models.Sale", b =>
                {
                    b.HasOne("BreweryWholesaleMngmnt.Models.Beer", "Beer")
                        .WithMany()
                        .HasForeignKey("BeerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BreweryWholesaleMngmnt.Models.Wholesaler", "Wholesaler")
                        .WithMany()
                        .HasForeignKey("WholesalerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beer");

                    b.Navigation("Wholesaler");
                });

            modelBuilder.Entity("BreweryWholesaleMngmnt.Models.WholesalerStock", b =>
                {
                    b.HasOne("BreweryWholesaleMngmnt.Models.Beer", "Beer")
                        .WithMany()
                        .HasForeignKey("BeerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BreweryWholesaleMngmnt.Models.Wholesaler", "Wholesaler")
                        .WithMany("WholesalerStocks")
                        .HasForeignKey("WholesalerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beer");

                    b.Navigation("Wholesaler");
                });

            modelBuilder.Entity("BreweryWholesaleMngmnt.Models.Brewery", b =>
                {
                    b.Navigation("Beers");
                });

            modelBuilder.Entity("BreweryWholesaleMngmnt.Models.Client", b =>
                {
                    b.Navigation("Quotes");
                });

            modelBuilder.Entity("BreweryWholesaleMngmnt.Models.Quote", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("BreweryWholesaleMngmnt.Models.Wholesaler", b =>
                {
                    b.Navigation("WholesalerStocks");
                });
#pragma warning restore 612, 618
        }
    }
}
