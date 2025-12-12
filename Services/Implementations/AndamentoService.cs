using Microsoft.EntityFrameworkCore;
using ProcessoDigital_Server.Services.Interfaces;
using ProcessoDigital_Server.Data.Context;
using ProcessoDigital_Server.Data.Model;



namespace ProcessoDigital_Server.Services.Implementations
{
    public class AndamentoService : IAndamentoService
    {
        private readonly AppDbContext _context;

        public AndamentoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AndamentoModel>> GetAllAndamentosAsync()
        {
            
            // Inclui o Processo relacionado para que possamos mostrar o Número CNJ no Dashboard
            return await _context.Andamentos
                .Include(a => a.Processo)
                    .ThenInclude(p => p.Cliente)
                .ToListAsync();
        }
        public async Task<IEnumerable<AndamentoModel>> GetByProcessoIdAsync(int processoId)
        {
            
            return await _context.Andamentos
                .Include(p => p.Processo)
                .Where(p => p.ProcessoId == processoId)
                .ToListAsync();
        }

        public async Task<AndamentoModel?> GetByIdAsync(int id)
        {
            
            return await _context.Andamentos
                .Include(a => a.Descricao)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(AndamentoModel andamento)
        {
            
            
            _context.Andamentos.Add(andamento);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AndamentoModel andamento)
        {
            
            _context.Andamentos.Update(andamento);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            
            var andamento = await _context.Andamentos.FindAsync(id);
            if (andamento != null)
            {
                _context.Andamentos.Remove(andamento);
                await _context.SaveChangesAsync();
            }
        }
    }
}
