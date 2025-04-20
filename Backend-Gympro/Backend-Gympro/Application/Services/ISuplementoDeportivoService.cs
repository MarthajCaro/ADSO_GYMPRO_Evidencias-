using Backend_Gympro.Domain.Entidades;

namespace Backend_Gympro.Application.Services
{
    public interface ISuplementoDeportivoService
    {
        Task<IEnumerable<SuplementoDeportivo>> GetAllSuplementosAsync();
        Task<SuplementoDeportivo> GetSuplementoByIdAsync(int id);
        Task AddSuplementoAsync(SuplementoDeportivo suplemento);
        Task UpdateSuplementoAsync(SuplementoDeportivo suplemento);
        Task DeleteSuplementoAsync(int id);
        Task<List<SuplementoDeportivo>> GetSuplementosByUserIdAsync(int userId);
        Task<bool> CambiarEstado(int id, bool nuevoEstado);
    }
}
