using UrbanControl.Backend.DTOs;
using UrbanControl.Backend.Models;

namespace UrbanControl.Backend.Services
{
    public interface IEmpresaService
    {
        Task<EmpresaConfig> GetEmpresaConfigAsync();
        Task<bool> UpdateEmpresaConfigAsync(EmpresaConfigDto updatedConfig); 
    }
}
