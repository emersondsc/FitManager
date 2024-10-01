namespace FitManagerAPI.Modelos
{
    public class RelatorioFinanceiro
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal Lucro => TotalReceitas - TotalDespesas;

        public void GerarRelatorio(List<Pagamento> pagamentos, List<Despesa> despesas)
        {
            TotalReceitas = pagamentos
                .Where(p => p.DataPagamento >= DataInicio && p.DataPagamento <= DataFim)
                .Sum(p => p.ValorPago);

            TotalDespesas = despesas
                .Where(d => d.Data >= DataInicio && d.Data <= DataFim)
                .Sum(d => d.Valor);
        }
    }
}
