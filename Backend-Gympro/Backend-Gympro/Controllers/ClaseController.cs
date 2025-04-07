using Backend_Gympro.Application.Services;
using Backend_Gympro.Domain.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Gympro.Controllers
{
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var clase = await _claseService.GetClaseByIdAsync(id);
            if (clase == null) return NotFound();
            return Ok(clase);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Clase clase)
        {
            await _claseService.AddClaseAsync(clase);
            return CreatedAtAction(nameof(GetById), new { id = clase.Id }, clase);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Clase clase)
        {
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
    }
}
