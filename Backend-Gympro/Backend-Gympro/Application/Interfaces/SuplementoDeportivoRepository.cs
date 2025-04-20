using Backend_Gympro.Domain.Entidades;
using Backend_Gympro.Infrastructure.Context;
using Backend_Gympro.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend_Gympro.Application.Interfaces
{
    public class SuplementoDeportivoRepository : GenericRepository<SuplementoDeportivo>, ISuplementoDeportivoRepository
    {
        private readonly AppDbContext _context;
        public SuplementoDeportivoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<SuplementoDeportivo>> GetSuplementosByUserIdAsync(int userId)
        {
            return await _context.SuplementoDeportivo
                                 .Where(s => s.id_usuario == userId) // Filtrar por UserId
                                 .ToListAsync();
        }

        public async Task<bool> CambiarEstado(int id, bool nuevoEstado)
        {
            var suplemento = await _context.SuplementoDeportivo.FindAsync(id);
            if (suplemento == null)
                return false;

            suplemento.estado = nuevoEstado;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
