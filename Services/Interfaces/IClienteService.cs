using ProcessoDigital_Server.Data.Model;

namespace ProcessoDigital_Server.Services.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteModel>> GetAllAsync();
        Task<ClienteModel?> GetByIdAsync(int id);
        Task AddAsync(ClienteModel cliente);
        Task UpdateAsync(ClienteModel cliente);
        Task DeleteAsync(int id);
    }
}