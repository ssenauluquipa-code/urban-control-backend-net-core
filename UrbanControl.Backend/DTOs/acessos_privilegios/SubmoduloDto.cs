namespace UrbanControl.Backend.DTOs.acessos_privilegios
{
    public class SubmoduloDto
    {
        public int SubmoduloId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public List<PermisoAccionDto> Acciones { get; set; } = new();
    }
}
