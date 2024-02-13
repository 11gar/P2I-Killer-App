﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace P2I_backend.Migrations
{
    [DbContext(typeof(ApiContext))]
    [Migration("20240213110544_shuffling")]
    partial class shuffling
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("Cible", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdCible")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdGame")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdKiller")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Cibles");
                });

            modelBuilder.Entity("Equipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdGame")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Equipes");
                });

            modelBuilder.Entity("Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsStarted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Kill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Confirmed")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("IdGame")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdKilled")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdKiller")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Kills");
                });

            modelBuilder.Entity("Moderate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdGame")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdModerator")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Moderators");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UserInGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Alive")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Famille")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdCible")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdGame")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdUser")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Kills")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("UsersInGames");
                });
#pragma warning restore 612, 618
        }
    }
}
