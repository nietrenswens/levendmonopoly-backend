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
    [Migration("20241123120846_AddStartCode")]
    partial class AddStartCode
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
                            Id = new Guid("cf670bc6-7c2c-4623-a4d9-e9fba7301fbd"),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("532c33b2-6ba3-4433-b05d-38becf9196d2"),
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

            modelBuilder.Entity("LevendMonopoly.Api.Models.StartcodePull", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Startcode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("StartcodePull");
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
                            Id = new Guid("e6c4e92c-1315-4cb4-be5a-6063a347a757"),
                            Balance = 5000,
                            Name = "RensTest",
                            PasswordHash = "E5DYIwmlJ7vpwjg7lEPGn13QoFMf3dxDtcX4rUvM8fY=",
                            PasswordSalt = "U9V0DaBkWpahR1DVfLrzAw=="
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
                            Id = new Guid("6b6b9357-aae9-466f-b65c-96e49cad0072"),
                            Name = "Rens",
                            PasswordHash = "WUaTfb77tlrAN56kOucZJsBUXgIwWXLTfS25aKp2SvI=",
                            PasswordSalt = "Fx/IsjDpKoDUs7VcKf5xug==",
                            RoleId = new Guid("cf670bc6-7c2c-4623-a4d9-e9fba7301fbd")
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