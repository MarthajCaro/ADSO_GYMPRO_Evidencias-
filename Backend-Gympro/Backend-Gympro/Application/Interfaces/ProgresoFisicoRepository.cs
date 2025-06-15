using Backend_Gympro.Application.DTOs;
using Backend_Gympro.Domain.Entidades;
using Backend_Gympro.Infrastructure.Context;
using Backend_Gympro.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend_Gympro.Application.Interfaces
{
    public class ProgresoFisicoRepository : GenericRepository<ProgresoFisico>, IProgresoFisicoRepository
    {
        private readonly AppDbContext _context;
        public ProgresoFisicoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ProgresoFisico>> ObtenerPorUsuario(int usuarioId)
        {
            return await _context.ProgresoFisico
                                 .Where(p => p.UsuarioId == usuarioId)
                                 .OrderBy(p => p.FechaRegistro)
                                 .ToListAsync();
        }

        public async Task<List<ProgresoComparadoDto>> ObtenerComparacionProgresosPorCliente()
        {
            var progresos = await _context.ProgresoFisico
                .Include(p => p.Usuario)
                .OrderByDescending(p => p.FechaRegistro)
                .ToListAsync();

            var comparaciones = progresos
                .GroupBy(p => p.UsuarioId)
                .Select(grupo =>
                {
                    var progresosCliente = grupo.OrderByDescending(p => p.FechaRegistro).ToList();
                    var actual = progresosCliente.ElementAtOrDefault(0);
                    var anterior = progresosCliente.ElementAtOrDefault(1);

                    return new ProgresoComparadoDto
                    {
                        UsuarioId = actual.UsuarioId,
                        NombreUsuario = actual.Usuario.usuario,
                        PesoActual = actual.Peso,
                        PesoAnterior = anterior?.Peso,
                        VariacionPeso = anterior != null ? actual.Peso - anterior.Peso : (decimal?)null,
                        VariacionMedidaCintura = anterior != null ? actual.MedidaCintura - anterior.MedidaCintura : (decimal?)null,
                        VariacionMedidaPecho = anterior != null ? actual.MedidaPecho - anterior.MedidaPecho : (decimal?)null,
                        UltimaFechaRegistro = actual.FechaRegistro,
                        Foto = actual.FotoProgresoUrl
                    };
                }).ToList();

            return comparaciones;
        }
    }
}
