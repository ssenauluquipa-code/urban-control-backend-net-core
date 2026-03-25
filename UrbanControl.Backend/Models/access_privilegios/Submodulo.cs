using System.ComponentModel.DataAnnotations;

namespace UrbanControl.Backend.Models.access_privilegios
{
    public class Submodulo
    {
        public int Id { get; set; }
        public int ModuloId { get; set; }
        [Required, MaxLength(100)]
        public string Nombre { get; set; } = string.Empty; // Ej: "Lotes"
        public string RutaAngular { get; set; } = string.Empty; // Ej: "/dashboard/lotes"

        public Modulo Modulo { get; set; } = null!;
        public ICollection<CapacidadSubmodulo> Capacidades { get; set; } = new List<CapacidadSubmodulo>();
    }
}
