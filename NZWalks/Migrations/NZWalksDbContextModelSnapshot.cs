﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NZWalks.API.Data;

#nullable disable

namespace NZWalks.API.Migrations
{
    [DbContext(typeof(NZWalksDbContext))]
    partial class NZWalksDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NZWalks.API.Models.Domain.Difficulty", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Difficulties");

                    b.HasData(
                        new
                        {
                            id = new Guid("a5846353-4058-4d25-8009-5ce53bc477a2"),
                            name = "Easy"
                        },
                        new
                        {
                            id = new Guid("6fa3457b-0b32-435a-9655-72fec73bec45"),
                            name = "Hard"
                        },
                        new
                        {
                            id = new Guid("666b45b2-e595-43f4-9ddf-22bfa3aefbf7"),
                            name = "Medium"
                        });
                });

            modelBuilder.Entity("NZWalks.API.Models.Domain.Regions", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegionImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            id = new Guid("69a8231e-22f4-4f72-99b6-e2b7bdc430e7"),
                            Code = "AKL",
                            Name = "Auckland",
                            RegionImageUrl = "https://www.shutterstock.com/image-photo/beautiful-mountains-landscape-pictures-arang-kel-kashmir-2499596223"
                        },
                        new
                        {
                            id = new Guid("c60012f0-1dfe-4f2f-9af5-d8d7c5d499d6"),
                            Code = "KSM",
                            Name = "Kashmir",
                            RegionImageUrl = "https://www.shutterstock.com/image-photo/stunning-view-kashmir's-aru-valley-sunset-where-2473158391"
                        });
                });

            modelBuilder.Entity("NZWalks.API.Models.Domain.Walks", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DifficultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WalkImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("lengthInKm")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyId");

                    b.HasIndex("RegionId");

                    b.ToTable("Walks");
                });

            modelBuilder.Entity("NZWalks.API.Models.Domain.Walks", b =>
                {
                    b.HasOne("NZWalks.API.Models.Domain.Difficulty", "Difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NZWalks.API.Models.Domain.Regions", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Difficulty");

                    b.Navigation("Region");
                });
#pragma warning restore 612, 618
        }
    }
}
