namespace UrbanControl.Backend.DTOs
{
    public class ReservaDto
    {
        public Guid LoteId { get; set; }
        public Guid ClienteId { get; set; }
        public decimal MontoReserva { get; set; }
    }
}
