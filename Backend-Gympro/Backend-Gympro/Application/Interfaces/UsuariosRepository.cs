using Backend_Gympro.Application.DTOs;
using Backend_Gympro.Domain.Entidades;
using Backend_Gympro.Infrastructure.Context;
using Backend_Gympro.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend_Gympro.Application.Interfaces
{
    public class UsuariosRepository : GenericRepository<Usuarios>, IUsuariosRepository
    {
        private readonly AppDbContext _context;
        public UsuariosRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Usuarios?> GetByCorreoAsync(string correo)
        {
            return await _context.Usuarios
                .Include(u => u.Persona) // Asegúrate de incluir la persona para acceder al correo
                .FirstOrDefaultAsync(u => u.Persona.correo == correo);
        }

        public async Task<Usuarios> GetByCredentialsAsync(string usuario, string contraseña)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.usuario == usuario && u.contrasena == contraseña);
        }

        public bool ExisteUsuario(string usuario)
        {
            return _context.Usuarios.Any(u => u.usuario == usuario);
        }

        public async Task<List<UsuarioConsultaDTO>> ObtenerUsuariosAsync()
        {
            var usuarios = await _context.Usuarios
            .Include(u => u.Persona)
            .Include(u => u.Rol)
            .Select(u => new UsuarioConsultaDTO
            {
                Id = u.Persona.Id,
                NombreCompleto = u.Persona.nombre + " " + u.Persona.apellido,  
                Edad = DateTime.Now.Year - u.Persona.fecha_nacimiento.Year,  // Cálculo de la edad
                Correo = u.Persona.correo,
                Usuario = u.usuario,  
                Rol = u.Rol.nombre,
                Estado = u.estado
            })
            .ToListAsync();

            return usuarios;
        }

        public async Task<Usuarios> ObtenerUsuarioPorPersona(int idPersona)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.PersonaId == idPersona);
        }
        public async Task<bool> ActualizarUsuarioPorPersona(int idPersona, ActualizarUsuarioDTO usuarioActualizado)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.PersonaId == idPersona);

            if (usuario == null)
            {
                return false;
            }

            usuario.RolId = usuarioActualizado.RolId;
            usuario.estado = usuarioActualizado.Estado;

            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
