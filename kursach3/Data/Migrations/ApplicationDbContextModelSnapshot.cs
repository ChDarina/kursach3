﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using kursach3.Data;

#nullable disable

namespace kursach3.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("kursach3.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("kursach3.Models.Character", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnOrder(0);

                    b.Property<int>("RolePlayId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<string>("СharacterName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("UserId", "RolePlayId");

                    b.HasIndex("RolePlayId");

                    b.ToTable("Characters", (string)null);
                });

            modelBuilder.Entity("kursach3.Models.Feed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Feed", (string)null);
                });

            modelBuilder.Entity("kursach3.Models.Friend", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnOrder(0);

                    b.Property<string>("UserFriendId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnOrder(1);

                    b.Property<bool>("Confirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "UserFriendId");

                    b.HasIndex("UserFriendId");

                    b.ToTable("Friends", (string)null);
                });

            modelBuilder.Entity("kursach3.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("FromUserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("RolePlayId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("ToRoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FromUserId");

                    b.HasIndex("RolePlayId");

                    b.HasIndex("ToRoomId");

                    b.ToTable("Messages", (string)null);
                });

            modelBuilder.Entity("kursach3.Models.RolePlay", b =>
                {
                    b.Property<int>("RolePlayId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RolePlayId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MasterId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("RolePlayId");

                    b.HasIndex("MasterId");

                    b.ToTable("RolePlays", (string)null);
                });

            modelBuilder.Entity("kursach3.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AdminId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("RolePlayId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("RolePlayId");

                    b.ToTable("Rooms", (string)null);
                });

            modelBuilder.Entity("kursach3.Models.SocialMedia", b =>
                {
                    b.Property<int>("SocialMediaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SocialMediaId"), 1L, 1);

                    b.Property<string>("SocialMediaName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("SocialMediaId");

                    b.ToTable("SocialMedias", (string)null);
                });

            modelBuilder.Entity("kursach3.Models.SocialMediaUser", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnOrder(0);

                    b.Property<int>("SocialMediaId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<string>("UserReference")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("UserId", "SocialMediaId");

                    b.HasIndex("SocialMediaId");

                    b.ToTable("SocialMediaUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("kursach3.Models.Character", b =>
                {
                    b.HasOne("kursach3.Models.RolePlay", "RolePlay")
                        .WithMany("Characters")
                        .HasForeignKey("RolePlayId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("kursach3.Models.ApplicationUser", "User")
                        .WithMany("Characters")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RolePlay");

                    b.Navigation("User");
                });

            modelBuilder.Entity("kursach3.Models.Feed", b =>
                {
                    b.HasOne("kursach3.Models.ApplicationUser", "User")
                        .WithMany("UserFeed")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("kursach3.Models.Friend", b =>
                {
                    b.HasOne("kursach3.Models.ApplicationUser", "UserFriend")
                        .WithMany()
                        .HasForeignKey("UserFriendId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("kursach3.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("UserFriend");
                });

            modelBuilder.Entity("kursach3.Models.Message", b =>
                {
                    b.HasOne("kursach3.Models.ApplicationUser", "FromUser")
                        .WithMany("Messages")
                        .HasForeignKey("FromUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("kursach3.Models.RolePlay", null)
                        .WithMany("Messages")
                        .HasForeignKey("RolePlayId");

                    b.HasOne("kursach3.Models.Room", "ToRoom")
                        .WithMany("Messages")
                        .HasForeignKey("ToRoomId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FromUser");

                    b.Navigation("ToRoom");
                });

            modelBuilder.Entity("kursach3.Models.RolePlay", b =>
                {
                    b.HasOne("kursach3.Models.ApplicationUser", "Master")
                        .WithMany("RolePlays")
                        .HasForeignKey("MasterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Master");
                });

            modelBuilder.Entity("kursach3.Models.Room", b =>
                {
                    b.HasOne("kursach3.Models.ApplicationUser", "Admin")
                        .WithMany("Rooms")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("kursach3.Models.RolePlay", "RolePlay")
                        .WithMany("Rooms")
                        .HasForeignKey("RolePlayId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Admin");

                    b.Navigation("RolePlay");
                });

            modelBuilder.Entity("kursach3.Models.SocialMediaUser", b =>
                {
                    b.HasOne("kursach3.Models.SocialMedia", "SocialMedia")
                        .WithMany("SocialMediaUsers")
                        .HasForeignKey("SocialMediaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("kursach3.Models.ApplicationUser", "User")
                        .WithMany("SocialMediaUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SocialMedia");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("kursach3.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("kursach3.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("kursach3.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("kursach3.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("kursach3.Models.ApplicationUser", b =>
                {
                    b.Navigation("Characters");

                    b.Navigation("Messages");

                    b.Navigation("RolePlays");

                    b.Navigation("Rooms");

                    b.Navigation("SocialMediaUsers");

                    b.Navigation("UserFeed");
                });

            modelBuilder.Entity("kursach3.Models.RolePlay", b =>
                {
                    b.Navigation("Characters");

                    b.Navigation("Messages");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("kursach3.Models.Room", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("kursach3.Models.SocialMedia", b =>
                {
                    b.Navigation("SocialMediaUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
