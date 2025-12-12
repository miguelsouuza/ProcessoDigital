using ProcessoDigital_Server.Data.Model;
namespace ProcessoDigital_Server.Services.Interfaces
{    
        public interface ILancamentoService
        {
            Task<IEnumerable<LancamentoModel>> GetAllAsync();
            Task<LancamentoModel?> GetByIdAsync(int id);
            Task AddAsync(LancamentoModel lancamento);
            Task UpdateAsync(LancamentoModel lancamento);
            Task DeleteAsync(int id);
        }
    // Lembre-se de criar a implementação (LancamentoService.cs) e registrar no Program.cs
    // (Exemplo: builder.Services.AddScoped<ILancamentoService, LancamentoService>();)
}
