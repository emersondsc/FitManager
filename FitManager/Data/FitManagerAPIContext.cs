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

       
    }
}
