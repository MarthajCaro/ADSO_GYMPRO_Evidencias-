using Backend_Gympro.Application.Interfaces;
using Backend_Gympro.Domain.Entidades;

namespace Backend_Gympro.Application.Services
{
    public class TipoMembresiaService : ITipoMembresiaService
    {
        private readonly ITipoMembresiaRepository _repository;
        public TipoMembresiaService(ITipoMembresiaRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<TipoMembresia>> GetAllTipoMembresiasAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<TipoMembresia> GetTipoMembresiaByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task AddTipoMembresiaAsync(TipoMembresia tipoMembresia)
        {
            await _repository.AddAsync(tipoMembresia);
            await _repository.SaveChangesAsync();
        }
        public async Task UpdateTipoMembresiaAsync(TipoMembresia tipoMembresia)
        {
            _repository.Update(tipoMembresia);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteTipoMembresiaAsync(int id)
        {
            var tipoMembresia = await _repository.GetByIdAsync(id);
            if (tipoMembresia != null)
            {
                _repository.Delete(tipoMembresia);
                await _repository.SaveChangesAsync();
            }
        }
    }
}
