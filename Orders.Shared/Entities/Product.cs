using System.ComponentModel.DataAnnotations;

namespace Orders.Shared.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Producto")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Descripción")]
        [MaxLength(500, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string? Description { get; set; }

        [Display(Name = "Precio")]
        [Range(0, double.MaxValue, ErrorMessage = "El campo {0} debe ser mayor o igual a {1}.")]
        public decimal Price { get; set; }

        // Relación muchos a muchos con Category
        public ICollection<Category> Categories { get; set; } = new List<Category>();

        // Relación uno a muchos con imágenes (por simplicidad, solo nombres de archivo)
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
    }
}
