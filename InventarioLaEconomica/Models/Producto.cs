using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventarioLaEconomica.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Codigo { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioCompra { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioVenta { get; set; }

        public int Stock { get; set; }

        public int CategoriaId { get; set; }

        public Categoria? Categoria { get; set; }

        public bool Activo { get; set; } = true;
    }
}