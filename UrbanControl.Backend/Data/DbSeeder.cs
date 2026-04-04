using Microsoft.EntityFrameworkCore;
using UrbanControl.Backend.Models;

namespace UrbanControl.Backend.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            // --- SECCIÓN 1: CONFIGURACIÓN GLOBAL (EMPRESA) ---
            // Esta sección debe correr siempre que no exista la empresa, 
            // independientemente de si hay proyectos o no.
            if (!await context.EmpresaConfigs.AnyAsync())
            {
                context.EmpresaConfigs.Add(new EmpresaConfig
                {
                    Id = Guid.NewGuid(),
                    NombreComercial = "Urbanización UrbanControl",
                    RazonSocial = "UrbanControl S.A.",
                    Nit = "123456789",
                    Direccion = "Av. Principal Nro 123", // Dato básico inicial
                    Telefono = "70000000",
                    Email = "contacto@urbancontrol.com",
                    DiasReservaVencimiento = 3,
                    MonedaSimbolo = "Bs",
                    FechaActualizacion = DateTime.Now
                });

                // Guardamos la empresa primero para asegurar que el sistema tenga reglas
                await context.SaveChangesAsync();
            }

            // --- SECCIÓN 2: INVENTARIO (PROYECTOS, MANZANAS, LOTES) ---
            // 1. Verificación: Solo insertamos si la tabla Proyectos está vacía
            // Esto evita duplicar datos cada vez que reinicias la App
            if (context.Proyectos.Any()) return;

            // 2. Crear Proyecto
            var proyecto = new Proyecto
            {
                Id = Guid.NewGuid(),
                Nombre = "Urbanización El Mollar - Fase 1",
                Ubicacion = "Tarija - Zona Sur",
                PrecioBaseM2 = 50.00m,
            };

            // 3. Crear Manzanas
            var mznA = new Manzana
            {
                Id = Guid.NewGuid(),
                ProyectoId = proyecto.Id,
                CodigoManzana = "Mzn A",
                Geometria = "[[0,0],[0,10],[10,10],[10,0]]"
            };

            var mznB = new Manzana
            {
                Id = Guid.NewGuid(),
                ProyectoId = proyecto.Id,
                CodigoManzana = "Mzn B"
            };

            // 4. Crear Lotes
            var lotes = new List<Lote>
    {
        new Lote {
            Id = Guid.NewGuid(),
            ManzanaId = mznA.Id,
            NumeroLote = "1",
            SuperficieM2 = 300,
            Estado = EstadoLote.Disponible,
            MapCode = "L1-MznA",
            Geometria = "[[10,10],[20,10],[20,20],[10,20]]"
        },
        new Lote {
            Id = Guid.NewGuid(),
            ManzanaId = mznA.Id,
            NumeroLote = "2",
            SuperficieM2 = 350,
            Estado = EstadoLote.Vendido,
            MapCode = "L2-MznA",
            Geometria = "[[20,10],[30,10],[30,20],[20,20]]"
        },
        new Lote {
            Id = Guid.NewGuid(),
            ManzanaId = mznB.Id,
            NumeroLote = "15",
            SuperficieM2 = 400,
            Estado = EstadoLote.Reservado,
            MapCode = "L15-MznB",
            Geometria = "[[10,30],[20,30],[20,40],[10,40]]"
        }
    };

            // 5. Guardar todo
            context.Proyectos.Add(proyecto);
            context.Manzanas.AddRange(mznA, mznB);
            context.Lotes.AddRange(lotes);

            await context.SaveChangesAsync();
        }
    }
}
