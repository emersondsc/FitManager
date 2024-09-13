using FitManagerAPI.Modelos;

namespace FitManagerAPI.Requests
{
    public record ClienteRequest(string nome, string email, string telefone, Guid planoAtual, bool ativo);
}
