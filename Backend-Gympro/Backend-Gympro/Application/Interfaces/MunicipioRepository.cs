using Backend_Gympro.Domain.Entidades;
using Backend_Gympro.Infrastructure.Context;
using Backend_Gympro.Infrastructure.Repositories;

namespace Backend_Gympro.Application.Interfaces
{
    public class MunicipioRepository : GenericRepository<Municipio>, IMunicipioRepository
    {
        public MunicipioRepository(AppDbContext context) : base(context)
        {
        }
    }
}
