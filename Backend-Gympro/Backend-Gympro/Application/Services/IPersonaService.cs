using Backend_Gympro.Domain.Entidades;

namespace Backend_Gympro.Application.Services
{
    public interface IPersonaService
    {
        Task<IEnumerable<Persona>> GetAllPersonasAsync();
        Task<Persona> GetPersonaByIdAsync(int id);
        Task AddPersonaAsync(Persona persona);
        Task UpdatePersonaAsync(Persona persona);
        Task DeletePersonaAsync(int id);
    }
}
