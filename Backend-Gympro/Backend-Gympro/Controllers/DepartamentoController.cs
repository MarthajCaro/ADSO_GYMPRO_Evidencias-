using Backend_Gympro.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Gympro.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly AppDbContext _context;

        public DepartamentoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("conexion")]
        public async Task<IActionResult> ProbarConexion()
        {
            var ok = await _context.Database.CanConnectAsync();

            return ok ? Ok("✅ Conexión exitosa con MySQL") : StatusCode(500, "❌ No se pudo conectar");
        }
    }
}
