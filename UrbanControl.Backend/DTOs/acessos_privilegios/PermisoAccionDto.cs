namespace UrbanControl.Backend.DTOs.acessos_privilegios
{
    public class PermisoAccionDto
    {
        public int CapacidadId { get; set; }
        public string NombrePermiso { get; set; } = string.Empty; // "Crear", "Ver", etc.
        public string Slug { get; set; } = string.Empty;
        public bool Concedido { get; set; } // El valor del checkbox
        public bool EsHabilitado { get; set; } // Si la celda debe estar bloqueada o no
    }
}
