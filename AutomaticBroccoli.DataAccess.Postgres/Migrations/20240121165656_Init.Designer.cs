﻿// <auto-generated />
using System;
using AutomaticBroccoli.DataAccess.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AutomaticBroccoli.DataAccess.Postgres.Migrations
{
    [DbContext(typeof(AutomaticBroccoliDbContext))]
    [Migration("20240121165656_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AutomaticBroccoli.DataAccess.Postgres.AutomaticBroccoliDbContext+OpenLoop", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<int>("UsertId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UsertId");

                    b.ToTable("OpenLoops");
                });

            modelBuilder.Entity("AutomaticBroccoli.DataAccess.Postgres.AutomaticBroccoliDbContext+User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AutomaticBroccoli.DataAccess.Postgres.AutomaticBroccoliDbContext+OpenLoop", b =>
                {
                    b.HasOne("AutomaticBroccoli.DataAccess.Postgres.AutomaticBroccoliDbContext+User", "User")
                        .WithMany("OpenLoops")
                        .HasForeignKey("UsertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AutomaticBroccoli.DataAccess.Postgres.AutomaticBroccoliDbContext+User", b =>
                {
                    b.Navigation("OpenLoops");
                });
#pragma warning restore 612, 618
        }
    }
}
