using UrbanControl.Backend.DTOs;
using UrbanControl.Backend.Models;

namespace UrbanControl.Backend.Services
{
    public interface ILoteService
    {
        Task<List<Lote>> GetLotesByProyectoAsync(Guid proyectoId);
        Task<Lote> GetByIdAsync(Guid loteId);
        Task<Lote> CreateLoteAsync(LoteDto dto);
        Task<bool> CambiarEstadoAsync(Guid loteId, EstadoLote nuevoEstado);

        Task<bool> UpdateLoteAsync(Guid id, LoteDto loteDto);
    }
}
