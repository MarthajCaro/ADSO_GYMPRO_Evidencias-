using Backend_Gympro.Application.Services;
using Backend_Gympro.Domain.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Gympro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InscripcionController : Controller
    {
        private readonly IInscripcionService _inscripcionService;

        public InscripcionController(IInscripcionService inscripcionService)
        {
            _inscripcionService = inscripcionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inscripciones = await _inscripcionService.GetAllInscripcionesAsync();
            return Ok(inscripciones);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var inscripcion = await _inscripcionService.GetInscripcionByIdAsync(id);
            if (inscripcion == null) return NotFound();
            return Ok(inscripcion);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Inscripcion inscripcion)
        {
            await _inscripcionService.AddInscripcionAsync(inscripcion);
            return CreatedAtAction(nameof(GetById), new { id = inscripcion.Id }, inscripcion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Inscripcion inscripcion)
        {
            if (id != inscripcion.Id) return BadRequest("El ID no coincide.");
            await _inscripcionService.UpdateInscripcionAsync(inscripcion);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _inscripcionService.DeleteInscripcionAsync(id);
            return NoContent();
        }
    }
}
