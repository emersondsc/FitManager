using System.Numerics;

namespace FitManagerAPI.Modelos
{
    public class Cliente
    {
        public Guid ClienteId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public Plano PlanoAtual { get; set; }
        public List<Pagamento> HistoricoPagamentos { get; set; }
        public bool Ativo { get; set; }

        //public void Ativar()
        //{
        //    Ativo = true;
        //}

        //public void Desativar()
        //{
        //    Ativo = false;
        //}
    }
}
