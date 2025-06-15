using Backend_Gympro.Application.DTOs;
using Backend_Gympro.Application.Services;
using Backend_Gympro.Domain.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Gympro.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProgresoFisicoController : Controller
    {
        private readonly IProgresoFisicoService _progresoService;

        public ProgresoFisicoController(IProgresoFisicoService progresoService)
        {
            _progresoService = progresoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var progresos = await _progresoService.GetAllProgresosAsync();
            return Ok(progresos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var progreso = await _progresoService.GetProgresoByIdAsync(id);
            if (progreso == null) return NotFound();
            return Ok(progreso);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProgresoFisico progresoFisico)
        {
            await _progresoService.AddProgresoFisicoAsync(progresoFisico);
            return CreatedAtAction(nameof(GetById), new { id = progresoFisico.Id }, progresoFisico);
        }

        [HttpPost("crear-con-imagen")]
        public async Task<IActionResult> CrearConImagen([FromForm] ProgresoFisicoCreateDto dto)
        {
            string rutaImagen = null;

            if (dto.Foto != null && dto.Foto.Length > 0)
            {
                // Crear nombre único
                var nombreArchivo = $"{Guid.NewGuid()}_{dto.Foto.FileName}";
                var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

                // Asegurar carpeta
                if (!Directory.Exists(rutaCarpeta))
                    Directory.CreateDirectory(rutaCarpeta);

                // Guardar la imagen
                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    await dto.Foto.CopyToAsync(stream);
                }

                // Ruta que se guarda en la BD (pública)
                rutaImagen = $"/uploads/{nombreArchivo}";
            }

            var progreso = new ProgresoFisico
            {
                UsuarioId = dto.UsuarioId,
                Peso = dto.Peso,
                MedidaCintura = dto.MedidaCintura,
                MedidaPecho = dto.MedidaPecho,
                FechaRegistro = dto.Fecha,
                FotoProgresoUrl = rutaImagen,
                Observaciones = dto.Observaciones
            };

            await _progresoService.AddProgresoFisicoAsync(progreso);

            return Ok(new { mensaje = "Progreso guardado con éxito", progreso });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProgresoFisico progresoFisico)
        {
            if (id != progresoFisico.Id) return BadRequest("El ID no coincide.");
            await _progresoService.UpdateProgresoFisicoAsync(progresoFisico);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _progresoService.DeleteProgresoFisicoAsync(id);
            return NoContent();
        }

        [HttpGet("por-usuario")]
        public async Task<IActionResult> ObtenerPorUsuario(int usuarioId)
        {
            var progresos = await _progresoService.ObtenerProgresosConVariacion(usuarioId);
            return Ok(progresos);
        }

        [HttpGet("comparacion-todos")]
        public async Task<IActionResult> ObtenerComparaciones()
        {
            var datos = await _progresoService.ObtenerComparaciones();
            return Ok(datos);
        }
    }
}
