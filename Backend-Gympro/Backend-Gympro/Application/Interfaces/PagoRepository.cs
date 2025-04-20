using Backend_Gympro.Domain.Entidades;
using Backend_Gympro.Infrastructure.Context;
using Backend_Gympro.Infrastructure.Repositories;

namespace Backend_Gympro.Application.Interfaces
{
    public class PagoRepository : GenericRepository<Pago>, IPagoRepository
    {
        public PagoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
