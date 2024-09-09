namespace FitManagerAPI.Modelos
{
    public class Despesa
    {
        public Guid DespesaId { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string Categoria { get; set; } // Ex: Salários, Aluguel

        public Despesa(string descricao, decimal valor, string categoria)
        {
            Descricao = descricao;
            Valor = valor;
            Categoria = categoria;
            Data = DateTime.Now;
        }
    }
}
