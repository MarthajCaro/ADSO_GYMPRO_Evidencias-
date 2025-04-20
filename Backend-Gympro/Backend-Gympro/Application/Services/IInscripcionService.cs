using Backend_Gympro.Application.DTOs;
using Backend_Gympro.Domain.Entidades;

namespace Backend_Gympro.Application.Services
{
    public interface IInscripcionService
    {
        Task<IEnumerable<Inscripcion>> GetAllInscripcionesAsync();
        Task<Inscripcion> GetInscripcionByIdAsync(int id);
        Task AddInscripcionAsync(Inscripcion inscripcion);
        Task UpdateInscripcionAsync(Inscripcion inscripcion);
        Task DeleteInscripcionAsync(int id);
        List<PersonaClaseDto> ObtenerInscritos(int entrenadorId);
    }
}
