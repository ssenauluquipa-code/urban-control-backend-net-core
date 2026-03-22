using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrbanControl.Backend.DTOs;
using UrbanControl.Backend.Services;

namespace UrbanControl.Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoteController : ControllerBase
    {
        private readonly ILoteService _loteService;

        public LoteController(ILoteService loteService)
        {
            _loteService = loteService;
        }

        [HttpGet("proyecto/{proyectoId}")]
        public async Task<IActionResult> GetByProyecto(Guid proyectoId)
        {
            return Ok(await _loteService.GetLotesByProyectoAsync(proyectoId));
        }

        [HttpPost]
        public async Task<IActionResult> Create(LoteDto dto)
        {
            var nuevoLote = await _loteService.CreateLoteAsync(dto);
            return Ok(nuevoLote);
        }

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> UpdateEstado(Guid id, [FromBody] string nuevoEstado)
        {
            var resultado = await _loteService.CambiarEstadoAsync(id, nuevoEstado);
            if (!resultado) return NotFound("Lote no encontrado");
            return Ok("Estado actualizado");
        }
    }
}
