

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

        public Pagamento() { }
        public Pagamento(Cliente cliente, DateTime dataPagamento, decimal valorPago, string metodoPagamento, bool confirmado)
        {
            Cliente = cliente;
            DataPagamento = dataPagamento; 
            ValorPago = valorPago;
            MetodoPagamento = metodoPagamento;
            Confirmado = confirmado;
        }


        //public void ConfirmarPagamento()
        //{
        //    Confirmado = true;
        //}
    }
}
