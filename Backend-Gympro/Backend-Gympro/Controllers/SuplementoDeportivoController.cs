using Backend_Gympro.Application.Services;
using Backend_Gympro.Domain.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Gympro.Controllers
{
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var suplementodeportivo = await _suplementodeportivoService.GetSuplementoByIdAsync(id);
            if (suplementodeportivo == null) return NotFound();
            return Ok(suplementodeportivo);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SuplementoDeportivo suplementodeportivo)
        {
            await _suplementodeportivoService.AddSuplementoAsync(suplementodeportivo);
            return CreatedAtAction(nameof(GetById), new { id = suplementodeportivo.id }, suplementodeportivo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SuplementoDeportivo suplementodeportivo)
        {
            if (id != suplementodeportivo.id) return BadRequest("El ID no coincide.");
            await _suplementodeportivoService.UpdateSuplementoAsync(suplementodeportivo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _suplementodeportivoService.DeleteSuplementoAsync(id);
            return NoContent();
        }
    }
}
