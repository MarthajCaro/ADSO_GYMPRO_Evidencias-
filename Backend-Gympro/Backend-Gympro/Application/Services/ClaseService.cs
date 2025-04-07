using Backend_Gympro.Application.Interfaces;
using Backend_Gympro.Domain.Entidades;

namespace Backend_Gympro.Application.Services
{
    public class ClaseService : IClaseService
    {
        private readonly IClaseRepository _repository;
        public ClaseService(IClaseRepository repository)
        {
            // Constructor logic here
            _repository = repository;
        }

        public async Task<IEnumerable<Clase>> GetAllClasesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Clase> GetClaseByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddClaseAsync(Clase clase)
        {
            await _repository.AddAsync(clase);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateClaseAsync(Clase clase)
        {
            _repository.Update(clase);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteClaseAsync(int id)
        {
            var clase = await _repository.GetByIdAsync(id);
            if (clase != null)
            {
                _repository.Delete(clase);
                await _repository.SaveChangesAsync();
            }
        }

    }
}
