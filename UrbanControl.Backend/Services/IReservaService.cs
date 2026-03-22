using UrbanControl.Backend.DTOs;
using UrbanControl.Backend.Models;

namespace UrbanControl.Backend.Services
{
    public interface IReservaService
    {
        Task<Reserva?> CrearReservaAsync(ReservaDto dto, Guid usuarioId);
        Task<List<Reserva>> GetReservasActivasAsync();
    }
}
