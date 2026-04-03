using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrbanControl.Backend.DTOs;
using UrbanControl.Backend.DTOs.lotes;
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
        public async Task<IActionResult> UpdateEstado(Guid id, [FromBody] LoteEstadoDTO dto)
        {
            var resultado = await _loteService.CambiarEstadoAsync(id, dto.Estado);
            if (!resultado) return NotFound("Lote no encontrado");
            return Ok(new { message = "Estado actualizado" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, LoteDto loteDto)
        {
            if (loteDto == null) return BadRequest("Los datos del lote son nulos");
            try
            {
                var actualizado = await _loteService.UpdateLoteAsync(id, loteDto);
                if (!actualizado)
                {
                    return NotFound($"No se encontró el lote con el ID {id} para actualizar.");
                }
                return Ok(new { message = "Lote actualizado existosamente" });
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if you have a logging mechanism
                return StatusCode(500, $"Ocurrió un error al actualizar el lote: {ex.Message}");
            }
        }
    }
}
