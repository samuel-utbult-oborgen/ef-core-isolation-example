﻿// <auto-generated />
using System;
using EfCoreIsolationExample.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EfCoreIsolationExample.DB.Migrations
{
    [DbContext(typeof(ExampleDBContext))]
    partial class ExampleDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EfCoreIsolationExample.DB.Entities.Price", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateOnly>("End")
                        .HasColumnType("date");

                    b.Property<int>("PriceGroupID")
                        .HasColumnType("int");

                    b.Property<DateOnly>("Start")
                        .HasColumnType("date");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.HasIndex("PriceGroupID");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("EfCoreIsolationExample.DB.Entities.PriceGroup", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<Guid>("PriceVersion")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.ToTable("PriceGroups");
                });

            modelBuilder.Entity("EfCoreIsolationExample.DB.Entities.Price", b =>
                {
                    b.HasOne("EfCoreIsolationExample.DB.Entities.PriceGroup", "PriceGroup")
                        .WithMany()
                        .HasForeignKey("PriceGroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PriceGroup");
                });
#pragma warning restore 612, 618
        }
    }
}