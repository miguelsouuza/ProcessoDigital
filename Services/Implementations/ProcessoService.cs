using Microsoft.EntityFrameworkCore;
using ProcessoDigital_Server.Services.Interfaces;
using ProcessoDigital_Server.Data.Context;
using ProcessoDigital_Server.Data.Model;

namespace ProcessoDigital_Server.Services.Implementations
{
    public class ProcessoService : IProcessoService
    {
        private readonly AppDbContext _context;

        public ProcessoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProcessoModel>> GetAllAsync()
        {
            return await _context.Processos
                .Include(p => p.Cliente)
                .ToListAsync();
        }
        public async Task<IEnumerable<ProcessoModel>> GetByClienteIdAsync(int clienteId)
        {
            return await _context.Processos
                .Include(p => p.Cliente)
                .Where(p => p.ClienteId == clienteId)
                .ToListAsync();
        }

        public async Task<ProcessoModel?> GetByIdAsync(int id)
        {
            return await _context.Processos
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(ProcessoModel processo)
        {
            _context.Processos.Add(processo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProcessoModel processo)
        {
            _context.Processos.Update(processo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var processo = await _context.Processos.FindAsync(id);
            if (processo != null)
            {
                _context.Processos.Remove(processo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
