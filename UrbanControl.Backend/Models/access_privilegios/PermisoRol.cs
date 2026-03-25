namespace UrbanControl.Backend.Models.access_privilegios
{
    public class PermisoRol
    {
        public int Id { get; set; }
        public int RolId { get; set; }
        public int CapacidadSubmoduloId { get; set; }
        public bool Concedido { get; set; } = false;

        public Rol Rol { get; set; } = null!;
        public CapacidadSubmodulo Capacidad { get; set; } = null!;
    }
}
