using System.ComponentModel.DataAnnotations;

namespace InventarioLaEconomica.Models
{
    public class MovimientoInventario
    {
        public int Id { get; set; }

        public int ProductoId { get; set; }

        public Producto? Producto { get; set; }

        [Required]
        [StringLength(20)]
        public string Tipo { get; set; } = string.Empty;

        public int Cantidad { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        [StringLength(250)]
        public string? Descripcion { get; set; }
    }
}