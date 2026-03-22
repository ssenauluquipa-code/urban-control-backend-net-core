using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrbanControl.Backend.Models
{
    public class Reserva
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid LoteId { get; set; }
        [ForeignKey("LoteId")]
        public Lote? Lote { get; set; }

        [Required]
        public Guid ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }

        [Required]
        public Guid UsuarioId { get; set; } // El vendedor que realizó la reserva
        [ForeignKey("UsuarioId")]
        public User? Usuario { get; set; }

        [Required]
        public decimal MontoReserva { get; set; } // Pago inicial para bloquear el lote

        public DateTime FechaReserva { get; set; } = DateTime.UtcNow;
        public DateTime FechaVencimiento { get; set; } = DateTime.UtcNow.AddDays(7); // Ejemplo: 7 días para completar el pago

        public string Estado { get; set; } = "Activa"; // Activa, Completada, Cancelada
    }
}
