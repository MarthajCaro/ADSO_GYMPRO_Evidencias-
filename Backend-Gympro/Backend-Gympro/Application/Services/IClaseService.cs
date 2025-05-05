using Backend_Gympro.Application.DTOs;
using Backend_Gympro.Domain.Entidades;

namespace Backend_Gympro.Application.Services
{
    public interface IClaseService
    {
        Task<IEnumerable<Clase>> GetAllClasesAsync();
        Task<Clase> GetClaseByIdAsync(int id);
        Task AddClaseAsync(Clase clase);
        Task UpdateClaseAsync(Clase clase);
        Task DeleteClaseAsync(int id);
        Task<List<ClaseConEntrenadorDto>> ObtenerClasesConEntrenadorAsync();
    }
}