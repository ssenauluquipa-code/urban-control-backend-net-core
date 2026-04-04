using System.ComponentModel.DataAnnotations;

namespace UrbanControl.Backend.Models
{
    public class EmpresaConfig
    {
        [Key]
        public Guid Id { get; set; }

        // Datos Legales
        [Required, StringLength(100)]
        public string NombreComercial { get; set; }
        [Required, StringLength(150)]
        public string RazonSocial { get; set; }
        [Required, StringLength(20)]
        public string Nit { get; set; }

        // Contacto
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        // Reglas de Negocio (Basado en el documento técnico)
        public int DiasReservaVencimiento { get; set; } // El CRON Job del Mes 3 usará esto
        public string MonedaSimbolo { get; set; } // "Bs" o "$"

        public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;
    }
}
