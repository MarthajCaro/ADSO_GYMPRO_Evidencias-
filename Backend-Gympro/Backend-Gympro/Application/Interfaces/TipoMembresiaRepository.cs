using Backend_Gympro.Domain.Entidades;
using Backend_Gympro.Infrastructure.Context;
using Backend_Gympro.Infrastructure.Repositories;

namespace Backend_Gympro.Application.Interfaces
{
    public class TipoMembresiaRepository : GenericRepository<TipoMembresia>, ITipoMembresiaRepository
    {
        public TipoMembresiaRepository(AppDbContext context) : base(context)
        {
        }
    }
}
