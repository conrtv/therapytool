﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using therapy.backend.Data;

#nullable disable

namespace therapy.backend.Migrations
{
    [DbContext(typeof(TherapyDbContext))]
    partial class TherapyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SchoolUser", b =>
                {
                    b.Property<int>("SchoolsId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("SchoolsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("SchoolUser");
                });

            modelBuilder.Entity("therapy.backend.Models.Domain.School", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Schools");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "1234 School St",
                            Name = "School 1"
                        },
                        new
                        {
                            Id = 2,
                            Address = "5678 School St",
                            Name = "School 2"
                        },
                        new
                        {
                            Id = 3,
                            Address = "91011 School St",
                            Name = "School 3"
                        });
                });

            modelBuilder.Entity("therapy.backend.Models.Domain.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Grade")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SchoolId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SchoolId");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateOfBirth = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Student",
                            LastName = "One",
                            SchoolId = 1
                        },
                        new
                        {
                            Id = 2,
                            DateOfBirth = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Student",
                            LastName = "Two",
                            SchoolId = 1
                        },
                        new
                        {
                            Id = 3,
                            DateOfBirth = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Student",
                            LastName = "Three",
                            SchoolId = 3
                        });
                });

            modelBuilder.Entity("therapy.backend.Models.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "email@email.com",
                            FirstName = "John",
                            LastName = "Doe",
                            PasswordHash = "password",
                            Role = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Email = "email@email.com",
                            FirstName = "Jane",
                            LastName = "Doe",
                            PasswordHash = "password",
                            Role = "OT"
                        },
                        new
                        {
                            Id = 3,
                            Email = "email@email.com",
                            FirstName = "John",
                            LastName = "Smith",
                            PasswordHash = "password",
                            Role = "PT"
                        });
                });

            modelBuilder.Entity("therapy.backend.Models.Domain.UserStudent", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("UserStudent");
                });

            modelBuilder.Entity("SchoolUser", b =>
                {
                    b.HasOne("therapy.backend.Models.Domain.School", null)
                        .WithMany()
                        .HasForeignKey("SchoolsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("therapy.backend.Models.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("therapy.backend.Models.Domain.Student", b =>
                {
                    b.HasOne("therapy.backend.Models.Domain.School", "School")
                        .WithMany("Students")
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("School");
                });

            modelBuilder.Entity("therapy.backend.Models.Domain.UserStudent", b =>
                {
                    b.HasOne("therapy.backend.Models.Domain.Student", "Student")
                        .WithMany("UserStudents")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("therapy.backend.Models.Domain.User", "User")
                        .WithMany("UserStudents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("User");
                });

            modelBuilder.Entity("therapy.backend.Models.Domain.School", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("therapy.backend.Models.Domain.Student", b =>
                {
                    b.Navigation("UserStudents");
                });

            modelBuilder.Entity("therapy.backend.Models.Domain.User", b =>
                {
                    b.Navigation("UserStudents");
                });
#pragma warning restore 612, 618
        }
    }
}
