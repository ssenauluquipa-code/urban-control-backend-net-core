using UrbanControl.Backend.DTOs.acessos_privilegios;

namespace UrbanControl.Backend.Interfaces
{
    public interface IPermisosService
    {
        Task<List<PermisoMatrizDto>> GetMatrizPorRolAsync(int rolId);
        Task<bool> GuardarPermisosAsync(List<PermisoRolUpdateDto> permisosDto);
    }
}
