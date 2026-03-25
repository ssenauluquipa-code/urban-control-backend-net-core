using Microsoft.EntityFrameworkCore;
using System;
using UrbanControl.Backend.Data;
using UrbanControl.Backend.DTOs.acessos_privilegios;
using UrbanControl.Backend.Interfaces;
using UrbanControl.Backend.Models.access_privilegios;

namespace UrbanControl.Backend.Services
{
    public class PermisosService : IPermisosService
    {
        private readonly ApplicationDbContext _context;

        public PermisosService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PermisoMatrizDto>> GetMatrizPorRolAsync(int rolId)
        {
            // Traemos toda la estructura de navegación
            var modulos = await _context.Modulos
                .Include(m => m.Submodulos)
                    .ThenInclude(s => s.Capacidades)
                        .ThenInclude(c => c.TipoPermiso)
                .OrderBy(m => m.Orden)
                .ToListAsync();

            // Traemos lo que el rol ya tiene marcado
            var permisosActuales = await _context.PermisosRoles
                .Where(p => p.RolId == rolId)
                .ToListAsync();

            // Traemos todos los tipos de permisos posibles (Ver, Crear, etc)
            var todosLosTipos = await _context.TipoPermisos.OrderBy(t => t.Id).ToListAsync();

            return modulos.Select(m => new PermisoMatrizDto
            {
                ModuloId = m.Id,
                ModuloNombre = m.Nombre,
                Submodulos = m.Submodulos.Select(s => new SubmoduloDto
                {
                    SubmoduloId = s.Id,
                    Nombre = s.Nombre,
                    Acciones = todosLosTipos.Select(tp => {
                        var capacidad = s.Capacidades.FirstOrDefault(c => c.TipoPermisoId == tp.Id);
                        return new PermisoAccionDto
                        {
                            CapacidadId = capacidad?.Id ?? 0,
                            NombrePermiso = tp.Nombre,
                            Slug = tp.Slug,
                            EsHabilitado = capacidad != null,
                            Concedido = capacidad != null && permisosActuales.Any(pa => pa.CapacidadSubmoduloId == capacidad.Id && pa.Concedido)
                        };
                    }).ToList()
                }).ToList()
            }).ToList();
        }

        public async Task<bool> GuardarPermisosAsync(List<PermisoRolUpdateDto> permisosDto)
        {
            foreach (var item in permisosDto)
            {
                var permiso = await _context.PermisosRoles
                    .FirstOrDefaultAsync(pr => pr.RolId == item.RolId && pr.CapacidadSubmoduloId == item.CapacidadId);

                if (permiso != null)
                {
                    permiso.Concedido = item.Concedido;
                }
                else if (item.Concedido) // Solo creamos si es true para no llenar de basura la DB
                {
                    _context.PermisosRoles.Add(new PermisoRol
                    {
                        RolId = item.RolId,
                        CapacidadSubmoduloId = item.CapacidadId,
                        Concedido = true
                    });
                }
            }
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
