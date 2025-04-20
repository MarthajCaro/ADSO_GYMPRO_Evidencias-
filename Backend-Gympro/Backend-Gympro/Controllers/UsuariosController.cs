using Backend_Gympro.Application.DTOs;
using Backend_Gympro.Application.Services;
using Backend_Gympro.Domain.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Gympro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

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

        [HttpPost]
        public async Task<IActionResult> Create(Usuarios usuario)
        {
            await _usuarioService.AddUsuarioAsync(usuario);
            return CreatedAtAction(nameof(GetById), new { id = usuario.id }, usuario);
        }

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
    }
}
