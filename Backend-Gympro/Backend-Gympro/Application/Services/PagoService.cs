using Backend_Gympro.Application.Interfaces;
using Backend_Gympro.Domain.Entidades;

namespace Backend_Gympro.Application.Services
{
    public class PagoService : IPagoService
    {
        private readonly IPagoRepository _repository;
        public PagoService(IPagoRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Pago>> GetAllPagosAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Pago> GetPagoByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task AddPagoAsync(Pago pago)
        {
            await _repository.AddAsync(pago);
            await _repository.SaveChangesAsync();
        }
        public async Task UpdatePagoAsync(Pago pago)
        {
            _repository.Update(pago);
            await _repository.SaveChangesAsync();
        }
        public async Task DeletePagoAsync(int id)
        {
            var pago = await _repository.GetByIdAsync(id);
            if (pago != null)
            {
                _repository.Delete(pago);
                await _repository.SaveChangesAsync();
            }
        }
    }
}
