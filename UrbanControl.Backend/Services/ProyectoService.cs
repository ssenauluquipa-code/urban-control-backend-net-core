using Microsoft.EntityFrameworkCore;
using UrbanControl.Backend.Data;
using UrbanControl.Backend.DTOs;
using UrbanControl.Backend.DTOs.proyecto;
using UrbanControl.Backend.Models;

namespace UrbanControl.Backend.Services
{
    public class ProyectoService : IProyectoService
    {
        private readonly ApplicationDbContext _context;
        public ProyectoService(ApplicationDbContext context) => _context = context;

        public async Task<List<Proyecto>> GetAllAsync() => await _context.Proyectos
            .Include(p => p.Lotes).ToListAsync();

        public async Task<Proyecto> CreateAsync(ProyectoDto dto)
        {
            var proyecto = new Proyecto
            {
                Nombre = dto.Nombre,
                Ubicacion = dto.Ubicacion,
                PrecioBaseM2 = dto.PrecioBaseM2
            };
            _context.Proyectos.Add(proyecto);
            await _context.SaveChangesAsync();
            return proyecto;
        }
        public async Task<ReporteProyectoDto?> GetReporteAsync(Guid proyectoId)
        {
            // Buscamos el proyecto con sus lotes cargados
            var proyecto = await _context.Proyectos
                .Include(p => p.Lotes)
                .FirstOrDefaultAsync(p => p.Id == proyectoId);

            if (proyecto == null) return null;

            // Construimos el DTO con los cálculos
            return new ReporteProyectoDto
            {
                ProyectoNombre = proyecto.Nombre,
                TotalLotes = proyecto.Lotes.Count,
                Disponibles = proyecto.Lotes.Count(l => l.Estado == "Disponible"),
                Reservados = proyecto.Lotes.Count(l => l.Estado == "Reservado"),
                Vendidos = proyecto.Lotes.Count(l => l.Estado == "Vendido"),

                // Cálculo financiero: Suma de (Superficie * PrecioBase) de todos sus lotes
                RecaudacionPotencial = proyecto.Lotes.Sum(l => l.SuperficieM2 * proyecto.PrecioBaseM2)
            };
        }

        //lookup para dropdowns
        public async Task<IEnumerable<ProyectoLookupDto>> GetProyectosLookupAsync()
        {
            return await _context.Proyectos.OrderBy(p => p.Nombre).Select(p => new ProyectoLookupDto
            {
                Id = p.Id,
                Name = p.Nombre
            }).ToListAsync();
        }


    }
}
