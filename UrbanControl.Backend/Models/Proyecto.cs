using System.ComponentModel.DataAnnotations;

namespace UrbanControl.Backend.Models
{
    public class Proyecto
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Nombre { get; set; } = string.Empty;
        public string Ubicacion { get; set; } = string.Empty;
        public decimal PrecioBaseM2 { get; set; }

        // Relación: Un proyecto tiene muchos lotes
        public ICollection<Lote> Lotes { get; set; } = new List<Lote>();
    }
}
