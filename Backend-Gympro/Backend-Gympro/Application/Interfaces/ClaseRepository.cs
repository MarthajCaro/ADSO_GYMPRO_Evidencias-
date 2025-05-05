using Backend_Gympro.Application.DTOs;
using Backend_Gympro.Domain.Entidades;
using Backend_Gympro.Infrastructure.Context;
using Backend_Gympro.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend_Gympro.Application.Interfaces
{
    public class ClaseRepository : GenericRepository<Clase>, IClaseRepository
    {
        private readonly AppDbContext _context;
        public ClaseRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ClaseConEntrenadorDto>> ObtenerClasesConEntrenadorAsync()
        {
            return await _context.Clase
                .Where(c => c.estado)
                .Include(c => c.Usuario)
                    .ThenInclude(u => u.Persona)
                .Select(c => new ClaseConEntrenadorDto
                {
                    IdClase = c.Id,
                    NombreClase = c.Nombre,
                    Descripcion = c.descripcion,
                    duracion = c.duracion_en_minutos,
                    NombreEntrenador = c.Usuario.Persona.nombre + " " + c.Usuario.Persona.apellido
                })
                .ToListAsync();
        }
    }
}
