using Microsoft.EntityFrameworkCore;
using ProcessoDigital_Server.Data.Context;
using ProcessoDigital_Server.Data.Model;
using ProcessoDigital_Server.Services.Interfaces;

namespace ProcessoDigital_Server.Services.Implementations
{
    public class LancamentoService : ILancamentoService
    {
        private readonly AppDbContext _context;

        public LancamentoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LancamentoModel>> GetAllAsync()
        {
            return await _context.Lancamentos
                                 .OrderBy(l => l.DataVencimento)
                                 .ToListAsync();
        }

        public async Task<LancamentoModel?> GetByIdAsync(int id)
        {
            // Inclui o Processo relacionado para que possamos mostrar o Número CNJ no Dashboard
            return await _context.Lancamentos
                                 .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task AddAsync(LancamentoModel lancamento)
        {
            // Inclui o Processo relacionado para que possamos mostrar o Número CNJ no Dashboard            
            _context.Lancamentos.Add(lancamento);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LancamentoModel lancamento)
        {
            // Atualiza um lançamento existente
            _context.Lancamentos.Update(lancamento);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            // Remove o lançamento pelo ID            
            var lancamento = await _context.Lancamentos.FindAsync(id);
            if (lancamento != null)
            {
                _context.Lancamentos.Remove(lancamento);
                await _context.SaveChangesAsync();
            }
        }

    }
}