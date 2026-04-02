using UrbanControl.Backend.DTOs;
using UrbanControl.Backend.DTOs.proyecto;
using UrbanControl.Backend.Models;

namespace UrbanControl.Backend.Services
{
    public interface IProyectoService
    {
        Task<List<Proyecto>> GetAllAsync();
        Task<Proyecto> CreateAsync(ProyectoDto dto);
        // El nuevo contrato para el Reporte de Inventario
        Task<ReporteProyectoDto?> GetReporteAsync(Guid proyectoId);


        //looks
        Task<IEnumerable<ProyectoLookupDto>> GetProyectosLookupAsync();

    }
}
