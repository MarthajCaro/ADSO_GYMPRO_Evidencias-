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
    public class MetodoPagoController : Controller
    {
        private readonly IMetodoPagoService _metodopagoService;

        public MetodoPagoController(IMetodoPagoService metodopagoService)
        {
            _metodopagoService = metodopagoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var metodopagos = await _metodopagoService.GetAllMetodoPagosAsync();

            var metodosActivos = metodopagos
                    .Where(m => m.estado == "Activo")
                    .ToList();
            return Ok(metodosActivos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var metodopago = await _metodopagoService.GetMetodoPagoByIdAsync(id);
            if (metodopago == null) return NotFound();
            return Ok(metodopago);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MetodoPago metodopago)
        {
            await _metodopagoService.AddMetodoPagoAsync(metodopago);
            return CreatedAtAction(nameof(GetById), new { id = metodopago.id }, metodopago);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MetodoPago metodopago)
        {
            if (id != metodopago.id) return BadRequest("El ID no coincide.");
            await _metodopagoService.UpdateMetodoPagoAsync(metodopago);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _metodopagoService.DeleteMetodoPagoAsync(id);
            return NoContent();
        }
    }
}
