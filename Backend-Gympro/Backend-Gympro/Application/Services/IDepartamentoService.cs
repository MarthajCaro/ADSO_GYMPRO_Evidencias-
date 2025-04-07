using Backend_Gympro.Domain.Entidades;

namespace Backend_Gympro.Application.Services
{
    public interface IDepartamentoService
    {
        Task<IEnumerable<Departamento>> GetAllDepartamentosAsync();
        Task<Departamento> GetDepartamentoByIdAsync(int id);
        Task AddDepartamentoAsync(Departamento departamento);
        Task UpdateDepartamentoAsync(Departamento departamento);
        Task DeleteDepartamentoAsync(int id);
    }
}
