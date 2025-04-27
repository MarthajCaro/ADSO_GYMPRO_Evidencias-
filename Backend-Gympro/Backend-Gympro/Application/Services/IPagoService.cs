using Backend_Gympro.Application.DTOs;
using Backend_Gympro.Domain.Entidades;

namespace Backend_Gympro.Application.Services
{
    public interface IPagoService
    {
        Task<IEnumerable<Pago>> GetAllPagosAsync();
        Task<Pago> GetPagoByIdAsync(int id);
        Task AddPagoAsync(Pago pago);
        Task UpdatePagoAsync(Pago pago);
        Task DeletePagoAsync(int id);
        Task<List<ClienteMembresiaDto>> ObtenerClientesConMembresiaAsync();
    }
}
