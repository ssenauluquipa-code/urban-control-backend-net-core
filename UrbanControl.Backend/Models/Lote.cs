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

        [Column(TypeName = "decimal(18,2)")]
        public decimal SuperficieM2 { get; set; }

        // 💡 Usamos el Enum que creamos anteriormente
        public EstadoLote Estado { get; set; } = EstadoLote.Disponible;

        public string? Geometria { get; set; }
        public string? MapCode { get; set; }

        public Guid ManzanaId { get; set; }
        [ForeignKey("ManzanaId")]
        public Manzana? Manzana { get; set; }
    }
}
