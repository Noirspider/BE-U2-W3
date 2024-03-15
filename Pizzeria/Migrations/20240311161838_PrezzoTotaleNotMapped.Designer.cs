﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pizzeria.Data;

#nullable disable

namespace Pizzeria.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240311161838_PrezzoTotaleNotMapped")]
    partial class PrezzoTotaleNotMapped
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Pizzeria.Models.Ordine", b =>
                {
                    b.Property<int>("IdOrdine")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdOrdine"));

                    b.Property<DateTime>("DataOrdine")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdUtente")
                        .HasColumnType("int");

                    b.Property<string>("IndirizzoDiConsegna")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEvaso")
                        .HasColumnType("bit");

                    b.Property<string>("Nota")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdOrdine");

                    b.HasIndex("IdUtente");

                    b.ToTable("Ordini");
                });

            modelBuilder.Entity("Pizzeria.Models.Prodotto", b =>
                {
                    b.Property<int>("IdProdotto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProdotto"));

                    b.Property<string>("ImgProdotto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ingredienti")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeProdotto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PrezzoProdotto")
                        .HasColumnType("float");

                    b.Property<int>("TempoConsegna")
                        .HasColumnType("int");

                    b.HasKey("IdProdotto");

                    b.ToTable("Prodotti");
                });

            modelBuilder.Entity("Pizzeria.Models.ProdottoAcquistato", b =>
                {
                    b.Property<int>("IdProdottoAcquistato")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProdottoAcquistato"));

                    b.Property<int>("IdOrdine")
                        .HasColumnType("int");

                    b.Property<int>("IdProdotto")
                        .HasColumnType("int");

                    b.Property<int>("Quantita")
                        .HasColumnType("int");

                    b.HasKey("IdProdottoAcquistato");

                    b.HasIndex("IdOrdine");

                    b.HasIndex("IdProdotto");

                    b.ToTable("ProdottiAcquistati");
                });

            modelBuilder.Entity("Pizzeria.Models.Utente", b =>
                {
                    b.Property<int>("IdUtente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUtente"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUtente");

                    b.ToTable("Utenti");
                });

            modelBuilder.Entity("Pizzeria.Models.Ordine", b =>
                {
                    b.HasOne("Pizzeria.Models.Utente", "Utente")
                        .WithMany("Ordini")
                        .HasForeignKey("IdUtente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Utente");
                });

            modelBuilder.Entity("Pizzeria.Models.ProdottoAcquistato", b =>
                {
                    b.HasOne("Pizzeria.Models.Ordine", "Ordine")
                        .WithMany("ProdottiAcquistati")
                        .HasForeignKey("IdOrdine")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pizzeria.Models.Prodotto", "Prodotto")
                        .WithMany("ProdottiAcquistati")
                        .HasForeignKey("IdProdotto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ordine");

                    b.Navigation("Prodotto");
                });

            modelBuilder.Entity("Pizzeria.Models.Ordine", b =>
                {
                    b.Navigation("ProdottiAcquistati");
                });

            modelBuilder.Entity("Pizzeria.Models.Prodotto", b =>
                {
                    b.Navigation("ProdottiAcquistati");
                });

            modelBuilder.Entity("Pizzeria.Models.Utente", b =>
                {
                    b.Navigation("Ordini");
                });
#pragma warning restore 612, 618
        }
    }
}
