using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using UrbanControl.Backend.Models;

namespace UrbanControl.Backend.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {}

        public DbSet<User> Users { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
    }
}
