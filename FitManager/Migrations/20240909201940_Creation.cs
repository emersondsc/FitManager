using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class Creation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Despesa",
                columns: table => new
                {
                    DespesaId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false),
                    Categoria = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despesa", x => x.DespesaId);
                });

            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    FuncionarioId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Cargo = table.Column<string>(type: "text", nullable: false),
                    Salario = table.Column<decimal>(type: "numeric", nullable: false),
                    HorarioTrabalho = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.FuncionarioId);
                });

            migrationBuilder.CreateTable(
                name: "Plano",
                columns: table => new
                {
                    PlanoId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Preco = table.Column<decimal>(type: "numeric", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Duracao = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plano", x => x.PlanoId);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Telefone = table.Column<string>(type: "text", nullable: false),
                    PlanoAtualPlanoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.ClienteId);
                    table.ForeignKey(
                        name: "FK_Cliente_Plano_PlanoAtualPlanoId",
                        column: x => x.PlanoAtualPlanoId,
                        principalTable: "Plano",
                        principalColumn: "PlanoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pagamento",
                columns: table => new
                {
                    PagamentoId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValorPago = table.Column<decimal>(type: "numeric", nullable: false),
                    MetodoPagamento = table.Column<string>(type: "text", nullable: false),
                    Confirmado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamento", x => x.PagamentoId);
                    table.ForeignKey(
                        name: "FK_Pagamento_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_PlanoAtualPlanoId",
                table: "Cliente",
                column: "PlanoAtualPlanoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_ClienteId",
                table: "Pagamento",
                column: "ClienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Despesa");

            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.DropTable(
                name: "Pagamento");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Plano");
        }
    }
}
