﻿// <auto-generated />
using System;
using Curso.ComercioElectronico.Infraestructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Curso.ComercioElectronico.Infraestructure.Migrations
{
    [DbContext(typeof(ComercioElectronicoDbContext))]
    [Migration("20221031151015_Orden.Ajustes.Tipo.Decimal")]
    partial class OrdenAjustesTipoDecimal
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("Curso.ComercioElectronico.Domain.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Curso.ComercioElectronico.Domain.Marca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Marcas");
                });

            modelBuilder.Entity("Curso.ComercioElectronico.Domain.Orden", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClienteId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Estado")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaAnulacion")
                        .HasColumnType("TEXT");

                    b.Property<string>("Observaciones")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Total")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Ordenes");
                });

            modelBuilder.Entity("Curso.ComercioElectronico.Domain.OrdenItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("Cantidad")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Observaciones")
                        .HasColumnType("TEXT");

                    b.Property<int>("OrdenId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Precio")
                        .HasColumnType("REAL");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("OrdenId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrdenItem");
                });

            modelBuilder.Entity("Curso.ComercioElectronico.Domain.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("Caducidad")
                        .HasColumnType("TEXT");

                    b.Property<int>("MarcaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("TEXT");

                    b.Property<string>("Observaciones")
                        .HasColumnType("TEXT");

                    b.Property<double>("Precio")
                        .HasColumnType("REAL");

                    b.Property<int>("TipoProductoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MarcaId");

                    b.HasIndex("TipoProductoId");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("Curso.ComercioElectronico.Domain.TipoProducto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TipoProductos");
                });

            modelBuilder.Entity("Curso.ComercioElectronico.Domain.Orden", b =>
                {
                    b.HasOne("Curso.ComercioElectronico.Domain.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("Curso.ComercioElectronico.Domain.OrdenItem", b =>
                {
                    b.HasOne("Curso.ComercioElectronico.Domain.Orden", "Orden")
                        .WithMany("Items")
                        .HasForeignKey("OrdenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Curso.ComercioElectronico.Domain.Producto", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orden");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Curso.ComercioElectronico.Domain.Producto", b =>
                {
                    b.HasOne("Curso.ComercioElectronico.Domain.Marca", "Marca")
                        .WithMany()
                        .HasForeignKey("MarcaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Curso.ComercioElectronico.Domain.TipoProducto", "TipoProducto")
                        .WithMany()
                        .HasForeignKey("TipoProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Marca");

                    b.Navigation("TipoProducto");
                });

            modelBuilder.Entity("Curso.ComercioElectronico.Domain.Orden", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
