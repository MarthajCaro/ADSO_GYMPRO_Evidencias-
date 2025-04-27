using Backend_Gympro.Application.Interfaces;
using Backend_Gympro.Domain.Entidades;

namespace Backend_Gympro.Application.Services
{
    public class PersonaService : IPersonaService
    {
        private readonly IPersonaRepository _repository;
        public PersonaService(IPersonaRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Persona>> GetAllPersonasAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Persona> GetPersonaByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<int> AddPersonaAsync(Persona persona)
        {
            await _repository.AddAsync(persona);
            await _repository.SaveChangesAsync();
            return persona.Id;
        }
        public async Task UpdatePersonaAsync(Persona persona)
        {
            _repository.Update(persona);
            await _repository.SaveChangesAsync();
        }
        public async Task DeletePersonaAsync(int id)
        {
            var persona = await _repository.GetByIdAsync(id);
            if (persona != null)
            {
                _repository.Delete(persona);
                await _repository.SaveChangesAsync();
            }
        }
        public bool ExisteCorreo(string correo)
        {
            return _repository.ExisteCorreo(correo);
        }
    }
}
