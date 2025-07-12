using System.ComponentModel.DataAnnotations;

namespace DispatchOrderSystem.Web.Models.Clients
{
    public class CreateClientViewModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Name { get; set; } = string.Empty;
    }
}
