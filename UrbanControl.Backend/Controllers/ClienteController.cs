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
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _clienteService.GetAllAsync());

        [HttpGet("buscar/{ci}")]
        public async Task<IActionResult> GetByCi(string ci)
        {
            var cliente = await _clienteService.GetByDocumentoAsync(ci);
            return cliente == null ? NotFound("Cliente no encontrado") : Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ClienteDto dto)
        {
            // Validación: ¿Ya existe este CI?
            var existe = await _clienteService.GetByDocumentoAsync(dto.DocumentoIdentidad);
            if (existe != null) return BadRequest("Ya existe un cliente con este documento.");

            var nuevo = await _clienteService.CreateAsync(dto);
            return Ok(nuevo);
        }
    }
}
