using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FitManagerAPI.Modelos;
using Npgsql;

namespace FitManagerAPI.Data
{
    public class FitManagerAPIContext : DbContext
    {
        public FitManagerAPIContext (DbContextOptions<FitManagerAPIContext> options)
            : base(options)
        {
        }

        public DbSet<FitManagerAPI.Modelos.Cliente> Cliente { get; set; } = default!;
        public DbSet<FitManagerAPI.Modelos.Plano> Plano { get; set; } = default!;
        public DbSet<FitManagerAPI.Modelos.Pagamento> Pagamento { get; set; } = default!;
        public DbSet<FitManagerAPI.Modelos.Despesa> Despesa { get; set; } = default!;
        public DbSet<FitManagerAPI.Modelos.Funcionario> Funcionario { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Gera UUID automaticamente para a entidade Plano
            modelBuilder.Entity<Plano>()
                .Property(p => p.PlanoId)
                .HasDefaultValueSql("gen_random_uuid()");

            // Gera UUID automaticamente para a entidade Cliente
            modelBuilder.Entity<Cliente>()
                .Property(c => c.ClienteId)
                .HasDefaultValueSql("gen_random_uuid()");

            // Gera UUID automaticamente para a entidade Pagamento
            modelBuilder.Entity<Pagamento>()
                .Property(pg => pg.PagamentoId)
                .HasDefaultValueSql("gen_random_uuid()");

            // Gera UUID automaticamente para a entidade Despesa
            modelBuilder.Entity<Despesa>()
                .Property(d => d.DespesaId)
                .HasDefaultValueSql("gen_random_uuid()");

            // Gera UUID automaticamente para a entidade Funcionario
            modelBuilder.Entity<Funcionario>()
                .Property(f => f.FuncionarioId)
                .HasDefaultValueSql("gen_random_uuid()");
        }

    }
}
