using Backend_Gympro.Application.Interfaces;
using Backend_Gympro.Domain.Entidades;

namespace Backend_Gympro.Application.Services
{
    public class MunicipioService : IMunicipioService
    {
        private readonly IMunicipioRepository _repository;
        public MunicipioService(IMunicipioRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Municipio>> GetAllMunicipiosAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Municipio> GetMunicipioByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task AddMunicipioAsync(Municipio municipio)
        {
            await _repository.AddAsync(municipio);
            await _repository.SaveChangesAsync();
        }
        public async Task UpdateMunicipioAsync(Municipio municipio)
        {
            _repository.Update(municipio);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteMunicipioAsync(int id)
        {
            var municipio = await _repository.GetByIdAsync(id);
            if (municipio != null)
            {
                _repository.Delete(municipio);
                await _repository.SaveChangesAsync();
            }
        }
    }
}
