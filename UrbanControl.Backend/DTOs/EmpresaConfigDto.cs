namespace UrbanControl.Backend.DTOs
{
    public class EmpresaConfigDto
    {
        public string NombreComercial { get; set; }
        public string Nit { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int DiasReservaVencimiento { get; set; }
        public string MonedaSimbolo { get; set; }
    }
}
