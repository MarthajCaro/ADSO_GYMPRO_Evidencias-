using Backend_Gympro.Application.Services;
using Backend_Gympro.Domain.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Gympro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoMembresiaController : Controller
    {
        private readonly ITipoMembresiaService _tipomembresiaService;

        public TipoMembresiaController(ITipoMembresiaService tipomembresiaService)
        {
            _tipomembresiaService = tipomembresiaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tipomembresias = await _tipomembresiaService.GetAllTipoMembresiasAsync();
            return Ok(tipomembresias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tipomembresia = await _tipomembresiaService.GetTipoMembresiaByIdAsync(id);
            if (tipomembresia == null) return NotFound();
            return Ok(tipomembresia);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TipoMembresia tipomembresia)
        {
            await _tipomembresiaService.AddTipoMembresiaAsync(tipomembresia);
            return CreatedAtAction(nameof(GetById), new { id = tipomembresia.id }, tipomembresia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TipoMembresia tipomembresia)
        {
            if (id != tipomembresia.id) return BadRequest("El ID no coincide.");
            await _tipomembresiaService.UpdateTipoMembresiaAsync(tipomembresia);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _tipomembresiaService.DeleteTipoMembresiaAsync(id);
            return NoContent();
        }
    }
}
