using Backend_Gympro.Application.Services;
using Backend_Gympro.Domain.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Gympro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagoController : Controller
    {
        private readonly IPagoService _pagoService;

        public PagoController(IPagoService pagoService)
        {
            _pagoService = pagoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pagos = await _pagoService.GetAllPagosAsync();
            return Ok(pagos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pago = await _pagoService.GetPagoByIdAsync(id);
            if (pago == null) return NotFound();
            return Ok(pago);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pago pago)
        {
            await _pagoService.AddPagoAsync(pago);
            return CreatedAtAction(nameof(GetById), new { id = pago.id }, pago);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Pago pago)
        {
            if (id != pago.id) return BadRequest("El ID no coincide.");
            await _pagoService.UpdatePagoAsync(pago);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _pagoService.DeletePagoAsync(id);
            return NoContent();
        }
    }
}
