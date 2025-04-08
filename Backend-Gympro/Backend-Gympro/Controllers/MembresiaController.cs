using Backend_Gympro.Application.Services;
using Backend_Gympro.Domain.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Gympro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembresiaController : Controller
    {
        private readonly IMembresiaService _membresiaService;

        public MembresiaController(IMembresiaService membresiaService)
        {
            _membresiaService = membresiaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var membresias = await _membresiaService.GetAllMembresiasAsync();
            return Ok(membresias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var membresia = await _membresiaService.GetMembresiaByIdAsync(id);
            if (membresia == null) return NotFound();
            return Ok(membresia);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Membresia membresia)
        {
            await _membresiaService.AddMembresiaAsync(membresia);
            return CreatedAtAction(nameof(GetById), new { id = membresia.id }, membresia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Membresia membresia)
        {
            if (id != membresia.id) return BadRequest("El ID no coincide.");
            await _membresiaService.UpdateMembresiaAsync(membresia);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _membresiaService.DeleteMembresiaAsync(id);
            return NoContent();
        }
    }
}
