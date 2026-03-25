namespace UrbanControl.Backend.Models.access_privilegios
{
    public class CapacidadSubmodulo
    {
        public int Id { get; set; }
        public int SubmoduloId { get; set; }
        public int TipoPermisoId { get; set; }

        public Submodulo Submodulo { get; set; } = null!;
        public TipoPermiso TipoPermiso { get; set; } = null!;
        public ICollection<PermisoRol> PermisosRoles { get; set; } = new List<PermisoRol>();
    }
}
