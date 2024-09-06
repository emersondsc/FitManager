namespace FitManagerAPI.Modelos
{
    public class SistemaFinanceiro
    {
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
        public List<Plano> Planos { get; set; } = new List<Plano>();
        public List<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
        public List<Despesa> Despesas { get; set; } = new List<Despesa>();

        //public void AdicionarCliente(Cliente cliente)
        //{
        //    Clientes.Add(cliente);
        //}

        //public void RegistrarPagamento(Pagamento pagamento)
        //{
        //    Pagamentos.Add(pagamento);
        //}

        //public void RegistrarDespesa(Despesa despesa)
        //{
        //    Despesas.Add(despesa);
        //}

        //public RelatorioFinanceiro GerarRelatorio(DateTime dataInicio, DateTime dataFim)
        //{
        //    var relatorio = new RelatorioFinanceiro
        //    {
        //        DataInicio = dataInicio,
        //        DataFim = dataFim
        //    };

        //    relatorio.GerarRelatorio(Pagamentos, Despesas);

        //    return relatorio;
        //}
    }
}
