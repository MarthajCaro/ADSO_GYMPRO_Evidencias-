using Backend_Gympro.Application.Interfaces;
using Backend_Gympro.Domain.Entidades;

namespace Backend_Gympro.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuariosRepository _repository;
        public UsuarioService(IUsuariosRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Usuarios>> GetAllUsuariosAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Usuarios> GetUsuarioByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task AddUsuarioAsync(Usuarios usuario)
        {
            await _repository.AddAsync(usuario);
            await _repository.SaveChangesAsync();
        }
        public async Task UpdateUsuarioAsync(Usuarios usuario)
        {
            _repository.Update(usuario);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteUsuarioAsync(int id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario != null)
            {
                _repository.Delete(usuario);
                await _repository.SaveChangesAsync();
            }
        }
    }
}
