using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrbanControl.Backend.Data;
using UrbanControl.Backend.DTOs;
using UrbanControl.Backend.Models;
using UrbanControl.Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UrbanControl.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthService _authService;

        public AuthController(ApplicationDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDto model)
        {
            if(await _context.Users.AnyAsync(u => u.Email == model.Email))
            {
                return BadRequest("El correo ya esta registrado");
            }

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                PasswordHash = _authService.HashPassword(model.Password),
                Role = "admin"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Usuario creado con exito" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null || !_authService.VerifyPassword(model.Password, user.PasswordHash)){
                return Unauthorized("Correo o contraseña incorrecta");
            }
            var token = _authService.GenerarTokenj(user);
            return Ok(new
            {
                Token = token,
                User = new { user.Name, user.Email, user.Role }
            });
        }
    }
}
