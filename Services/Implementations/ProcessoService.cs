using Microsoft.EntityFrameworkCore;
using ProcessoDigital_Server.Services.Interfaces;
using ProcessoDigital_Server.Data.Context;
using ProcessoDigital_Server.Data.Model;

namespace ProcessoDigital_Server.Services.Implementations
{
    public class ProcessoService : IProcessoService
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;

        public ProcessoService(IDbContextFactory<AppDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<IEnumerable<ProcessoModel>> GetAllAsync()
        {
            await using var context = _dbFactory.CreateDbContext();
            return await context.Processos
                .Include(p => p.Cliente)
                .ToListAsync();
        }
        public async Task<IEnumerable<ProcessoModel>> GetByClienteIdAsync(int clienteId)
        {
            await using var context = _dbFactory.CreateDbContext();
            return await context.Processos
                .Include(p => p.Cliente)
                .Where(p => p.ClienteId == clienteId)
                .ToListAsync();
        }

        public async Task<ProcessoModel?> GetByIdAsync(int id)
        {
            await using var context = _dbFactory.CreateDbContext();
            return await context.Processos
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(ProcessoModel processo)
        {
            await using var context = _dbFactory.CreateDbContext();
            context.Processos.Add(processo);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProcessoModel processo)
        {
            await using var context = _dbFactory.CreateDbContext();
            context.Processos.Update(processo);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await using var context = _dbFactory.CreateDbContext();
            var processo = await context.Processos.FindAsync(id);
            if (processo != null)
            {
                context.Processos.Remove(processo);
                await context.SaveChangesAsync();
            }
        }
    }
}
