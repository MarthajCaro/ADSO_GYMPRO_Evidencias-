using Backend_Gympro.Domain.Entidades;
using Backend_Gympro.Infrastructure.Context;
using Backend_Gympro.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend_Gympro.Application.Interfaces
{
    public class PersonaRepository : GenericRepository<Persona>, IPersonaRepository
    {
        private readonly AppDbContext _context;
        public PersonaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public bool ExisteCorreo(string correo)
        {
            return _context.Persona.Any(p => p.correo == correo);
        }
    }
}
