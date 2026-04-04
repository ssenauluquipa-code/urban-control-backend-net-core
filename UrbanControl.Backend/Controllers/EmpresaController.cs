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
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;

        public EmpresaController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var config = await _empresaService.GetEmpresaConfigAsync();
            return config != null ? Ok(config) : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> Update(EmpresaConfigDto dto)
        {
            var result = await _empresaService.UpdateEmpresaConfigAsync(dto);
            return result ? NoContent(): BadRequest("No se pudo actualizar la configuración de la empresa.");
        }
    }
}
