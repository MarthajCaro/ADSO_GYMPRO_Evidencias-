using Backend_Gympro.Domain.Entidades;

namespace Backend_Gympro.Application.Services
{
    public interface IMunicipioService
    {
        Task<IEnumerable<Municipio>> GetAllMunicipiosAsync();
        Task<Municipio> GetMunicipioByIdAsync(int id);
        Task AddMunicipioAsync(Municipio municipio);
        Task UpdateMunicipioAsync(Municipio municipio);
        Task DeleteMunicipioAsync(int id);
    }
}
