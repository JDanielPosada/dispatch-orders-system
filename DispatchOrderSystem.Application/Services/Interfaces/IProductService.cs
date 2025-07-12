using DispatchOrderSystem.Application.DTOs;

namespace DispatchOrderSystem.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<bool> ExistsAsync(Guid clientId);
        Task<Guid> CreateProductAsync(string name, string description);

    }
}
