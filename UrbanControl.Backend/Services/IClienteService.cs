using UrbanControl.Backend.DTOs;
using UrbanControl.Backend.Models;

namespace UrbanControl.Backend.Services
{
    public interface IClienteService
    {
        Task<List<Cliente>> GetAllAsync();
        Task<Cliente> CreateAsync(ClienteDto dto);
        Task<Cliente?> GetByDocumentoAsync(string ci);
    }
}
