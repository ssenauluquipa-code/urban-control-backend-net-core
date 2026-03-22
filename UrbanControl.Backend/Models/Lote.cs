using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrbanControl.Backend.Models
{
    public class Lote
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string NumeroLote { get; set; } = string.Empty;
        [Required]
        public string Manzana { get; set; } = string.Empty;
        public decimal SuperficieM2 { get; set; }
        public string Estado { get; set; } = "Disponible"; // Disponible, Reservado, Vendido

        // Clave foránea hacia Proyecto
        public Guid ProyectoId { get; set; }
        [ForeignKey("ProyectoId")]
        public Proyecto? Proyecto { get; set; }
    }
}
