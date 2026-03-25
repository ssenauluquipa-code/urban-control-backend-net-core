using System.ComponentModel.DataAnnotations;

namespace UrbanControl.Backend.Models.access_privilegios
{
    public class TipoPermiso
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Nombre { get; set; } = string.Empty; // Ej: "Crear"
        [Required, MaxLength(50)]
        public string Slug { get; set; } = string.Empty;   // Ej: "create"
    }
}
