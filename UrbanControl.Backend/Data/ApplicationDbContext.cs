using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using UrbanControl.Backend.Models;
using UrbanControl.Backend.Models.access_privilegios;

namespace UrbanControl.Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        //
        public DbSet<Modulo> Modulos { get; set; }
        public DbSet<Submodulo> Submodulos { get; set; }
        public DbSet<TipoPermiso> TipoPermisos { get; set; }
        public DbSet<CapacidadSubmodulo> CapacidadSubmodulos { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<PermisoRol> PermisosRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Claves Únicas para evitar duplicidad de lógica
            modelBuilder.Entity<CapacidadSubmodulo>()
                .HasIndex(c => new { c.SubmoduloId, c.TipoPermisoId }).IsUnique();

            modelBuilder.Entity<PermisoRol>()
                .HasIndex(p => new { p.RolId, p.CapacidadSubmoduloId }).IsUnique();

            // --- SEED DATA ---

            // 1. Tipos de Permisos
            modelBuilder.Entity<TipoPermiso>().HasData(
                new TipoPermiso { Id = 1, Nombre = "Ver", Slug = "view" },
                new TipoPermiso { Id = 2, Nombre = "Crear", Slug = "create" },
                new TipoPermiso { Id = 3, Nombre = "Editar", Slug = "edit" },
                new TipoPermiso { Id = 4, Nombre = "Eliminar", Slug = "delete" },
                new TipoPermiso { Id = 5, Nombre = "Imprimir PDF", Slug = "pdf" }
            );

            // 2. Módulos
            modelBuilder.Entity<Modulo>().HasData(
                new Modulo { Id = 1, Nombre = "Inventario", Icono = "pi pi-box", Orden = 1 },
                new Modulo { Id = 2, Nombre = "Ventas", Icono = "pi pi-shopping-cart", Orden = 2 }
            );

            // 3. Submódulos
            modelBuilder.Entity<Submodulo>().HasData(
                new Submodulo { Id = 1, ModuloId = 1, Nombre = "Proyectos", RutaAngular = "/proyectos" },
                new Submodulo { Id = 2, ModuloId = 1, Nombre = "Lotes", RutaAngular = "/lotes" },
                new Submodulo { Id = 3, ModuloId = 2, Nombre = "Reservas", RutaAngular = "/reservas" }
            );

            // 4. Capacidades (Definimos qué se puede hacer en cada pantalla)
            modelBuilder.Entity<CapacidadSubmodulo>().HasData(
                // Proyectos: Ver, Crear, Editar
                new CapacidadSubmodulo { Id = 1, SubmoduloId = 1, TipoPermisoId = 1 },
                new CapacidadSubmodulo { Id = 2, SubmoduloId = 1, TipoPermisoId = 2 },
                new CapacidadSubmodulo { Id = 3, SubmoduloId = 1, TipoPermisoId = 3 },
                // Lotes: Ver, Crear, Editar, Eliminar
                new CapacidadSubmodulo { Id = 4, SubmoduloId = 2, TipoPermisoId = 1 },
                new CapacidadSubmodulo { Id = 5, SubmoduloId = 2, TipoPermisoId = 2 },
                new CapacidadSubmodulo { Id = 6, SubmoduloId = 2, TipoPermisoId = 3 },
                new CapacidadSubmodulo { Id = 7, SubmoduloId = 2, TipoPermisoId = 4 },
                // Reservas: Ver, Crear, PDF (No se permite 'Eliminar' por seguridad contable)
                new CapacidadSubmodulo { Id = 8, SubmoduloId = 3, TipoPermisoId = 1 },
                new CapacidadSubmodulo { Id = 9, SubmoduloId = 3, TipoPermisoId = 2 },
                new CapacidadSubmodulo { Id = 10, SubmoduloId = 3, TipoPermisoId = 5 }
            );

            // 5. Roles iniciales
            modelBuilder.Entity<Rol>().HasData(
                new Rol { Id = 1, Nombre = "Administrador", Descripcion = "Acceso total" },
                new Rol { Id = 2, Nombre = "Vendedor", Descripcion = "Solo ventas y clientes" }
            );
        }
    }
}