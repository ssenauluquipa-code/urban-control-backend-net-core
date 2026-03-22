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
    public class ReservaController : ControllerBase
    {
        private readonly IReservaService _reservaService;
        public ReservaController(IReservaService reservaService) => _reservaService = reservaService;

        [HttpPost]
        public async Task<IActionResult> Post(ReservaDto dto)
        {
            // Extraer el ID del usuario del Token (Claim NameIdentifier)
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null) return Unauthorized();

            var userId = Guid.Parse(userIdClaim);

            var resultado = await _reservaService.CrearReservaAsync(dto, userId);
            if (resultado == null) return BadRequest("El lote no está disponible o no existe.");

            return Ok(resultado);
        }
    }
}
