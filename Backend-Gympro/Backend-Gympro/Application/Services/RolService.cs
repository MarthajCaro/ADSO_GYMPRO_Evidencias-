using Backend_Gympro.Application.Interfaces;
using Backend_Gympro.Domain.Entidades;

namespace Backend_Gympro.Application.Services
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _repository;
        public RolService(IRolRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Rol>> GetAllRolesAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Rol> GetRolByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task AddRolAsync(Rol rol)
        {
            await _repository.AddAsync(rol);
            await _repository.SaveChangesAsync();
        }
        public async Task UpdateRolAsync(Rol rol)
        {
            _repository.Update(rol);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteRolAsync(int id)
        {
            var rol = await _repository.GetByIdAsync(id);
            if (rol != null)
            {
                _repository.Delete(rol);
                await _repository.SaveChangesAsync();
            }
        }
    }
}
