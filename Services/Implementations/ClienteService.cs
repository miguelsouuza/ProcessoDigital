using Microsoft.EntityFrameworkCore;
using ProcessoDigital_Server.Services.Interfaces;
using ProcessoDigital_Server.Data.Context;
using ProcessoDigital_Server.Data.Model;

namespace ProcessoDigital_Server.Services.Implementations
{
    public class ClienteService : IClienteService
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClienteModel>> GetAllAsync()
        {
            return await _context.Clientes
                .Include(c => c.Processos)
                .ToListAsync();
        }

        public async Task<ClienteModel?> GetByIdAsync(int id)
        {
            return await _context.Clientes
                .Include(c => c.Processos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(ClienteModel cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClienteModel cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }
    }
}
