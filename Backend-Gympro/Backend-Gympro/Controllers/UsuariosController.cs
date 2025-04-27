using Backend_Gympro.Application.DTOs;
using Backend_Gympro.Application.Services;
using Backend_Gympro.Domain.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Gympro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPersonaService _personaService;

        public UsuariosController(IUsuarioService usuarioService, IPersonaService personaService)
        {
            _usuarioService = usuarioService;
            _personaService = personaService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioDTO usuarioDto)
        {
            var usuario = new Usuarios
            {
                usuario = usuarioDto.Usuario,
                contrasena = usuarioDto.Contrasena,
                PersonaId = usuarioDto.PersonaId,
                RolId = usuarioDto.RolId,
                estado = true // Por defecto, el usuario está activo
            };

            var id = await _usuarioService.AddUsuarioAsync(usuario);
            return Ok(id);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Usuarios usuario)
        {
            if (id != usuario.id) return BadRequest("El ID no coincide.");
            await _usuarioService.UpdateUsuarioAsync(usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _usuarioService.DeleteUsuarioAsync(id);
            return NoContent();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var usuario = await _usuarioService.ValidarCredencialesAsync(dto);
            if (usuario == null) return Unauthorized("Credenciales inválidas.");

            var token = _usuarioService.GenerateJwtToken(usuario);

            return Ok(new { token, usuario });
        }

        [HttpPost("olvidar-contrasena")]
        public async Task<IActionResult> OlvidarContrasena([FromBody] OlvidarContraseñaDto dto)
        {
            var resultado = await _usuarioService.OlvidarContraseñaAsync(dto.Correo);
            return Ok(resultado);
        }

        [HttpPost("validar")]
        public IActionResult ValidarUsuarioCorreo([FromBody] UsuarioCorreoDTO datos)
        {
            bool existeUsuario = _usuarioService.ExisteUsuario(datos.Usuario);
            bool existeCorreo = _personaService.ExisteCorreo(datos.Correo);

            return Ok(new { usuarioExistente = existeUsuario, correoExistente = existeCorreo });
        }

        [Authorize]
        [HttpGet("obtener-usuarios")]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            try
            {
                var usuarios = await _usuarioService.ObtenerUsuariosAsync();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener los usuarios: " + ex.Message);
            }
        }

        [Authorize]
        [HttpGet("porPersona/{idPersona}")]
        public async Task<IActionResult> ObtenerUsuarioPorPersona(int idPersona)
        {
            var usuario = await _usuarioService.ObtenerUsuarioPorPersona(idPersona);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [Authorize]
        [HttpPut("porPersona/{idPersona}")]
        public async Task<IActionResult> ActualizarUsuarioPorPersona(int idPersona, [FromBody] ActualizarUsuarioDTO usuarioActualizado)
        {
            var resultado = await _usuarioService.ActualizarUsuarioPorPersona(idPersona, usuarioActualizado);

            if (!resultado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
