using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrbanControl.Backend.DTOs;
using UrbanControl.Backend.Services;

namespace UrbanControl.Backend.Controllers
{
    [Authorize] // Seguridad activa: requiere JWT
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoController : ControllerBase
    {
        private readonly IProyectoService _proyectoService;

        // Inyección de dependencias (D de SOLID)
        public ProyectoController(IProyectoService proyectoService)
        {
            _proyectoService = proyectoService;
        }

        // GET: api/Proyecto
        [HttpGet]
        public async Task<IActionResult> GetProyectos()
        {
            var proyectos = await _proyectoService.GetAllAsync();
            return Ok(proyectos);
        }

        // POST: api/Proyecto
        [HttpPost]
        public async Task<IActionResult> CreateProyecto([FromBody] ProyectoDto proyectoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuevoProyecto = await _proyectoService.CreateAsync(proyectoDto);

            return CreatedAtAction(nameof(GetProyectos), new { id = nuevoProyecto.Id }, nuevoProyecto);
        }
        // NUEVO ENDPOINT PARA EL REPORTE
        [HttpGet("{id}/reporte")]
        public async Task<IActionResult> GetReporte(Guid id)
        {
            var reporte = await _proyectoService.GetReporteAsync(id);

            if (reporte == null)
                return NotFound($"No se encontró la urbanización con ID: {id}");

            return Ok(reporte);
        }
    }
}
