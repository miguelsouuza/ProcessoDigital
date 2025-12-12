using ProcessoDigital_Server.Data.Model;

namespace ProcessoDigital_Server.Services.Interfaces
{
    public interface IAndamentoService
    {
        Task<IEnumerable<AndamentoModel>> GetAllAndamentosAsync();
        Task<IEnumerable<AndamentoModel>> GetByProcessoIdAsync(int processoId);
        Task<AndamentoModel?> GetByIdAsync(int id);
        Task AddAsync(AndamentoModel andamento);
        Task UpdateAsync(AndamentoModel andamento);
        Task DeleteAsync(int id);
    }
}
