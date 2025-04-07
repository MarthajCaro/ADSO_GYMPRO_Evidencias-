using Backend_Gympro.Domain.Entidades;

namespace Backend_Gympro.Application.Services
{
    public interface IRolService
    {
        Task<IEnumerable<Rol>> GetAllRolesAsync();
        Task<Rol> GetRolByIdAsync(int id);
        Task AddRolAsync(Rol rol);
        Task UpdateRolAsync(Rol rol);
        Task DeleteRolAsync(int id);
    }
}
