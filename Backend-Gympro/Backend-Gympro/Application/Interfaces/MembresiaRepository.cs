using Backend_Gympro.Domain.Entidades;
using Backend_Gympro.Infrastructure.Context;
using Backend_Gympro.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend_Gympro.Application.Interfaces
{
    public class MembresiaRepository : GenericRepository<Membresia>, IMembresiaRepository
    {
        private readonly AppDbContext _context;
        public MembresiaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Membresia>> ObtenerConTipoAsync()
        {
            return await _context.Membresia
                .Include(m => m.TipoMembresia)
                .ToListAsync();
        }
    }
}
