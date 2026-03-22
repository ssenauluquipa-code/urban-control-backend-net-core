namespace UrbanControl.Backend.DTOs
{
    public class ProyectoDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Ubicacion { get; set; } = string.Empty;
        public decimal PrecioBaseM2 { get; set; }
    }
}
