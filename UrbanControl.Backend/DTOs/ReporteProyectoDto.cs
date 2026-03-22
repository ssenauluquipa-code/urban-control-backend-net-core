namespace UrbanControl.Backend.DTOs
{
    public class ReporteProyectoDto
    {
        public string ProyectoNombre { get; set; } = string.Empty;
        public int TotalLotes { get; set; }
        public int Disponibles { get; set; }
        public int Reservados { get; set; }
        public int Vendidos { get; set; }
        public decimal RecaudacionPotencial { get; set; } // Suma de precios de todos los lotes
    }
}
