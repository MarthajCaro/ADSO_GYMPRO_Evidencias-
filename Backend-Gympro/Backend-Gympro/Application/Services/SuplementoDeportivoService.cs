using Backend_Gympro.Application.Interfaces;
using Backend_Gympro.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Backend_Gympro.Application.Services
{
    public class SuplementoDeportivoService : ISuplementoDeportivoService
    {
        private readonly ISuplementoDeportivoRepository _repository;
        public SuplementoDeportivoService(ISuplementoDeportivoRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<SuplementoDeportivo>> GetAllSuplementosAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<SuplementoDeportivo> GetSuplementoByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task AddSuplementoAsync(SuplementoDeportivo suplemento)
        {
            await _repository.AddAsync(suplemento);
            await _repository.SaveChangesAsync();
        }
        public async Task UpdateSuplementoAsync(SuplementoDeportivo suplemento)
        {
            _repository.Update(suplemento);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteSuplementoAsync(int id)
        {
            var suplemento = await _repository.GetByIdAsync(id);
            if (suplemento != null)
            {
                _repository.Delete(suplemento);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task<List<SuplementoDeportivo>> GetSuplementosByUserIdAsync(int userId)
        {
            return await _repository.GetSuplementosByUserIdAsync(userId);
        }

        public async Task<bool> CambiarEstado(int id, bool nuevoEstado)
        {
            return await _repository.CambiarEstado(id, nuevoEstado);
        }
    }
}
