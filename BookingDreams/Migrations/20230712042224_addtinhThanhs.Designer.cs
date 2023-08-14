﻿// <auto-generated />
using BookingDreams.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookingDreams.Migrations
{
    [DbContext(typeof(BookingDreamsContext))]
    [Migration("20230712042224_addtinhThanhs")]
    partial class addtinhThanhs
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BookingDreams.Data.KhachSan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GioiThieu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdHinhAnh")
                        .HasColumnType("int");

                    b.Property<int>("IdTinhThanh")
                        .HasColumnType("int");

                    b.Property<string>("MaKhachSan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenKhachSan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TieuDe")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("KhachSan");
                });

            modelBuilder.Entity("BookingDreams.Data.TinhThanh", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdHinhAnh")
                        .HasColumnType("int");

                    b.Property<string>("MaTinhThanh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenTinhThanh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TinhThanh");
                });
#pragma warning restore 612, 618
        }
    }
}
