﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoreAPI.Entities;

#nullable disable

namespace StoreAPI.Data.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    [Migration("20230209034810_RemovedIdFromBasketAndBasketItem")]
    partial class RemovedIdFromBasketAndBasketItem
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StoreAPI.Entities.Basket", b =>
                {
                    b.Property<Guid>("BasketKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BasketKey");

                    b.ToTable("Basket", (string)null);
                });

            modelBuilder.Entity("StoreAPI.Entities.BasketItem", b =>
                {
                    b.Property<Guid>("BasketItemKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BasketKey")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductKey")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("BasketItemKey");

                    b.HasIndex("BasketKey");

                    b.HasIndex("ProductKey");

                    b.ToTable("BasketItem", (string)null);
                });

            modelBuilder.Entity("StoreAPI.Entities.Product", b =>
                {
                    b.Property<Guid>("ProductKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("QuantityInStock")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductKey");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("StoreAPI.Entities.BasketItem", b =>
                {
                    b.HasOne("StoreAPI.Entities.Basket", "Basket")
                        .WithMany("BasketItems")
                        .HasForeignKey("BasketKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoreAPI.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Basket");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("StoreAPI.Entities.Basket", b =>
                {
                    b.Navigation("BasketItems");
                });
#pragma warning restore 612, 618
        }
    }
}
