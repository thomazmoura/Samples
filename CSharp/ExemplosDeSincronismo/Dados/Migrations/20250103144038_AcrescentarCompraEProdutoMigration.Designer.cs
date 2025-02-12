﻿// <auto-generated />
using System;
using ExemplosDeSincronismo.Dados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExemplosDeSincronismo.Dados.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20250103144038_AcrescentarCompraEProdutoMigration")]
    partial class AcrescentarCompraEProdutoMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ExemplosDeSincronismo.Entidades.Compra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataDaCompra")
                        .HasColumnType("datetime2");

                    b.Property<int>("PessoaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PessoaId");

                    b.ToTable("Compras");
                });

            modelBuilder.Entity("ExemplosDeSincronismo.Entidades.ItemDaCompra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CompraId")
                        .HasColumnType("int");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompraId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("ItemDaCompra");
                });

            modelBuilder.Entity("ExemplosDeSincronismo.Entidades.Pessoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apelido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataDeNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PessoaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PessoaId");

                    b.ToTable("Pessoas");
                });

            modelBuilder.Entity("ExemplosDeSincronismo.Entidades.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ValorUnitario")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("ExemplosDeSincronismo.Entidades.Compra", b =>
                {
                    b.HasOne("ExemplosDeSincronismo.Entidades.Pessoa", "Pessoa")
                        .WithMany("Compras")
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("ExemplosDeSincronismo.Entidades.ItemDaCompra", b =>
                {
                    b.HasOne("ExemplosDeSincronismo.Entidades.Compra", "Compra")
                        .WithMany("ItensDaCompra")
                        .HasForeignKey("CompraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExemplosDeSincronismo.Entidades.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compra");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("ExemplosDeSincronismo.Entidades.Pessoa", b =>
                {
                    b.HasOne("ExemplosDeSincronismo.Entidades.Pessoa", null)
                        .WithMany("PessoasAtivasComMesmaDataDeAniversario")
                        .HasForeignKey("PessoaId");
                });

            modelBuilder.Entity("ExemplosDeSincronismo.Entidades.Compra", b =>
                {
                    b.Navigation("ItensDaCompra");
                });

            modelBuilder.Entity("ExemplosDeSincronismo.Entidades.Pessoa", b =>
                {
                    b.Navigation("Compras");

                    b.Navigation("PessoasAtivasComMesmaDataDeAniversario");
                });
#pragma warning restore 612, 618
        }
    }
}
