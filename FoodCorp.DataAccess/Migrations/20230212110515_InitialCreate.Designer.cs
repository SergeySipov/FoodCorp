﻿// <auto-generated />
using System;
using FoodCorp.DataAccess.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FoodCorp.DataAccess.Migrations
{
    [DbContext(typeof(FoodCorpDbContext))]
    [Migration("20230212110515_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FoodCorp.DataAccess.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(48)
                        .HasColumnType("nvarchar(48)");

                    b.HasKey("Id");

                    b.ToTable("Role", "ref");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Legal Entity"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Natural Person"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Individual Enterpreneur"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Moderator"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("FoodCorp.DataAccess.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ProfileImagePath")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<DateTime>("RegistrationDateTimeUtc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("SmallDateTime")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<int>("Role")
                        .HasColumnType("int")
                        .HasColumnName("RoleId");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("Role");

                    b.ToTable("User", "dbo");
                });

            modelBuilder.Entity("FoodCorp.DataAccess.Entities.UserShowcaseImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserShowcaseImage", "dbo");
                });

            modelBuilder.Entity("FoodCorp.DataAccess.Entities.User", b =>
                {
                    b.HasOne("FoodCorp.DataAccess.Entities.Role", null)
                        .WithMany("Users")
                        .HasForeignKey("Role")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodCorp.DataAccess.Entities.UserShowcaseImage", b =>
                {
                    b.HasOne("FoodCorp.DataAccess.Entities.User", "User")
                        .WithMany("ShowcaseImages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FoodCorp.DataAccess.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("FoodCorp.DataAccess.Entities.User", b =>
                {
                    b.Navigation("ShowcaseImages");
                });
#pragma warning restore 612, 618
        }
    }
}