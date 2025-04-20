using Backend_Gympro.Application.DTOs;
using Backend_Gympro.Domain.Entidades;

namespace Backend_Gympro.Application.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuarios>> GetAllUsuariosAsync();
        Task<Usuarios> GetUsuarioByIdAsync(int id);
        Task AddUsuarioAsync(Usuarios usuario);
        Task UpdateUsuarioAsync(Usuarios usuario);
        Task DeleteUsuarioAsync(int id);
        Task<Usuarios> ValidarCredencialesAsync(LoginDto dto);
        Task<string> OlvidarContraseñaAsync(string correo);
        string GenerateJwtToken(Usuarios usuario);
    }
}
