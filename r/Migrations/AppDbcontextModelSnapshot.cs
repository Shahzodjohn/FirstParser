﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using r.Context;

namespace r.Migrations
{
    [DbContext(typeof(AppDbcontext))]
    partial class AppDbcontextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("r.Entities.Categories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CategorySSS");
                });

            modelBuilder.Entity("r.Entities.DRT", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategorySSSId")
                        .HasColumnType("int");

                    b.Property<string>("DATA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GTIN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KLASYFIKACJAGPC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NAZWAPRODUKTU")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("POBIERZ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DRTS");
                });

            modelBuilder.Entity("r.Entities.Drop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DATA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GTIN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KLASYFIKACJAGPC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NAZWAPRODUKTU")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("POBIERZ")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Drops");
                });
#pragma warning restore 612, 618
        }
    }
}