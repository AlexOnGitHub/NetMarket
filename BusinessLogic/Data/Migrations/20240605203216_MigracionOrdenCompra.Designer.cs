﻿// <auto-generated />
using System;
using BusinessLogic.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusinessLogic.Data.Migrations
{
    [DbContext(typeof(MarketDbContext))]
    [Migration("20240605203216_MigracionOrdenCompra")]
    partial class MigracionOrdenCompra
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Core.Entities.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("Core.Entities.Marca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Marca");
                });

            modelBuilder.Entity("Core.Entities.OrdenCompra.OrdenCompras", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CompradorEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("OrdenCompraFecha")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("PagoIntentoId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("TipoEnvioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TipoEnvioId");

                    b.ToTable("OrdenCompras");
                });

            modelBuilder.Entity("Core.Entities.OrdenCompra.OrdenItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int?>("OrdenComprasId")
                        .HasColumnType("int");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("OrdenComprasId");

                    b.ToTable("OrdenItems");
                });

            modelBuilder.Entity("Core.Entities.OrdenCompra.TipoEnvio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DeliveryTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("TipoEnvios");
                });

            modelBuilder.Entity("Core.Entities.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Imagen")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("MarcaId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,4)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("MarcaId");

                    b.ToTable("Producto");
                });

            modelBuilder.Entity("Core.Entities.OrdenCompra.OrdenCompras", b =>
                {
                    b.HasOne("Core.Entities.OrdenCompra.TipoEnvio", "TipoEnvio")
                        .WithMany()
                        .HasForeignKey("TipoEnvioId");

                    b.OwnsOne("Core.Entities.OrdenCompra.Direccion", "DireccionEnvio", b1 =>
                        {
                            b1.Property<int>("OrdenComprasId")
                                .HasColumnType("int");

                            b1.Property<string>("CP")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Calle")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Ciudad")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Departamento")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Pais")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("OrdenComprasId");

                            b1.ToTable("OrdenCompras");

                            b1.WithOwner()
                                .HasForeignKey("OrdenComprasId");
                        });

                    b.Navigation("DireccionEnvio");

                    b.Navigation("TipoEnvio");
                });

            modelBuilder.Entity("Core.Entities.OrdenCompra.OrdenItem", b =>
                {
                    b.HasOne("Core.Entities.OrdenCompra.OrdenCompras", null)
                        .WithMany("OrdenItems")
                        .HasForeignKey("OrdenComprasId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("Core.Entities.OrdenCompra.ProductoItemOrdenado", "ItemOrdenado", b1 =>
                        {
                            b1.Property<int>("OrdenItemId")
                                .HasColumnType("int");

                            b1.Property<string>("ImagenUrl")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("ProductoItemId")
                                .HasColumnType("int");

                            b1.Property<string>("ProductoNombre")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("OrdenItemId");

                            b1.ToTable("OrdenItems");

                            b1.WithOwner()
                                .HasForeignKey("OrdenItemId");
                        });

                    b.Navigation("ItemOrdenado");
                });

            modelBuilder.Entity("Core.Entities.Producto", b =>
                {
                    b.HasOne("Core.Entities.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Marca", "Marca")
                        .WithMany()
                        .HasForeignKey("MarcaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Marca");
                });

            modelBuilder.Entity("Core.Entities.OrdenCompra.OrdenCompras", b =>
                {
                    b.Navigation("OrdenItems");
                });
#pragma warning restore 612, 618
        }
    }
}
