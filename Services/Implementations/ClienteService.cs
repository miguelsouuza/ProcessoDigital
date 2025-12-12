using Microsoft.EntityFrameworkCore;
using ProcessoDigital_Server.Services.Interfaces;
using ProcessoDigital_Server.Data.Context;
using ProcessoDigital_Server.Data.Model;

namespace ProcessoDigital_Server.Services.Implementations
{
    public class ClienteService : IClienteService
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;

        public ClienteService(IDbContextFactory<AppDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<IEnumerable<ClienteModel>> GetAllAsync()
        {
            await using var context = _dbFactory.CreateDbContext();
            // Inclui a lista de Processos para visualização de vinculo
            return await context.Clientes
                .Include(c => c.Processos)
                .ToListAsync();
        }

        public async Task<ClienteModel?> GetByIdAsync(int id)
        {
            await using var context = _dbFactory.CreateDbContext();
            return await context.Clientes
                .Include(c => c.Processos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(ClienteModel cliente)
        {
            await using var context = _dbFactory.CreateDbContext();
            context.Clientes.Add(cliente);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClienteModel cliente)
        {
            await using var context = _dbFactory.CreateDbContext();
            context.Clientes.Update(cliente);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await using var context = _dbFactory.CreateDbContext();
            var cliente = await context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                context.Clientes.Remove(cliente);
                await context.SaveChangesAsync();
            }
        }
    }
}
