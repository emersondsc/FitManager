namespace FitManagerAPI.Modelos
{
    public class Funcionario
    {
        public Guid FuncionarioId { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; } // Ex: Instrutor, Gerente
        public decimal Salario { get; set; }
        public string HorarioTrabalho { get; set; }

        //public Funcionario(string nome, string cargo, decimal salario)
        //{
        //    Nome = nome;
        //    Cargo = cargo;
        //    Salario = salario;
        //}
    }
}
