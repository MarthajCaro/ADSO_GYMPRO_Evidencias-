using Backend_Gympro.Application.DTOs;
using Backend_Gympro.Domain.Entidades;
using Backend_Gympro.Infrastructure.Repositories;

namespace Backend_Gympro.Application.Interfaces
{
    public interface IUsuariosRepository : IGenericRepository<Usuarios>
    {
        Task<Usuarios> GetByCredentialsAsync(string usuario, string contraseña);
        Task<Usuarios?> GetByCorreoAsync(string correo);
        bool ExisteUsuario(string usuario);
        Task<List<UsuarioConsultaDTO>> ObtenerUsuariosAsync();
        Task<Usuarios> ObtenerUsuarioPorPersona(int idPersona);
        Task<bool> ActualizarUsuarioPorPersona(int idPersona, ActualizarUsuarioDTO usuarioActualizado);
        Task<Usuarios> ObtenerPorNombre(string nombreUsuario);
    }
}
