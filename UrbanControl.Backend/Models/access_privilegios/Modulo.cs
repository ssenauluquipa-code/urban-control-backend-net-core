using System.ComponentModel.DataAnnotations;

namespace UrbanControl.Backend.Models.access_privilegios
{
    public class Modulo
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Nombre { get; set; } = string.Empty; // Ej: "Inventario"
        public string Icono { get; set; } = string.Empty; // Ej: "pi pi-box"
        public int Orden { get; set; }
        public ICollection<Submodulo> Submodulos { get; set; } = new List<Submodulo>();
    }
}
