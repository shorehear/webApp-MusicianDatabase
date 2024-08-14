﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Musicians.Database;

#nullable disable

namespace Musicians.Database.Migrations
{
    [DbContext(typeof(MusiciansDbContext))]
    [Migration("20240814150201_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("Musicians.Database.Album", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AlbumTitle")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CollectiveId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("MusicianId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CollectiveId");

                    b.HasIndex("MusicianId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("Musicians.Database.Collective", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("CollectiveGenre")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CollectiveName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Collectives");
                });

            modelBuilder.Entity("Musicians.Database.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Musicians.Database.Musician", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CollectiveId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("MusicianBirthDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("MusicianDeathDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("MusicianName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CollectiveId");

                    b.HasIndex("CountryId");

                    b.ToTable("Musicians");
                });

            modelBuilder.Entity("Musicians.Database.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Musicians.Database.Album", b =>
                {
                    b.HasOne("Musicians.Database.Collective", "Collective")
                        .WithMany("Albums")
                        .HasForeignKey("CollectiveId");

                    b.HasOne("Musicians.Database.Musician", "Musician")
                        .WithMany("Albums")
                        .HasForeignKey("MusicianId");

                    b.Navigation("Collective");

                    b.Navigation("Musician");
                });

            modelBuilder.Entity("Musicians.Database.Musician", b =>
                {
                    b.HasOne("Musicians.Database.Collective", "Collective")
                        .WithMany("CollectiveMembers")
                        .HasForeignKey("CollectiveId");

                    b.HasOne("Musicians.Database.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Collective");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Musicians.Database.Collective", b =>
                {
                    b.Navigation("Albums");

                    b.Navigation("CollectiveMembers");
                });

            modelBuilder.Entity("Musicians.Database.Musician", b =>
                {
                    b.Navigation("Albums");
                });
#pragma warning restore 612, 618
        }
    }
}
