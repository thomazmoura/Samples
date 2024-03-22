﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Net8Playground.API.Dados;

#nullable disable

namespace Net8Playground.API.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20231209000859_AcrescentarPessoaMigration")]
    partial class AcrescentarPessoaMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Net8Playground.API.Entidades.Pessoa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pessoas");
                });

            modelBuilder.Entity("Net8Playground.API.Entidades.Pessoa", b =>
                {
                    b.OwnsOne("Net8Playground.API.Entidades.Ocupacao", "Ocupacao", b1 =>
                        {
                            b1.Property<Guid>("PessoaId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Cargo")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("DataDeInicio")
                                .HasColumnType("datetime2");

                            b1.Property<decimal>("ValorDaHora")
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)");

                            b1.HasKey("PessoaId");

                            b1.ToTable("Pessoas");

                            b1.ToJson("Ocupacao");

                            b1.WithOwner()
                                .HasForeignKey("PessoaId");

                            b1.OwnsOne("Net8Playground.API.Entidades.Empresa", "Empresa", b2 =>
                                {
                                    b2.Property<Guid>("OcupacaoPessoaId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Nome")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("OcupacaoPessoaId");

                                    b2.ToTable("Pessoas");

                                    b2.WithOwner()
                                        .HasForeignKey("OcupacaoPessoaId");
                                });

                            b1.Navigation("Empresa")
                                .IsRequired();
                        });

                    b.Navigation("Ocupacao")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}