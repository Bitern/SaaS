﻿// <auto-generated />
using System;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SaaS.Infrastructure.Database.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("saas")
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SaaS.Domain.Emails.Email", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("body");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("EmailSender")
                        .HasColumnType("text")
                        .HasColumnName("email_sender");

                    b.Property<string>("FailureReason")
                        .HasColumnType("text")
                        .HasColumnName("failure_reason");

                    b.Property<string>("RecipientEmail")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("recipient_email");

                    b.Property<DateTime?>("SentAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("sent_at");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("subject");

                    b.HasKey("Id")
                        .HasName("pk_emails");

                    b.ToTable("emails", "saas");
                });

            modelBuilder.Entity("SaaS.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<bool>("IsEmailVerified")
                        .HasColumnType("boolean")
                        .HasColumnName("is_email_verified");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_users_email");

                    b.ToTable("users", "saas");
                });
#pragma warning restore 612, 618
        }
    }
}
