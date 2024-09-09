namespace FitManagerAPI.Modelos
{
    public class Pagamento
    {
        public Guid PagamentoId { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorPago { get; set; }
        public string MetodoPagamento { get; set; } // Ex: Cartão, Boleto
        public bool Confirmado { get; set; }

        //public void ConfirmarPagamento()
        //{
        //    Confirmado = true;
        //}
    }
}
