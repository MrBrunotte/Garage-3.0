﻿// <auto-generated />
using System;
using Garage3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Garage3.Migrations
{
    [DbContext(typeof(Garage3Context))]
    partial class Garage3ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Garage3.Models.Entities.Parking", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ParkingSpaceID")
                        .HasColumnType("int");

                    b.Property<int>("VehicleTypeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ParkingSpaceID");

                    b.HasIndex("VehicleTypeID");

                    b.ToTable("Parking");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            ParkingSpaceID = 1,
                            VehicleTypeID = 1
                        });
                });

            modelBuilder.Entity("Garage3.Models.Entities.ParkingSpace", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<int>("ParkingSpaceNum")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("ParkingSpace");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Available = false,
                            ParkingSpaceNum = 1
                        },
                        new
                        {
                            ID = 2,
                            Available = true,
                            ParkingSpaceNum = 2
                        },
                        new
                        {
                            ID = 3,
                            Available = true,
                            ParkingSpaceNum = 3
                        });
                });

            modelBuilder.Entity("Garage3.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNum")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("Member");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConfirmPassword = "plugga",
                            Email = "kalle.kula@hotail.com",
                            FirstName = "Kalle",
                            LastName = "Kula",
                            Password = "plugga",
                            PhoneNum = "070234567"
                        },
                        new
                        {
                            Id = 2,
                            ConfirmPassword = "plugga2",
                            Email = "and.and@hotail.com",
                            FirstName = "Andreas",
                            LastName = "Andersson",
                            Password = "plugga2",
                            PhoneNum = "070234568"
                        },
                        new
                        {
                            Id = 3,
                            ConfirmPassword = "plugga3",
                            Email = "zlatan@hotail.com",
                            FirstName = "Zlatan",
                            LastName = "Ibrahimovic",
                            Password = "plugga3",
                            PhoneNum = "070234569"
                        });
                });

            modelBuilder.Entity("Garage3.Models.ParkedVehicle", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<int>("MemberID")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("RegNum")
                        .IsRequired()
                        .HasColumnType("nvarchar(8)")
                        .HasMaxLength(8);

                    b.Property<int>("VehicleTypeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MemberID");

                    b.HasIndex("VehicleTypeID");

                    b.ToTable("ParkedVehicle");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            ArrivalTime = new DateTime(2020, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Color = "Black",
                            Make = "Dodge",
                            MemberID = 1,
                            Model = "Nitro TR 4/4",
                            RegNum = "FZK678",
                            VehicleTypeID = 3
                        },
                        new
                        {
                            ID = 2,
                            ArrivalTime = new DateTime(2020, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Color = "Black",
                            Make = "Camaro",
                            MemberID = 3,
                            Model = "SS",
                            RegNum = "FZK677",
                            VehicleTypeID = 3
                        },
                        new
                        {
                            ID = 3,
                            ArrivalTime = new DateTime(2020, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Color = "Orange",
                            Make = "Harley Davidson",
                            MemberID = 2,
                            Model = "NightRod",
                            RegNum = "MKT677",
                            VehicleTypeID = 4
                        });
                });

            modelBuilder.Entity("Garage3.Models.VehicleTypes", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FillsNumberOfSpaces")
                        .HasColumnType("int");

                    b.Property<string>("VehicleType")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("ID");

                    b.ToTable("VehicleTypes");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            FillsNumberOfSpaces = 3,
                            VehicleType = "Bus"
                        },
                        new
                        {
                            ID = 2,
                            FillsNumberOfSpaces = 1,
                            VehicleType = "Car"
                        },
                        new
                        {
                            ID = 3,
                            FillsNumberOfSpaces = 1,
                            VehicleType = "Sportscar"
                        },
                        new
                        {
                            ID = 4,
                            FillsNumberOfSpaces = 1,
                            VehicleType = "Motorcycle"
                        });
                });

            modelBuilder.Entity("Garage3.Models.Entities.Parking", b =>
                {
                    b.HasOne("Garage3.Models.Entities.ParkingSpace", "ParkingSpace")
                        .WithMany()
                        .HasForeignKey("ParkingSpaceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Garage3.Models.VehicleTypes", "VehicleType")
                        .WithMany()
                        .HasForeignKey("VehicleTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Garage3.Models.ParkedVehicle", b =>
                {
                    b.HasOne("Garage3.Models.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Garage3.Models.VehicleTypes", "VehicleType")
                        .WithMany()
                        .HasForeignKey("VehicleTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
