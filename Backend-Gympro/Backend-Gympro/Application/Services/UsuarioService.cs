using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend_Gympro.Application.DTOs;
using Backend_Gympro.Application.Interfaces;
using Backend_Gympro.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Backend_Gympro.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuariosRepository _repository;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;

        public UsuarioService(IEmailService emailService, IUsuariosRepository repository, IConfiguration config)
        {
            _emailService = emailService;
            _repository = repository;
            _config = config;
        }
        public async Task<IEnumerable<Usuarios>> GetAllUsuariosAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Usuarios> GetUsuarioByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<int> AddUsuarioAsync(Usuarios usuario)
        {
            await _repository.AddAsync(usuario);
            await _repository.SaveChangesAsync();
            return usuario.id;
        }
        public async Task UpdateUsuarioAsync(Usuarios usuario)
        {
            _repository.Update(usuario);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteUsuarioAsync(int id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario != null)
            {
                _repository.Delete(usuario);
                await _repository.SaveChangesAsync();
            }
        }

        public string GenerateJwtToken(Usuarios usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.usuario),
                new Claim("UsuarioId", usuario.id.ToString()),
                new Claim("RolId", usuario.RolId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Usuarios?> ValidarCredencialesAsync(LoginDto dto)
        {
            return await _repository.GetByCredentialsAsync(dto.Usuario, dto.Contrasena);
        }

        public async Task<string> OlvidarContraseñaAsync(string correo)
        {
            var usuario = await _repository.GetByCorreoAsync(correo);

            if (usuario == null)
                return "No hay ninguna cuenta asociada a ese correo.";

            // URL del enlace de restablecimiento (este enlace sería parte de tu frontend)
            var resetPasswordUrl = $"https://tudominio.com/reset-password?token";

            // Contenido del correo
            var subject = "Recuperación de Contraseña";
            var body = $"Haz clic en el siguiente enlace para restablecer tu contraseña: {resetPasswordUrl}";

            // Enviar correo con el enlace de recuperación
            await _emailService.SendEmailAsync(correo, subject, body);

            return "Se ha enviado un correo con instrucciones para restablecer la contraseña.";
        }

        public bool ExisteUsuario(string usuario)
        {
            return _repository.ExisteUsuario(usuario);
        }

        public async Task<List<UsuarioConsultaDTO>> ObtenerUsuariosAsync()
        {
            return await _repository.ObtenerUsuariosAsync();
        }

        public async Task<Usuarios> ObtenerUsuarioPorPersona(int idPersona)
        {
            return await _repository.ObtenerUsuarioPorPersona(idPersona);
        }

        public async Task<bool> ActualizarUsuarioPorPersona(int idPersona, ActualizarUsuarioDTO usuarioActualizado)
        {
            return await _repository.ActualizarUsuarioPorPersona(idPersona, usuarioActualizado);
        }
    }
}
