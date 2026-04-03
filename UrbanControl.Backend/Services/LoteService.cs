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
                .Include(l => l.Manzana)
                .Where(l => l.Manzana.ProyectoId == proyectoId && l.Estado != EstadoLote.Eliminado)
            .ToListAsync();
        }

        public async Task<Lote> CreateLoteAsync(LoteDto dto)
        {
            var lote = new Lote
            {
                NumeroLote = dto.NumeroLote,
                SuperficieM2 = dto.SuperficieM2,
                Estado = dto.Estado,
                ManzanaId = dto.ManzanaId, // Asignamos a la manzana
                Geometria = dto.Geometria,
                MapCode = dto.MapCode
            };

            _context.Lotes.Add(lote);
            await _context.SaveChangesAsync();
            return lote;
        }

        public async Task<bool> CambiarEstadoAsync(Guid loteId, EstadoLote nuevoEstado)
        {
            var lote = await _context.Lotes.FindAsync(loteId);
            if (lote == null) return false;

            lote.Estado = nuevoEstado;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateLoteAsync(Guid id, LoteDto dto)
        {
            var lote = await _context.Lotes.FindAsync(id);

            if (lote == null) return false;

            lote.NumeroLote = dto.NumeroLote;
            lote.SuperficieM2 = dto.SuperficieM2;
            lote.Estado = dto.Estado;
            lote.ManzanaId = dto.ManzanaId;
            lote.Geometria = dto.Geometria;
            lote.MapCode = dto.MapCode;
            _context.Lotes.Update(lote);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Lote> GetByIdAsync(Guid loteId)
        {
            return await _context.Lotes.Include(l => l.Manzana).FirstOrDefaultAsync(l => l.Id == loteId);
        }

    }
}
