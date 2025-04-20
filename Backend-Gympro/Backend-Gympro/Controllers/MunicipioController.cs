using Backend_Gympro.Application.Services;
using Backend_Gympro.Domain.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Gympro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MunicipioController : Controller
    {
        private readonly IMunicipioService _municipioService;

        public MunicipioController(IMunicipioService municipioService)
        {
            _municipioService = municipioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var municipios = await _municipioService.GetAllMunicipiosAsync();
            return Ok(municipios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var municipio = await _municipioService.GetMunicipioByIdAsync(id);
            if (municipio == null) return NotFound();
            return Ok(municipio);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Municipio municipio)
        {
            await _municipioService.AddMunicipioAsync(municipio);
            return CreatedAtAction(nameof(GetById), new { id = municipio.Id }, municipio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Municipio municipio)
        {
            if (id != municipio.Id) return BadRequest("El ID no coincide.");
            await _municipioService.UpdateMunicipioAsync(municipio);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _municipioService.DeleteMunicipioAsync(id);
            return NoContent();
        }
    }
}
