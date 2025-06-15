using Backend_Gympro.Application.DTOs;
using Backend_Gympro.Domain.Entidades;
using Backend_Gympro.Infrastructure.Repositories;

namespace Backend_Gympro.Application.Interfaces
{
    public interface IProgresoFisicoRepository : IGenericRepository<ProgresoFisico>
    {
        Task<List<ProgresoFisico>> ObtenerPorUsuario(int usuarioId);
        Task<List<ProgresoComparadoDto>> ObtenerComparacionProgresosPorCliente();
    }
}
