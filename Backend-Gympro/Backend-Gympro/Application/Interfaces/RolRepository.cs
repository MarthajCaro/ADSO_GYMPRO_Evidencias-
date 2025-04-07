using Backend_Gympro.Domain.Entidades;
using Backend_Gympro.Infrastructure.Context;
using Backend_Gympro.Infrastructure.Repositories;

namespace Backend_Gympro.Application.Interfaces
{
    public class RolRepository : GenericRepository<Rol>, IRolRepository
    {
        public RolRepository(AppDbContext context) : base(context)
        {
        }
    }
}
