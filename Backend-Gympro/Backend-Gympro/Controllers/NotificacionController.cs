using Backend_Gympro.Application.DTOs;
using Backend_Gympro.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Gympro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificacionController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public NotificacionController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("enviar-correo")]
        public async Task<IActionResult> EnviarCorreo([FromBody] FormularioContactoDto dto)
        {
            try
            {
                var asunto = "Nuevo mensaje desde el formulario de contacto";
                var cuerpo = $@"
                <h2>Nuevo mensaje recibido</h2>
                <p><strong>Nombre:</strong> {dto.Nombre}</p>
                <p><strong>Correo:</strong> {dto.Correo}</p>
                <p><strong>Asunto:</strong> {dto.Asunto}</p>
                <p><strong>Mensaje:</strong> {dto.Mensaje}</p>";

                await _emailService.SendEmailAsync("gymprobogota@gmail.com", asunto, cuerpo);
                return Ok(new { message = "Correo enviado exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error al enviar el correo: {ex.Message}" });
            }
        }
    }
}
