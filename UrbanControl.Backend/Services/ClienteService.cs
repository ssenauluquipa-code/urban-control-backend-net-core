using Microsoft.EntityFrameworkCore;
using UrbanControl.Backend.Data;
using UrbanControl.Backend.DTOs;
using UrbanControl.Backend.Models;

namespace UrbanControl.Backend.Services
{
    public class ClienteService : IClienteService
    {
        private readonly ApplicationDbContext _context;
        public ClienteService(ApplicationDbContext context) => _context = context;

        public async Task<List<Cliente>> GetAllAsync()
            => await _context.Clientes.OrderByDescending(c => c.FechaRegistro).ToListAsync();

        public async Task<Cliente?> GetByDocumentoAsync(string ci)
            => await _context.Clientes.FirstOrDefaultAsync(c => c.DocumentoIdentidad == ci);

        public async Task<Cliente> CreateAsync(ClienteDto dto)
        {
            var cliente = new Cliente
            {
                NombreCompleto = dto.NombreCompleto,
                DocumentoIdentidad = dto.DocumentoIdentidad,
                Telefono = dto.Telefono,
                Email = dto.Email,
                FechaRegistro = DateTime.UtcNow
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }
    }
}
