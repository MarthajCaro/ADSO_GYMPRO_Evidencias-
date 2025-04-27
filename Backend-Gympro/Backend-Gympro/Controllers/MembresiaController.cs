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
        public async Task<IActionResult> Create([FromBody] TipoMembresiaDto dto)
        {
            var nuevaMembresia = new Membresia
            {
                Precio = dto.Precio,
                duracion_membresia_en_meses = dto.Duracion_Membresia_En_Meses,
                descripcion = dto.Descripcion,
                id_tipo_membresia = dto.Id_Tipo_Membresia
            };

            await _membresiaService.AddMembresiaAsync(nuevaMembresia);
            return Ok(nuevaMembresia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TipoMembresiaDto dto)
        {
            if (id != dto.Id) return BadRequest("El ID no coincide.");

            var editarMembresia = new Membresia
            {
                id =dto.Id,
                Precio = dto.Precio,
                duracion_membresia_en_meses = dto.Duracion_Membresia_En_Meses,
                descripcion = dto.Descripcion,
                id_tipo_membresia = dto.Id_Tipo_Membresia
            };
            await _membresiaService.UpdateMembresiaAsync(editarMembresia);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _membresiaService.DeleteMembresiaAsync(id);
            return NoContent();
        }

        [HttpGet("consultar-membresias")]
        public async Task<IActionResult> GetMembresias()
        {
            var lista = await _membresiaService.ObtenerMembresiasAsync();
            return Ok(lista);
        }
    }
}
