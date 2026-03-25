using System.ComponentModel.DataAnnotations;

namespace UrbanControl.Backend.Models.access_privilegios
{
    public class Rol
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Nombre { get; set; } = string.Empty; // Ej: "Admin", "Vendedor"
        public string? Descripcion { get; set; }

        public ICollection<PermisoRol> PermisosRoles { get; set; } = new List<PermisoRol>();
    }
}
