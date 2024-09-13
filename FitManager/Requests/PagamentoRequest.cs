using FitManagerAPI.Modelos;

namespace FitManagerAPI.Requests
{
    public record PagamentoRequest(Guid clienteId, DateTime dataPagamento, decimal valorPago, string metodoPagamento, bool confirmado);

}
