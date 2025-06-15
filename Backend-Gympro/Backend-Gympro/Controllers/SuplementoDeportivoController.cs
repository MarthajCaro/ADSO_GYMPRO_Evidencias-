using Backend_Gympro.Application.DTOs;
using Backend_Gympro.Application.Services;
using Backend_Gympro.Domain.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Gympro.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SuplementoDeportivoController : Controller
    {
        private readonly ISuplementoDeportivoService _suplementodeportivoService;

        public SuplementoDeportivoController(ISuplementoDeportivoService suplementodeportivoService)
        {
            _suplementodeportivoService = suplementodeportivoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var suplementodeportivos = await _suplementodeportivoService.GetAllSuplementosAsync();
            return Ok(suplementodeportivos);
        }

        [HttpGet("ObtenerSuplementosVendedor")]
        public async Task<IActionResult> GetSuplementosByUserId()
        {
            var vendedorId = int.Parse(User.FindFirst("usuarioId").Value);

            // Obtener los suplementos para ese usuario desde el servicio
            var suplementosDeportivo = await _suplementodeportivoService.GetSuplementosByUserIdAsync(vendedorId);

            if (suplementosDeportivo == null || suplementosDeportivo.Count == 0)
            {
                return NotFound("No se encontraron suplementos para este usuario.");
            }

            return Ok(suplementosDeportivo);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var suplementodeportivo = await _suplementodeportivoService.GetSuplementoByIdAsync(id);
            if (suplementodeportivo == null) return NotFound();
            return Ok(suplementodeportivo);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]  SuplementoDeportivoDto dto)
        {
            string rutaImagen = null;

            if (dto.Foto != null && dto.Foto.Length > 0)
            {
                // Crear nombre único
                var nombreArchivo = $"{Guid.NewGuid()}_{dto.Foto.FileName}";
                var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/suplementos");
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
                rutaImagen = $"/suplementos/{nombreArchivo}";
            }

            var spl = new SuplementoDeportivo
            {
                nombre = dto.nombre,
                tipo = dto.tipo,
                descripcion = dto.descripcion,
                precio = dto.precio,
                id_usuario = dto.id_usuario,
                UrlImagen = rutaImagen,
                Stock = dto.stock
            };

            await _suplementodeportivoService.AddSuplementoAsync(spl);

            //return Ok(new { mensaje = "Progreso guardado con éxito", spl });

            return CreatedAtAction(nameof(GetById), new { id = spl.id }, spl);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] SuplementoDeportivoDto dto)
        {
            string rutaImagen = null;

            if (id != dto.id) return BadRequest("El ID no coincide.");

            // Si se envió una imagen nueva, actualiza
            if (dto.Foto != null && dto.Foto.Length > 0)
            {
                // Crear nombre único
                var nombreArchivo = $"{Guid.NewGuid()}_{dto.Foto.FileName}";
                var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/suplementos");
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
                rutaImagen = $"/suplementos/{nombreArchivo}";
            }


                var spl = new SuplementoDeportivo
                {
                    id = dto.id,
                    nombre = dto.nombre,
                    tipo = dto.tipo,
                    descripcion = dto.descripcion,
                    precio = dto.precio,
                    id_usuario = dto.id_usuario,
                    UrlImagen = rutaImagen,
                    Stock = dto.stock
                };

                await _suplementodeportivoService.UpdateSuplementoAsync(spl);
                return NoContent();
        
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _suplementodeportivoService.DeleteSuplementoAsync(id);
            return NoContent();
        }

        [HttpPut("cambiar-estado/{id}")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody]  bool nuevoEstado)
        {
            var actualizado = await _suplementodeportivoService.CambiarEstado(id, nuevoEstado);
            if (!actualizado)
                return NotFound();

            return NoContent();
        }
    }
}
