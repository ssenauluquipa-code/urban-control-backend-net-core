using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrbanControl.Backend.Models
{
    public class Manzana
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string CodigoManzana { get; set; } = string.Empty; // Ej: "Mzn A"

        public string? Geometria { get; set; } // Contorno de la manzana para el mapa

        // Relación con Proyecto
        public Guid ProyectoId { get; set; }
        [ForeignKey("ProyectoId")]
        public Proyecto? Proyecto { get; set; }

        // Relación: Una manzana tiene muchos lotes
        public ICollection<Lote> Lotes { get; set; } = new List<Lote>();
    }
}
