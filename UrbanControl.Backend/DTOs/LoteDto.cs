using UrbanControl.Backend.Models;

namespace UrbanControl.Backend.DTOs
{
    public class LoteDto
    {
        public Guid? Id { get; set; }
        public string NumeroLote { get; set; } = string.Empty;
        public decimal SuperficieM2 { get; set; }
        public EstadoLote Estado { get; set; } = EstadoLote.Disponible;
        public string? Geometria { get; set; }
        public string? MapCode { get; set; }

        // Relación obligatoria
        public Guid ManzanaId { get; set; }
        public string? NombreManzana { get; set; } // Opcional: para mostrar en tablas
    }
}
