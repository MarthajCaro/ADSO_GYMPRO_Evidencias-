using Backend_Gympro.Application.Interfaces;
using Backend_Gympro.Domain.Entidades;

namespace Backend_Gympro.Application.Services
{
    public class MembresiaService : IMembresiaService
    {
        private readonly IMembresiaRepository _repository;
        public MembresiaService(IMembresiaRepository repository)
        {
            // Constructor logic here
            _repository = repository;
        }
        public async Task<IEnumerable<Membresia>> GetAllMembresiasAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Membresia> GetMembresiaByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task AddMembresiaAsync(Membresia membresia)
        {
            await _repository.AddAsync(membresia);
            await _repository.SaveChangesAsync();
        }
        public async Task UpdateMembresiaAsync(Membresia membresia)
        {
            _repository.Update(membresia);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteMembresiaAsync(int id)
        {
            var membresia = await _repository.GetByIdAsync(id);
            if (membresia != null)
            {
                _repository.Delete(membresia);
                await _repository.SaveChangesAsync();
            }
        }
    }
}