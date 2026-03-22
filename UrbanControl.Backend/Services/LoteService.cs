using Microsoft.EntityFrameworkCore;
using UrbanControl.Backend.Data;
using UrbanControl.Backend.DTOs;
using UrbanControl.Backend.Models;

namespace UrbanControl.Backend.Services
{
    public class LoteService : ILoteService
    {
        private readonly ApplicationDbContext _context;
        public LoteService(ApplicationDbContext context) => _context = context;

        public async Task<List<Lote>> GetLotesByProyectoAsync(Guid proyectoId)
        {
            return await _context.Lotes
                .Include(l => l.Proyecto)
                .Where(l => l.ProyectoId == proyectoId)
                .ToListAsync();
        }

        public async Task<Lote> CreateLoteAsync(LoteDto dto)
        {
            var lote = new Lote
            {
                NumeroLote = dto.NumeroLote,
                Manzana = dto.Manzana,
                SuperficieM2 = dto.SuperficieM2,
                ProyectoId = dto.ProyectoId,
                Estado = "Disponible" // Estado inicial por defecto
            };

            _context.Lotes.Add(lote);
            await _context.SaveChangesAsync();
            return lote;
        }

        public async Task<bool> CambiarEstadoAsync(Guid loteId, string nuevoEstado)
        {
            var lote = await _context.Lotes.FindAsync(loteId);
            if (lote == null) return false;

            lote.Estado = nuevoEstado;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
