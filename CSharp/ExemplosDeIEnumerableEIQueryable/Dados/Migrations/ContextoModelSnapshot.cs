﻿// <auto-generated />
using System;
using ExemplosDeIEnumerableEIQueryable.Dados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExemplosDeIEnumerableEIQueryable.Dados.Migrations
{
    [DbContext(typeof(Contexto))]
    partial class ContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ExemplosDeIEnumerableEIQueryable.Entidades.Compra", b =>
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

            modelBuilder.Entity("ExemplosDeIEnumerableEIQueryable.Entidades.Pessoa", b =>
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

            modelBuilder.Entity("ExemplosDeIEnumerableEIQueryable.Entidades.Produto", b =>
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

            modelBuilder.Entity("ExemplosDeIEnumerableEIQueryable.Entidades.Compra", b =>
                {
                    b.HasOne("ExemplosDeIEnumerableEIQueryable.Entidades.Pessoa", "Pessoa")
                        .WithMany("Compras")
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("ExemplosDeIEnumerableEIQueryable.Entidades.ItemDaCompra", "ItensDaCompra", b1 =>
                        {
                            b1.Property<int>("CompraId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<string>("NomeDoProduto")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("ProdutoId")
                                .HasColumnType("int");

                            b1.Property<int>("Quantidade")
                                .HasColumnType("int");

                            b1.Property<decimal>("ValorUnitario")
                                .HasColumnType("decimal(18,2)");

                            b1.HasKey("CompraId", "Id");

                            b1.ToTable("Compras");

                            b1.ToJson("ItensDaCompra");

                            b1.WithOwner("Compra")
                                .HasForeignKey("CompraId");

                            b1.Navigation("Compra");
                        });

                    b.Navigation("ItensDaCompra");

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("ExemplosDeIEnumerableEIQueryable.Entidades.Pessoa", b =>
                {
                    b.HasOne("ExemplosDeIEnumerableEIQueryable.Entidades.Pessoa", null)
                        .WithMany("PessoasAtivasComMesmaDataDeAniversario")
                        .HasForeignKey("PessoaId");
                });

            modelBuilder.Entity("ExemplosDeIEnumerableEIQueryable.Entidades.Pessoa", b =>
                {
                    b.Navigation("Compras");

                    b.Navigation("PessoasAtivasComMesmaDataDeAniversario");
                });
#pragma warning restore 612, 618
        }
    }
}
