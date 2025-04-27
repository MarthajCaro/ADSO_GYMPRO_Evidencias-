using Backend_Gympro.Application.DTOs;
using Backend_Gympro.Domain.Entidades;

namespace Backend_Gympro.Application.Services
{
    public interface IMembresiaService
    {
        Task<IEnumerable<Membresia>> GetAllMembresiasAsync();
        Task<Membresia> GetMembresiaByIdAsync(int id);
        Task AddMembresiaAsync(Membresia membresia);
        Task UpdateMembresiaAsync(Membresia membresia);
        Task DeleteMembresiaAsync(int id);
        Task<List<MembresiaDto>> ObtenerMembresiasAsync();
    }
}
