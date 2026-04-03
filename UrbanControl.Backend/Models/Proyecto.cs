using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrbanControl.Backend.Models
{
    public class Proyecto
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Nombre { get; set; } = string.Empty;
        public string Ubicacion { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioBaseM2 { get; set; }

        public string? UrlImagenPlano { get; set; }
        public string? ConfigMapa { get; set; } // JSON (zoom, centro)

        // Relación: Un proyecto tiene muchas manzanas
        public ICollection<Manzana> Manzanas { get; set; } = new List<Manzana>();
    }
}
