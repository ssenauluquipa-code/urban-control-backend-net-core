using Microsoft.EntityFrameworkCore;
using UrbanControl.Backend.Data;
using UrbanControl.Backend.DTOs;
using UrbanControl.Backend.Models;

namespace UrbanControl.Backend.Services
{
    public class ReservaService : IReservaService
    {
        private readonly ApplicationDbContext _context;
        public ReservaService(ApplicationDbContext context) => _context = context;

        public async Task<Reserva?> CrearReservaAsync(ReservaDto dto, Guid usuarioId)
        {
            // 1. Buscar el lote y verificar disponibilidad
            var lote = await _context.Lotes.FindAsync(dto.LoteId);
            if (lote == null || lote.Estado != "Disponible") return null;

            // 2. Iniciar transacción (opcional pero recomendado)
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 3. Crear la reserva
                var reserva = new Reserva
                {
                    LoteId = dto.LoteId,
                    ClienteId = dto.ClienteId,
                    UsuarioId = usuarioId,
                    MontoReserva = dto.MontoReserva
                };

                // 4. Actualizar estado del lote
                lote.Estado = "Reservado";

                _context.Reservas.Add(reserva);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return reserva;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<Reserva>> GetReservasActivasAsync()
            => await _context.Reservas.Include(r => r.Lote).Include(r => r.Cliente).ToListAsync();
    }
}
