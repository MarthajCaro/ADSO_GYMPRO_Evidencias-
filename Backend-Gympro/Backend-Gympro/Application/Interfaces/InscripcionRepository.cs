using Backend_Gympro.Application.DTOs;
using Backend_Gympro.Domain.Entidades;
using Backend_Gympro.Infrastructure.Context;
using Backend_Gympro.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend_Gympro.Application.Interfaces
{
    public class InscripcionRepository : GenericRepository<Inscripcion>, IInscripcionRepository
    {
        private readonly AppDbContext _context;
        public InscripcionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public List<PersonaClaseDto> ObtenerInscritosPorEntrenador(int entrenadorId)
        {
            return (from clase in _context.Clase
            where clase.id_usuario == entrenadorId
                    join inscripcion in _context.Inscripcion on clase.Id equals inscripcion.id_clase
                    join usuario in _context.Usuarios on inscripcion.id_usuario equals usuario.id
                    join persona in _context.Persona on usuario.PersonaId equals persona.Id
                    select new PersonaClaseDto
                    {
                        Nombre = persona.nombre,
                        Apellido = persona.apellido,
                        Correo = persona.correo,
                        NombreClase = clase.Nombre,
                        Duracion = clase.duracion_en_minutos,
                        Edad = EF.Functions.DateDiffYear(persona.fecha_nacimiento, DateTime.Now)
                    }).ToList();
        }
        public async Task<List<ClasePorUsuarioDto>> ObtenerClasesPorUsuarioAsync(int usuarioId)
        {
            return await _context.Inscripcion
                .Where(i => i.id_usuario == usuarioId)
                .Include(i => i.Clase)
                    .ThenInclude(c => c.Usuario) // Usuario que dicta la clase
                        .ThenInclude(u => u.Persona)
                .Select(i => new ClasePorUsuarioDto
                {
                    NombreClase = i.Clase.Nombre,
                    Descripcion = i.Clase.descripcion,
                    duracion = i.Clase.duracion_en_minutos,
                    horario = i.Clase.Hora,
                    NombreEntrenador = i.Clase.Usuario.Persona.nombre + " " + i.Clase.Usuario.Persona.apellido
                })
                .Distinct()
                .ToListAsync();
        }

    }
}
