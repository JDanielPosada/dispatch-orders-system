using System.ComponentModel.DataAnnotations;

namespace DispatchOrderSystem.Web.Models.Products
{
    public class CreateProductViewModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string Description { get; set; } = string.Empty;
    }
}
