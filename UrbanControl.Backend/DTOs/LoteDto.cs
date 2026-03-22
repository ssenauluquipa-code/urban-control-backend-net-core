namespace UrbanControl.Backend.DTOs
{
    public class LoteDto
    {
        public string NumeroLote { get; set; } = string.Empty;
        public string Manzana { get; set; } = string.Empty;
        public decimal SuperficieM2 { get; set; }
        public Guid ProyectoId { get; set; } // Para saber a qué urbanización pertenece
    }
}
