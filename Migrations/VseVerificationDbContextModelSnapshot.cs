﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VseVerification.Data;

#nullable disable

namespace VseVerification.Migrations
{
    [DbContext(typeof(VseVerificationDbContext))]
    partial class VseVerificationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.1.22076.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("VseVerification.Models.MemberVerification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AzureId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("azure_id");

                    b.Property<decimal>("DiscordId")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnName("discord_id");

                    b.HasKey("Id")
                        .HasName("pk_verifications");

                    b.HasIndex("AzureId")
                        .IsUnique()
                        .HasDatabaseName("ix_verifications_azure_id");

                    b.HasIndex("DiscordId")
                        .IsUnique()
                        .HasDatabaseName("ix_verifications_discord_id");

                    b.ToTable("verifications", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
