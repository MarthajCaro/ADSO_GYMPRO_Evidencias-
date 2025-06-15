using Backend_Gympro.Application.DTOs;
using Backend_Gympro.Domain.Entidades;

namespace Backend_Gympro.Application.Services
{
    public interface IProgresoFisicoService
    {
        Task<IEnumerable<ProgresoFisico>> GetAllProgresosAsync();
        Task<ProgresoFisico> GetProgresoByIdAsync(int id);
        Task AddProgresoFisicoAsync(ProgresoFisico progresoFisico);
        Task UpdateProgresoFisicoAsync(ProgresoFisico progresoFisico);
        Task DeleteProgresoFisicoAsync(int id);
        Task<List<ProgresoConVariacionDto>> ObtenerProgresosConVariacion(int usuarioId);
        Task<List<ProgresoComparadoDto>> ObtenerComparaciones();
    }
}
