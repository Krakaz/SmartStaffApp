﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repo.Models;

namespace Repo.Migrations
{
    [DbContext(typeof(RepoContext))]
    [Migration("20210217151720_AddStaffTable")]
    partial class AddStaffTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Repo.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.Property<int?>("StaffId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StaffId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Repo.Models.Interview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("InterviewCount")
                        .HasColumnType("int");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<string>("PositionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Interviews");
                });

            modelBuilder.Entity("Repo.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.Property<int?>("StaffId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StaffId");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("Repo.Models.Staff", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Birthday")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Female")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstWorkingDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsArived")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phones")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Skype")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("Repo.Models.Group", b =>
                {
                    b.HasOne("Repo.Models.Staff", null)
                        .WithMany("Groups")
                        .HasForeignKey("StaffId");
                });

            modelBuilder.Entity("Repo.Models.Position", b =>
                {
                    b.HasOne("Repo.Models.Staff", null)
                        .WithMany("Positions")
                        .HasForeignKey("StaffId");
                });

            modelBuilder.Entity("Repo.Models.Staff", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("Positions");
                });
#pragma warning restore 612, 618
        }
    }
}
