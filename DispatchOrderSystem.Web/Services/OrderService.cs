using DispatchOrderSystem.Web.Models.Orders;
using DispatchOrderSystem.Web.Services.Interfaces;
using System.Net.Http;

namespace DispatchOrderSystem.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _http;

        public OrderService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("DispatchOrderApi");
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersByClientIdAsync(Guid clientId)
        {
            try
            {
                var response = await _http.GetFromJsonAsync<IEnumerable<OrderResponse>>($"/api/orders/client/{clientId}");
                return response ?? Enumerable.Empty<OrderResponse>();
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Error al obtener las órdenes del cliente. Intenta nuevamente.", ex);
            }

        }

        public async Task CreateOrderAsync(CreateOrderRequest request)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("/api/orders", request);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Error al crear la orden. Intenta nuevamente.", ex);
            }
        }

        public async Task<List<OrderResponse>> GetAllOrdersAsync()
        {
            var response = await _http.GetFromJsonAsync<List<OrderResponse>>("api/orders/getAll");
            return response ?? new List<OrderResponse>();
        }
    }
}
