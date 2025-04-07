using Backend_Gympro.Application.Interfaces;
using Backend_Gympro.Domain.Entidades;

namespace Backend_Gympro.Application.Services
{
    public class MetodoPagoService : IMetodoPagoService
    {
        private readonly IMetodoPagoRepository _repository;
        public MetodoPagoService(IMetodoPagoRepository repository)
        {
            // Constructor logic here
            _repository = repository;
        }
        public async Task<IEnumerable<MetodoPago>> GetAllMetodoPagosAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<MetodoPago> GetMetodoPagoByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task AddMetodoPagoAsync(MetodoPago metodoPago)
        {
            await _repository.AddAsync(metodoPago);
            await _repository.SaveChangesAsync();
        }
        public async Task UpdateMetodoPagoAsync(MetodoPago metodoPago)
        {
            _repository.Update(metodoPago);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteMetodoPagoAsync(int id)
        {
            var metodoPago = await _repository.GetByIdAsync(id);
            if (metodoPago != null)
            {
                _repository.Delete(metodoPago);
                await _repository.SaveChangesAsync();
            }
        }
    }
}
