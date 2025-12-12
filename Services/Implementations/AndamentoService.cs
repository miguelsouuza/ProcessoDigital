using Microsoft.EntityFrameworkCore;
using ProcessoDigital_Server.Services.Interfaces;
using ProcessoDigital_Server.Data.Context;
using ProcessoDigital_Server.Data.Model;



namespace ProcessoDigital_Server.Services.Implementations
{
    public class AndamentoService : IAndamentoService
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;

        public AndamentoService(IDbContextFactory<AppDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<IEnumerable<AndamentoModel>> GetAllAndamentosAsync()
        {
            await using var context = _dbFactory.CreateDbContext();
            // Inclui o Processo relacionado para que possamos mostrar o Número CNJ no Dashboard
            return await context.Andamentos
                .Include(a => a.Processo)
                    .ThenInclude(p => p.Cliente)
                .ToListAsync();
        }
        public async Task<IEnumerable<AndamentoModel>> GetByProcessoIdAsync(int processoId)
        {
            await using var context = _dbFactory.CreateDbContext();
            return await context.Andamentos
                .Include(p => p.Processo)
                .Where(p => p.ProcessoId == processoId)
                .ToListAsync();
        }

        public async Task<AndamentoModel?> GetByIdAsync(int id)
        {
            await using var context = _dbFactory.CreateDbContext();
            return await context.Andamentos
                .Include(a => a.Descricao)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(AndamentoModel andamento)
        {
            await using var context = _dbFactory.CreateDbContext();
            
            context.Andamentos.Add(andamento);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AndamentoModel andamento)
        {
            await using var context = _dbFactory.CreateDbContext();
            context.Andamentos.Update(andamento);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await using var context = _dbFactory.CreateDbContext();
            var andamento = await context.Andamentos.FindAsync(id);
            if (andamento != null)
            {
                context.Andamentos.Remove(andamento);
                await context.SaveChangesAsync();
            }
        }
    }
}
