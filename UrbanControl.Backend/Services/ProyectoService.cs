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
            .Include(p => p.Manzanas).ToListAsync();

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
                .Include(p => p.Manzanas)
                .FirstOrDefaultAsync(p => p.Id == proyectoId);

            if (proyecto == null) return null;

            // CORRECCIÓN: Obtenemos la lista plana de todos los lotes de todas las manzanas
            var todosLosLotes = proyecto.Manzanas.SelectMany(m => m.Lotes).ToList();
            // Construimos el DTO con los cálculos
            return new ReporteProyectoDto
            {
                ProyectoNombre = proyecto.Nombre,
                TotalLotes = todosLosLotes.Count,

                // CORRECCIÓN: Usamos el Enum para filtrar
                Disponibles = todosLosLotes.Count(l => l.Estado == EstadoLote.Disponible),
                Reservados = todosLosLotes.Count(l => l.Estado == EstadoLote.Reservado),
                Vendidos = todosLosLotes.Count(l => l.Estado == EstadoLote.Vendido),

                RecaudacionPotencial = todosLosLotes.Sum(l => l.SuperficieM2 * proyecto.PrecioBaseM2)
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
