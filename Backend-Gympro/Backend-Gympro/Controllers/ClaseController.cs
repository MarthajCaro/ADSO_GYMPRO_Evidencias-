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
    public class ClaseController : Controller
    {
        private readonly IClaseService _claseService;

        public ClaseController(IClaseService claseService)
        {
            _claseService = claseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clases = await _claseService.GetAllClasesAsync();
            return Ok(clases);
        }

        [HttpGet("publico/clases")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerClasesPublicas()
        {
            var clases = await _claseService.GetAllClasesAsync();

            var clasesActivas = clases
                    .Where(c => c.estado == true)
                    .Select(c => new ClasePublicaDTO
                    {
                        Nombre = c.Nombre,
                        Dia = c.Dia,
                        Hora = c.Hora,
                        DuracionEnMinutos = c.duracion_en_minutos,
                        HoraFin = CalcularHoraFin(c.Hora, c.duracion_en_minutos)
                    })
                    .ToList();

            return Ok(clasesActivas);
        }

        private string CalcularHoraFin(string horaInicio, int duracionEnMinutos)
        {
            // Convertir la hora de inicio de string a TimeSpan
            TimeSpan horaInicioTimeSpan = TimeSpan.Parse(horaInicio);
            // Sumar la duración al tiempo de inicio
            DateTime horaFin = DateTime.Today.Add(horaInicioTimeSpan).AddMinutes(duracionEnMinutos);

            // Devuelve la hora de fin en formato "HH:mm"
            return horaFin.ToString("HH:mm");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var clase = await _claseService.GetClaseByIdAsync(id);
            if (clase == null) return NotFound();
            return Ok(clase);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClaseDOT claseDto)
        {
            var clase = new Clase
            {
                Nombre = claseDto.Nombre,
                duracion_en_minutos = claseDto.duracion_en_minutos,
                descripcion = claseDto.descripcion,
                id_usuario = claseDto.id_usuario,
                Dia = claseDto.Dia,
                Hora = claseDto.Hora,
                estado = claseDto.estado
            };

            await _claseService.AddClaseAsync(clase);
            return CreatedAtAction(nameof(GetById), new { id = clase.Id }, clase);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ClaseDOT claseDto)
        {
            var clase = new Clase
            {
                Id = claseDto.Id,
                Nombre = claseDto.Nombre,
                duracion_en_minutos = claseDto.duracion_en_minutos,
                descripcion = claseDto.descripcion,
                id_usuario = claseDto.id_usuario,
                Dia = claseDto.Dia,
                Hora = claseDto.Hora,
                estado = claseDto.estado
            };

            if (id != clase.Id) return BadRequest("El ID no coincide.");
            await _claseService.UpdateClaseAsync(clase);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _claseService.DeleteClaseAsync(id);
            return NoContent();
        }

        [HttpGet("con-entrenador")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerClasesConEntrenador()
        {
            var clases = await _claseService.ObtenerClasesConEntrenadorAsync();
            return Ok(clases);
        }
    }
}
