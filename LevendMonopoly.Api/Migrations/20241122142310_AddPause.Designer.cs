﻿// <auto-generated />
using System;
using LevendMonopoly.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LevendMonopoly.Api.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241122142310_AddPause")]
    partial class AddPause
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LevendMonopoly.Api.Models.Building", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<bool>("Tax")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("LevendMonopoly.Api.Models.ChanceCardPull", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("ChanceCardPulls");
                });

            modelBuilder.Entity("LevendMonopoly.Api.Models.GameSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Paused")
                        .HasColumnType("boolean");

                    b.Property<decimal>("TaxRate")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("GameSettings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Paused = false,
                            TaxRate = 0.6m
                        });
                });

            modelBuilder.Entity("LevendMonopoly.Api.Models.Log", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Suspicious")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("LevendMonopoly.Api.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3e964950-eb32-443a-afd8-e76124bfe616"),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("f07eff9a-b81a-4ea3-9da2-506f447889fa"),
                            Name = "Moderator"
                        });
                });

            modelBuilder.Entity("LevendMonopoly.Api.Models.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("LevendMonopoly.Api.Models.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Balance")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Teams");

                    b.HasData(
                        new
                        {
                            Id = new Guid("60d53f10-f2c4-464d-b02c-b0ce98f35d07"),
                            Balance = 5000,
                            Name = "RensTest",
                            PasswordHash = "VMLjnpipPgD04M60eJB5W1TJ1nG7bOSmfnUlZ8R6+FQ=",
                            PasswordSalt = "WUN41CnApyWW8eQpuIZqqg=="
                        });
                });

            modelBuilder.Entity("LevendMonopoly.Api.Models.TeamSession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsValid")
                        .HasColumnType("boolean");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("uuid");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamSessions");
                });

            modelBuilder.Entity("LevendMonopoly.Api.Models.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("Receiver")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("Sender")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("LevendMonopoly.Api.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("08ca0924-39f9-4d65-809a-1ddafd95a761"),
                            Name = "Rens",
                            PasswordHash = "K2NT0vUtwqoakiQbJOTupoM6OV5yxVW5wa5s+jRa6Rw=",
                            PasswordSalt = "mpvVxaYCDkpW4oMsuSqs8w==",
                            RoleId = new Guid("3e964950-eb32-443a-afd8-e76124bfe616")
                        });
                });

            modelBuilder.Entity("LevendMonopoly.Api.Models.Building", b =>
                {
                    b.HasOne("LevendMonopoly.Api.Models.Team", "Owner")
                        .WithMany("Buildings")
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("LevendMonopoly.Api.Models.ChanceCardPull", b =>
                {
                    b.HasOne("LevendMonopoly.Api.Models.Team", null)
                        .WithMany("ChanceCardPulls")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LevendMonopoly.Api.Models.Session", b =>
                {
                    b.HasOne("LevendMonopoly.Api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LevendMonopoly.Api.Models.TeamSession", b =>
                {
                    b.HasOne("LevendMonopoly.Api.Models.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("LevendMonopoly.Api.Models.User", b =>
                {
                    b.HasOne("LevendMonopoly.Api.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("LevendMonopoly.Api.Models.Team", b =>
                {
                    b.Navigation("Buildings");

                    b.Navigation("ChanceCardPulls");
                });
#pragma warning restore 612, 618
        }
    }
}
