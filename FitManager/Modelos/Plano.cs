namespace FitManagerAPI.Modelos
{
    public class Plano
    {
        public Guid PlanoId { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public TimeSpan Duracao { get; set; }
    }
}
