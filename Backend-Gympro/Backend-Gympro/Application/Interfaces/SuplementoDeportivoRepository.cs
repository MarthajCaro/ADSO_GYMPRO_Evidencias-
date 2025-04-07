using Backend_Gympro.Domain.Entidades;
using Backend_Gympro.Infrastructure.Context;
using Backend_Gympro.Infrastructure.Repositories;

namespace Backend_Gympro.Application.Interfaces
{
    public class SuplementoDeportivoRepository : GenericRepository<SuplementoDeportivo>, ISuplementoDeportivoRepository
    {
        public SuplementoDeportivoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
