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
    }
}
