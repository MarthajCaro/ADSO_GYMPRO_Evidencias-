using Backend_Gympro.Application.DTOs;
using Backend_Gympro.Application.Interfaces;
using Backend_Gympro.Domain.Entidades;

namespace Backend_Gympro.Application.Services
{
    public class ProgresoFisicoService : IProgresoFisicoService
    {
        private readonly IProgresoFisicoRepository _repository;
        public ProgresoFisicoService(IProgresoFisicoRepository repository)
        {
            _repository = repository;
        }
        public async Task AddProgresoFisicoAsync(ProgresoFisico progresoFisico)
        {
            await _repository.AddAsync(progresoFisico);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteProgresoFisicoAsync(int id)
        {
            var progreso = await _repository.GetByIdAsync(id);
            if (progreso != null)
            {
                _repository.Delete(progreso);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ProgresoFisico>> GetAllProgresosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ProgresoFisico> GetProgresoByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateProgresoFisicoAsync(ProgresoFisico progresoFisico)
        {
            _repository.Update(progresoFisico);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<ProgresoConVariacionDto>> ObtenerProgresosConVariacion(int usuarioId)
        {
            var progresos = await _repository.ObtenerPorUsuario(usuarioId);
            progresos = progresos.OrderBy(p => p.FechaRegistro).ToList();

            var resultado = new List<ProgresoConVariacionDto>();
            ProgresoFisico? anterior = null;

            foreach (var actual in progresos)
            {
                var dto = new ProgresoConVariacionDto
                {
                    Id = actual.Id,
                    Peso = (double)actual.Peso,
                    MedidaCintura = (double)actual.MedidaCintura,
                    MedidaPecho = (double)actual.MedidaPecho,
                    Observaciones = actual.Observaciones,
                    FechaRegistro = actual.FechaRegistro,
                    UrlFoto = actual.FotoProgresoUrl,
                    VariacionPeso = (double)(anterior != null ? (actual.Peso - anterior.Peso) : 0),
                    VariacionCintura = (double)(anterior != null ? (actual.MedidaCintura - anterior.MedidaCintura) : 0),
                    VariacionPecho = (double)(anterior != null ? (actual.MedidaPecho - anterior.MedidaPecho) : 0)
                };

                resultado.Add(dto);
                anterior = actual;
            }

            return resultado;
        }

        public async Task<List<ProgresoComparadoDto>> ObtenerComparaciones()
        {
            return await _repository.ObtenerComparacionProgresosPorCliente();
        }
    }
}
