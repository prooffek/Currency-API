﻿// <auto-generated />
using System;
using AveneoRerutacja.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AveneoRerutacja.Migrations
{
    [DbContext(typeof(AuthenticationKeyDbContext))]
    [Migration("20211214225529_v_03")]
    partial class v_03
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("AveneoRerutacja.KeyGenerator.AuthenticationKey", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("KeyValue")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Keys");
                });
#pragma warning restore 612, 618
        }
    }
}