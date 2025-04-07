using Backend_Gympro.Domain.Entidades;
using Backend_Gympro.Infrastructure.Context;
using Backend_Gympro.Infrastructure.Repositories;

namespace Backend_Gympro.Application.Interfaces
{
    public class UsuariosRepository : GenericRepository<Usuarios>, IUsuariosRepository
    {
        public UsuariosRepository(AppDbContext context) : base(context)
        {
        }
    }
}
