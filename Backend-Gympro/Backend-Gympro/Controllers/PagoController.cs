using Backend_Gympro.Application.DTOs;
using Backend_Gympro.Application.Services;
using Backend_Gympro.Domain.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Gympro.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Create(PagoDto pagoDto)
        {
            // Mapea el DTO al modelo de pago
            var pago = new Pago
            {
                precio = pagoDto.Precio,
                fecha_pago = pagoDto.FechaPago,
                fecha_vigencia = pagoDto.FechaVigencia,
                id_usuario = pagoDto.IdUsuario,
                id_metodo_pago = pagoDto.IdMetodoPago,
                MembresiaId = pagoDto.membresia_id
            };

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

        [HttpGet("clientes-membresia")]
        public async Task<IActionResult> GetClientesConMembresia()
        {
            var result = await _pagoService.ObtenerClientesConMembresiaAsync();
            return Ok(result);
        }

        [HttpGet("usuario/{idUsuario}")]
        public async Task<IActionResult> GetPagosPorUsuario(int idUsuario)
        {
            var pagos = await _pagoService.GetAllPagosAsync();

            var ultimoPago = pagos
                .Where(p => p.id_usuario == idUsuario)
                .OrderByDescending(p => p.fecha_pago)
                .FirstOrDefault();

            if (ultimoPago == null)
                return NotFound();

            return Ok(ultimoPago);
        }
    }
}
