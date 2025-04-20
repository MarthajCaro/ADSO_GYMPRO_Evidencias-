using Backend_Gympro.Domain.Entidades;
using Backend_Gympro.Infrastructure.Context;
using Backend_Gympro.Infrastructure.Repositories;

namespace Backend_Gympro.Application.Interfaces
{
    public class DepartamentoRepository : GenericRepository<Departamento>, IDepartamentoRepository
    {
        public DepartamentoRepository(AppDbContext context) : base(context)
        {
        }

    }
}
