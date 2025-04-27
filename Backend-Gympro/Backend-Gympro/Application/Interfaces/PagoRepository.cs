using Backend_Gympro.Application.DTOs;
using Backend_Gympro.Domain.Entidades;
using Backend_Gympro.Infrastructure.Context;
using Backend_Gympro.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend_Gympro.Application.Interfaces
{
    public class PagoRepository : GenericRepository<Pago>, IPagoRepository
    {
        private readonly AppDbContext _context;
        public PagoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<ClienteMembresiaDto>> ObtenerClientesConMembresiaAsync()
        {
            var hoy = DateTime.Now;

            var query = from usuario in _context.Usuarios
                        join persona in _context.Persona on usuario.PersonaId equals persona.Id
                        join pago in _context.Pago on usuario.id equals pago.id_usuario
                        join membresia in _context.Membresia on pago.membresia_id equals membresia.id
                        join tipoMembresia in _context.TipoMembresia on membresia.id_tipo_membresia equals tipoMembresia.id
                        select new ClienteMembresiaDto
                        {
                            NombreCliente = persona.nombre + " " + persona.apellido,
                            Membresia = tipoMembresia.nombre,
                            DescripcionMembresia = membresia.descripcion,
                            Precio = pago.precio,
                            FechaVencimiento = pago.fecha_vigencia,
                            Estado = pago.fecha_vigencia >= hoy ? "Activo" : "Vencido"
                        };

            return await query.ToListAsync();
        }
    }
}
