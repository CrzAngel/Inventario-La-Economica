using System.ComponentModel.DataAnnotations;

namespace InventarioLaEconomica.Models
{
    public class Proveedor
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(20)]
        public string? RNC { get; set; }

        [StringLength(20)]
        public string? Telefono { get; set; }

        [StringLength(150)]
        public string? Email { get; set; }

        [StringLength(250)]
        public string? Direccion { get; set; }

        public bool Activo { get; set; } = true;
    }
}