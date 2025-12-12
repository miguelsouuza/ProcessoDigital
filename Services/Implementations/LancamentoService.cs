using Microsoft.EntityFrameworkCore;
using ProcessoDigital_Server.Data.Context;
using ProcessoDigital_Server.Data.Model;
using ProcessoDigital_Server.Services.Interfaces;

namespace ProcessoDigital_Server.Services.Implementations
{
    public class LancamentoService : ILancamentoService
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;

        public LancamentoService(IDbContextFactory<AppDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public async Task<IEnumerable<LancamentoModel>> GetAllAsync()
        {
            await using var context = _dbFactory.CreateDbContext();
            return await context.Lancamentos
                                 .OrderBy(l => l.DataVencimento)
                                 .ToListAsync();
        }

        public async Task<LancamentoModel?> GetByIdAsync(int id)
        {
            await using var context = _dbFactory.CreateDbContext();
            // Inclui o Processo relacionado para que possamos mostrar o Número CNJ no Dashboard
            return await context.Lancamentos
                                 .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task AddAsync(LancamentoModel lancamento)
        {
            // Adiciona um novo lançamento
            await using var context = _dbFactory.CreateDbContext();
            // Inclui o Processo relacionado para que possamos mostrar o Número CNJ no Dashboard            
            context.Lancamentos.Add(lancamento);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LancamentoModel lancamento)
        {
            // Atualiza um lançamento existente
            await using var context = _dbFactory.CreateDbContext();
            context.Lancamentos.Update(lancamento);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            // Remove o lançamento pelo ID
            await using var context = _dbFactory.CreateDbContext();
            var lancamento = await context.Lancamentos.FindAsync(id);
            if (lancamento != null)
            {
                context.Lancamentos.Remove(lancamento);
                await context.SaveChangesAsync();
            }
        }

    }
}