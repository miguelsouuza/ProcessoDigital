using ProcessoDigital_Server.Data.Model;

namespace ProcessoDigital_Server.Services.Interfaces
{
    public interface IProcessoService
    {
        Task<IEnumerable<ProcessoModel>> GetAllAsync();
        Task<IEnumerable<ProcessoModel>> GetByClienteIdAsync(int clienteId);
        Task<ProcessoModel?> GetByIdAsync(int id);
        Task AddAsync(ProcessoModel processo);
        Task UpdateAsync(ProcessoModel processo);
        Task DeleteAsync(int id);
    }
}
