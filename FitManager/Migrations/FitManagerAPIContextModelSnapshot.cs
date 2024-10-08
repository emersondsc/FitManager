﻿// <auto-generated />
using System;
using FitManagerAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FitManagerAPI.Migrations
{
    [DbContext(typeof(FitManagerAPIContext))]
    partial class FitManagerAPIContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FitManagerAPI.Modelos.Cliente", b =>
                {
                    b.Property<Guid>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("PlanoAtualPlanoId")
                        .HasColumnType("uuid");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ClienteId");

                    b.HasIndex("PlanoAtualPlanoId");

                    b.ToTable("Cliente", (string)null);
                });

            modelBuilder.Entity("FitManagerAPI.Modelos.Despesa", b =>
                {
                    b.Property<Guid>("DespesaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Data")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Valor")
                        .HasColumnType("numeric");

                    b.HasKey("DespesaId");

                    b.ToTable("Despesa", (string)null);
                });

            modelBuilder.Entity("FitManagerAPI.Modelos.Funcionario", b =>
                {
                    b.Property<Guid>("FuncionarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("Cargo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HorarioTrabalho")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Salario")
                        .HasColumnType("numeric");

                    b.HasKey("FuncionarioId");

                    b.ToTable("Funcionario", (string)null);
                });

            modelBuilder.Entity("FitManagerAPI.Modelos.Pagamento", b =>
                {
                    b.Property<Guid>("PagamentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Confirmado")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("DataPagamento")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("MetodoPagamento")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("ValorPago")
                        .HasColumnType("numeric");

                    b.HasKey("PagamentoId");

                    b.HasIndex("ClienteId");

                    b.ToTable("Pagamento", (string)null);
                });

            modelBuilder.Entity("FitManagerAPI.Modelos.Plano", b =>
                {
                    b.Property<Guid>("PlanoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<TimeSpan>("Duracao")
                        .HasColumnType("interval");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Preco")
                        .HasColumnType("numeric");

                    b.HasKey("PlanoId");

                    b.ToTable("Plano", (string)null);
                });

            modelBuilder.Entity("FitManagerAPI.Modelos.Cliente", b =>
                {
                    b.HasOne("FitManagerAPI.Modelos.Plano", "PlanoAtual")
                        .WithMany()
                        .HasForeignKey("PlanoAtualPlanoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlanoAtual");
                });

            modelBuilder.Entity("FitManagerAPI.Modelos.Pagamento", b =>
                {
                    b.HasOne("FitManagerAPI.Modelos.Cliente", "Cliente")
                        .WithMany("HistoricoPagamentos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("FitManagerAPI.Modelos.Cliente", b =>
                {
                    b.Navigation("HistoricoPagamentos");
                });
#pragma warning restore 612, 618
        }
    }
}
