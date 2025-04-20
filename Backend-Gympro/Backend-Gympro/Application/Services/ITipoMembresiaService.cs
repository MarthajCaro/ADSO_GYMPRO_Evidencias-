using Backend_Gympro.Domain.Entidades;

namespace Backend_Gympro.Application.Services
{
    public interface ITipoMembresiaService
    {
        Task<IEnumerable<TipoMembresia>> GetAllTipoMembresiasAsync();
        Task<TipoMembresia> GetTipoMembresiaByIdAsync(int id);
        Task AddTipoMembresiaAsync(TipoMembresia tipoMembresia);
        Task UpdateTipoMembresiaAsync(TipoMembresia tipoMembresia);
        Task DeleteTipoMembresiaAsync(int id);
    }
}
