using Backend_Gympro.Application.Services;
using Backend_Gympro.Domain.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Gympro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaController : Controller
    {
        private readonly IPersonaService _personaService;

        public PersonaController(IPersonaService personaService)
        {
            _personaService = personaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var personas = await _personaService.GetAllPersonasAsync();
            return Ok(personas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var persona = await _personaService.GetPersonaByIdAsync(id);
            if (persona == null) return NotFound();
            return Ok(persona);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Persona persona)
        {
            await _personaService.AddPersonaAsync(persona);
            return CreatedAtAction(nameof(GetById), new { id = persona.Id }, persona);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,Persona persona)
        {
            if (id != persona.Id) return BadRequest("El ID no coincide.");
            await _personaService.UpdatePersonaAsync(persona);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _personaService.DeletePersonaAsync(id);
            return NoContent();
        }
    }
}
