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
    [Migration("20210728093810_AddTableApplicants")]
    partial class AddTableApplicants
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("EmployeeValue", b =>
                {
                    b.Property<int>("EmployeesId")
                        .HasColumnType("int");

                    b.Property<int>("ValuesId")
                        .HasColumnType("int");

                    b.HasKey("EmployeesId", "ValuesId");

                    b.HasIndex("ValuesId");

                    b.ToTable("EmployeeValue");
                });

            modelBuilder.Entity("GroupStaff", b =>
                {
                    b.Property<int>("GroupsId")
                        .HasColumnType("int");

                    b.Property<int>("StaffsId")
                        .HasColumnType("int");

                    b.HasKey("GroupsId", "StaffsId");

                    b.HasIndex("StaffsId");

                    b.ToTable("GroupStaff");
                });

            modelBuilder.Entity("NotificationEmailNotificationType", b =>
                {
                    b.Property<int>("NotificationEmailsId")
                        .HasColumnType("int");

                    b.Property<int>("NotificationTypesId")
                        .HasColumnType("int");

                    b.HasKey("NotificationEmailsId", "NotificationTypesId");

                    b.HasIndex("NotificationTypesId");

                    b.ToTable("NotificationEmailNotificationType");
                });

            modelBuilder.Entity("PositionStaff", b =>
                {
                    b.Property<int>("PositionsId")
                        .HasColumnType("int");

                    b.Property<int>("StaffsId")
                        .HasColumnType("int");

                    b.HasKey("PositionsId", "StaffsId");

                    b.HasIndex("StaffsId");

                    b.ToTable("PositionStaff");
                });

            modelBuilder.Entity("Repo.Models.Applicant", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnglishLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastActivity")
                        .HasColumnType("datetime2");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Applicants");
                });

            modelBuilder.Entity("Repo.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Repo.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

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

            modelBuilder.Entity("Repo.Models.NotificationEmail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("NotificationEmails");
                });

            modelBuilder.Entity("Repo.Models.NotificationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("NotificationTypes");
                });

            modelBuilder.Entity("Repo.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<bool>("HasRO")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTarget")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PositionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("Repo.Models.Staff", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ArivedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Female")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FirstWorkingDate")
                        .HasColumnType("datetime2");

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

                    b.Property<DateTime?>("NotActiveDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Phones")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Skype")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Staffs");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Staff");
                });

            modelBuilder.Entity("Repo.Models.Value", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Values");
                });

            modelBuilder.Entity("Repo.Models.Employee", b =>
                {
                    b.HasBaseType("Repo.Models.Staff");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Comment2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quality")
                        .HasColumnType("int");

                    b.Property<DateTime>("RevisionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Employee");
                });

            modelBuilder.Entity("EmployeeValue", b =>
                {
                    b.HasOne("Repo.Models.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repo.Models.Value", null)
                        .WithMany()
                        .HasForeignKey("ValuesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GroupStaff", b =>
                {
                    b.HasOne("Repo.Models.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repo.Models.Staff", null)
                        .WithMany()
                        .HasForeignKey("StaffsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NotificationEmailNotificationType", b =>
                {
                    b.HasOne("Repo.Models.NotificationEmail", null)
                        .WithMany()
                        .HasForeignKey("NotificationEmailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repo.Models.NotificationType", null)
                        .WithMany()
                        .HasForeignKey("NotificationTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PositionStaff", b =>
                {
                    b.HasOne("Repo.Models.Position", null)
                        .WithMany()
                        .HasForeignKey("PositionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repo.Models.Staff", null)
                        .WithMany()
                        .HasForeignKey("StaffsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Repo.Models.Position", b =>
                {
                    b.HasOne("Repo.Models.Position", null)
                        .WithMany("Childs")
                        .HasForeignKey("PositionId");
                });

            modelBuilder.Entity("Repo.Models.Staff", b =>
                {
                    b.HasOne("Repo.Models.City", "City")
                        .WithMany("Staffs")
                        .HasForeignKey("CityId");

                    b.Navigation("City");
                });

            modelBuilder.Entity("Repo.Models.City", b =>
                {
                    b.Navigation("Staffs");
                });

            modelBuilder.Entity("Repo.Models.Position", b =>
                {
                    b.Navigation("Childs");
                });
#pragma warning restore 612, 618
        }
    }
}
