namespace UrbanControl.Backend.DTOs.acessos_privilegios
{
    public class PermisoRolUpdateDto
    {
        public int RolId { get; set; }
        public int CapacidadId { get; set; }
        public bool Concedido { get; set; }
    }
}
