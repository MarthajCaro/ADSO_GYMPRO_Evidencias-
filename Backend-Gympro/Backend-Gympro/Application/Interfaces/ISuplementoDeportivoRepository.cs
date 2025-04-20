using Backend_Gympro.Domain.Entidades;
using Backend_Gympro.Infrastructure.Repositories;

namespace Backend_Gympro.Application.Interfaces
{
    public interface ISuplementoDeportivoRepository : IGenericRepository<SuplementoDeportivo>
    {
        Task<List<SuplementoDeportivo>> GetSuplementosByUserIdAsync(int userId);
        Task<bool> CambiarEstado(int id, bool nuevoEstado);
    }
}
