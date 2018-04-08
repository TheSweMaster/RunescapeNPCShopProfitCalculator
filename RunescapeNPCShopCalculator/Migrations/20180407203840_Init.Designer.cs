﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using RunescapeNPCShopCalculator.Data;
using System;

namespace RunescapeNPCShopCalculator.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20180407203840_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RunescapeNPCShopCalculator.Models.ShopDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Location");

                    b.Property<bool>("Members");

                    b.Property<string>("Name");

                    b.Property<string>("Notes");

                    b.Property<string>("Shopkeeper");

                    b.HasKey("Id");

                    b.ToTable("ShopDetails");
                });

            modelBuilder.Entity("RunescapeNPCShopCalculator.Models.ShopItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DefaultStock");

                    b.Property<string>("DisplayPrice");

                    b.Property<string>("Item");

                    b.Property<int>("Price");

                    b.Property<int?>("ShopDetailId");

                    b.HasKey("Id");

                    b.HasIndex("ShopDetailId");

                    b.ToTable("ShopItems");
                });

            modelBuilder.Entity("RunescapeNPCShopCalculator.Models.ShopItem", b =>
                {
                    b.HasOne("RunescapeNPCShopCalculator.Models.ShopDetail")
                        .WithMany("ShopItems")
                        .HasForeignKey("ShopDetailId");
                });
#pragma warning restore 612, 618
        }
    }
}