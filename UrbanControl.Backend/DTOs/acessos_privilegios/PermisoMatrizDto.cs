namespace UrbanControl.Backend.DTOs.acessos_privilegios
{
    public class PermisoMatrizDto
    {
        public int ModuloId { get; set; }
        public string ModuloNombre { get; set; } = string.Empty;
        public List<SubmoduloDto> Submodulos { get; set; } = new();
    }
}
