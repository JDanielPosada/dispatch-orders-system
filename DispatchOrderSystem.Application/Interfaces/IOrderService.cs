using DispatchOrderSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatchOrderSystem.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponse> CreateOrderAsync(CreateOrderRequest request);
        Task<List<OrderDetailDto>> GetOrdersByClientIdAsync(Guid clientId);
        Task<OrderDetailDto?> GetOrderDetailByIdAsync(Guid orderId);
        Task DeleteOrderAsync(Guid orderId);
        Task<List<OrderDetailDto>> GetAllOrderDetailsAsync();
        Task<List<ClientDistanceReportDto>> GetClientDistanceReportAsync();
    }
}
