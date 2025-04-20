using Backend_Gympro.Application.DTOs;
using Backend_Gympro.Application.Interfaces;
using Backend_Gympro.Domain.Entidades;

namespace Backend_Gympro.Application.Services
{
    public class InscripcionService : IInscripcionService
    {
        private readonly IInscripcionRepository _repository;
        public InscripcionService(IInscripcionRepository repository)
        {
            // Constructor logic here
            _repository = repository;
        }
        public async Task<IEnumerable<Inscripcion>> GetAllInscripcionesAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Inscripcion> GetInscripcionByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task AddInscripcionAsync(Inscripcion inscripcion)
        {
            await _repository.AddAsync(inscripcion);
            await _repository.SaveChangesAsync();
        }
        public async Task UpdateInscripcionAsync(Inscripcion inscripcion)
        {
            _repository.Update(inscripcion);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteInscripcionAsync(int id)
        {
            var inscripcion = await _repository.GetByIdAsync(id);
            if (inscripcion != null)
            {
                _repository.Delete(inscripcion);
                await _repository.SaveChangesAsync();
            }
        }
        public List<PersonaClaseDto> ObtenerInscritos(int entrenadorId)
        {
            return _repository.ObtenerInscritosPorEntrenador(entrenadorId);
        }
    }
}
