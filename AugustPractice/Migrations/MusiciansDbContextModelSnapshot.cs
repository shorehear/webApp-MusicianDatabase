﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MusiciansAPI.Database;

#nullable disable

namespace AugustPractice.Migrations
{
    [DbContext(typeof(MusiciansDbContext))]
    partial class MusiciansDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("MusiciansAPI.Database.Album", b =>
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

                    b.Property<int>("NumberOfTracks")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CollectiveId");

                    b.HasIndex("MusicianId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("MusiciansAPI.Database.Collective", b =>
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

            modelBuilder.Entity("MusiciansAPI.Database.Country", b =>
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

            modelBuilder.Entity("MusiciansAPI.Database.Musician", b =>
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

            modelBuilder.Entity("MusiciansAPI.Database.Song", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AlbumId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SongTitle")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("MusiciansAPI.Database.Album", b =>
                {
                    b.HasOne("MusiciansAPI.Database.Collective", "Collective")
                        .WithMany("Albums")
                        .HasForeignKey("CollectiveId");

                    b.HasOne("MusiciansAPI.Database.Musician", "Musician")
                        .WithMany("Albums")
                        .HasForeignKey("MusicianId");

                    b.Navigation("Collective");

                    b.Navigation("Musician");
                });

            modelBuilder.Entity("MusiciansAPI.Database.Musician", b =>
                {
                    b.HasOne("MusiciansAPI.Database.Collective", "Collective")
                        .WithMany("CollectiveMembers")
                        .HasForeignKey("CollectiveId");

                    b.HasOne("MusiciansAPI.Database.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Collective");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("MusiciansAPI.Database.Song", b =>
                {
                    b.HasOne("MusiciansAPI.Database.Album", "Album")
                        .WithMany()
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");
                });

            modelBuilder.Entity("MusiciansAPI.Database.Collective", b =>
                {
                    b.Navigation("Albums");

                    b.Navigation("CollectiveMembers");
                });

            modelBuilder.Entity("MusiciansAPI.Database.Musician", b =>
                {
                    b.Navigation("Albums");
                });
#pragma warning restore 612, 618
        }
    }
}
