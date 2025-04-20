using Backend_Gympro.Application.Services;
using Backend_Gympro.Domain.Entidades;
using Backend_Gympro.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Gympro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartamentoController : Controller
    {
        private readonly IDepartamentoService _departamentoService;

        public DepartamentoController(IDepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departamentos = await _departamentoService.GetAllDepartamentosAsync();
            return Ok(departamentos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var departamento = await _departamentoService.GetDepartamentoByIdAsync(id);
            if (departamento == null) return NotFound();
            return Ok(departamento);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Departamento departamento)
        {
            await _departamentoService.AddDepartamentoAsync(departamento);
            return CreatedAtAction(nameof(GetById), new { id = departamento.Id }, departamento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Departamento departamento)
        {
            if (id != departamento.Id) return BadRequest("El ID no coincide.");
            await _departamentoService.UpdateDepartamentoAsync(departamento);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _departamentoService.DeleteDepartamentoAsync(id);
            return NoContent();
        }
    }
}
