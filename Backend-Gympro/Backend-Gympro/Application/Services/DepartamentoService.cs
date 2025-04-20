using Backend_Gympro.Application.Interfaces;
using Backend_Gympro.Domain.Entidades;

namespace Backend_Gympro.Application.Services
{
    public class DepartamentoService : IDepartamentoService
    {
        private readonly IDepartamentoRepository _repository;
        public DepartamentoService(IDepartamentoRepository repository) 
        {
            // Constructor logic here
            _repository = repository;
        }

        public async Task<IEnumerable<Departamento>> GetAllDepartamentosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Departamento> GetDepartamentoByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddDepartamentoAsync(Departamento departamento)
        {
            await _repository.AddAsync(departamento);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateDepartamentoAsync(Departamento departamento)
        {
            _repository.Update(departamento);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteDepartamentoAsync(int id)
        {
            var departamento = await _repository.GetByIdAsync(id);
            if (departamento != null)
            {
                _repository.Delete(departamento);
                await _repository.SaveChangesAsync();
            }
        }
    }
}
