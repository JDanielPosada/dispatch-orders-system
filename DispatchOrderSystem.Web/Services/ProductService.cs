using DispatchOrderSystem.Web.Models.Products;
using DispatchOrderSystem.Web.Services.Interfaces;

namespace DispatchOrderSystem.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;

        public ProductService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("DispatchOrderApi");
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var result = await _http.GetFromJsonAsync<IEnumerable<ProductDto>>("/api/products");
            return result ?? Enumerable.Empty<ProductDto>();
        }
        public async Task CreateAsync(CreateProductViewModel product)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("/api/products", product);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Error al crear el producto. Intenta nuevamente.", ex);
            }
        }

    }
}
