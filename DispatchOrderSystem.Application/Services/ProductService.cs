using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Services.Interfaces;
using DispatchOrderSystem.Domain.Aggregates.Entities;
using DispatchOrderSystem.Domain.Interfaces;

namespace DispatchOrderSystem.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var clients = await _productRepository.GetAllAsync();
            return clients.Select(c => new ProductDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
            }).ToList();
        }

        public async Task<bool> ExistsAsync(Guid clientId)
        {
            var client = await _productRepository.GetByIdAsync(clientId);
            return client != null;
        }

        public async Task<Guid> CreateProductAsync(string name, string description)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description
            };

            await _productRepository.AddAsync(product);
            return product.Id;
        }
    }
}
