using Microsoft.EntityFrameworkCore;
using UrbanControl.Backend.Data;
using UrbanControl.Backend.DTOs;
using UrbanControl.Backend.Models;

namespace UrbanControl.Backend.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly ApplicationDbContext _context;
        public EmpresaService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<EmpresaConfig> GetEmpresaConfigAsync()
        {
            return await _context.EmpresaConfigs.FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateEmpresaConfigAsync(EmpresaConfigDto dto)
        {
            var config = await _context.EmpresaConfigs.FirstOrDefaultAsync();
            if (config == null) return false;

            // Actualizamos los campos
            config.NombreComercial = dto.NombreComercial;
            config.Nit = dto.Nit;
            config.Direccion = dto.Direccion;
            config.Telefono = dto.Telefono;
            config.Email = dto.Email;
            config.DiasReservaVencimiento = dto.DiasReservaVencimiento;
            config.MonedaSimbolo = dto.MonedaSimbolo;
            config.FechaActualizacion = DateTime.Now;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
