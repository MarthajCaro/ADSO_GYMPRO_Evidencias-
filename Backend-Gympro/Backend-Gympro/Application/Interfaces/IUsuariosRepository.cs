using Backend_Gympro.Application.DTOs;
using Backend_Gympro.Domain.Entidades;
using Backend_Gympro.Infrastructure.Repositories;

namespace Backend_Gympro.Application.Interfaces
{
    public interface IUsuariosRepository : IGenericRepository<Usuarios>
    {
        Task<Usuarios> GetByCredentialsAsync(string usuario, string contraseña);
        Task<Usuarios?> GetByCorreoAsync(string correo);
    }
}
