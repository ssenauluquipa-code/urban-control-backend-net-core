using System.ComponentModel.DataAnnotations;

namespace UrbanControl.Backend.Models
{
    public class Cliente
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required]
        public string DocumentoIdentidad { get; set; } = string.Empty; // CI o NIT

        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;

        // Fecha en que se registró en el sistema
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}
