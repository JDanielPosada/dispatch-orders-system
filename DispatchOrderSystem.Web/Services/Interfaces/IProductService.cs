using DispatchOrderSystem.Web.Models.Products;

namespace DispatchOrderSystem.Web.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task CreateAsync(CreateProductViewModel product);

    }
}
