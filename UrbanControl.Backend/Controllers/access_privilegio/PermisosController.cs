using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrbanControl.Backend.DTOs.acessos_privilegios;
using UrbanControl.Backend.Interfaces;

namespace UrbanControl.Backend.Controllers.access_privilegio
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisosController : ControllerBase
    {
        private readonly IPermisosService _permisosService;

        public PermisosController(IPermisosService permisosService)
        {
            _permisosService = permisosService;
        }

        [HttpGet("matriz/{rolId}")]
        public async Task<IActionResult> GetMatriz(int rolId)
        {
            var matriz = await _permisosService.GetMatrizPorRolAsync(rolId);
            return Ok(matriz);
        }

        [HttpPost("guardar")]
        public async Task<IActionResult> Guardar([FromBody] List<PermisoRolUpdateDto> permisos)
        {
            var resultado = await _permisosService.GuardarPermisosAsync(permisos);
            if (resultado) return Ok(new { message = "Cambios guardados con éxito" });
            return BadRequest("No se pudo guardar la información");
        }
    }
}
