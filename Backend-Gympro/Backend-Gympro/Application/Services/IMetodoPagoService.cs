using Backend_Gympro.Domain.Entidades;

namespace Backend_Gympro.Application.Services
{
    public interface IMetodoPagoService
    {
        Task<IEnumerable<MetodoPago>> GetAllMetodoPagosAsync();
        Task<MetodoPago> GetMetodoPagoByIdAsync(int id);
        Task AddMetodoPagoAsync(MetodoPago metodoPago);
        Task UpdateMetodoPagoAsync(MetodoPago metodoPago);
        Task DeleteMetodoPagoAsync(int id);
    }
}
