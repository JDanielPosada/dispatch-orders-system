using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Interfaces;
using DispatchOrderSystem.Application.Utils;
using DispatchOrderSystem.Domain.Aggregates.Entities;
using DispatchOrderSystem.Domain.Aggregates.ValueObjects;

namespace DispatchOrderSystem.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderResponse> CreateOrderAsync(CreateOrderRequest request)
        {

            double distance = HaversineCalculator.CalculateDistanceKm(
                request.OriginLatitude, request.OriginLongitude,
                request.DestinationLatitude, request.DestinationLongitude);

            if (distance < 1 || distance > 1000)
            {
                throw new ArgumentException("Distance must be between 1 and 1000 kilometers.");
            }

            decimal cost = GetCostByDistance(distance);

            var order = new Order
            {
                Id = Guid.NewGuid(),
                ClientId = request.ClientId,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                Origin = new Coordinates(request.OriginLatitude, request.OriginLongitude),
                Destination = new Coordinates(request.DestinationLatitude, request.DestinationLongitude),
                DistanceKm = distance,
                Cost = cost,
                CreatedAt = DateTime.UtcNow
            };

            await _orderRepository.AddAsync(order);

            var orderWithIncludes = await _orderRepository.GetByIdAsync(order.Id);


            return new OrderResponse
            {
                OrderId = order.Id,
                ClientName = orderWithIncludes!.Client.Name, 
                ProductName = orderWithIncludes!.Product.Name,
                DistanceKm = distance,
                Cost = cost
            };
        }
        private decimal GetCostByDistance(double distance)
        {
            if (distance <= 50) return 100;
            if (distance <= 200) return 300;
            if (distance <= 500) return 1000;
            return 1500;
        }

        public async Task<List<OrderDetailDto>> GetOrdersByClientIdAsync(Guid clientId)
        {
            var orders = await _orderRepository.GetByClientIdAsync(clientId);
            return orders.Select(o => new OrderDetailDto
            {
                OrderId = o.Id,
                ClientName = o.Client.Name,
                ProductName = o.Product.Name,
                DistanceKm = o.DistanceKm,
                Cost = o.Cost,
                CreatedAt = o.CreatedAt
            }).ToList();
        }

        public async Task<OrderDetailDto?> GetOrderDetailByIdAsync(Guid orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                return null;
            }
            return new OrderDetailDto
            {
                OrderId = order.Id,
                ClientName = order.Client.Name,
                ProductName = order.Product.Name,
                DistanceKm = order.DistanceKm,
                Cost = order.Cost,
                CreatedAt = order.CreatedAt
            };
        }

        public async Task DeleteOrderAsync(Guid orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new ArgumentException("Order not found.");
            }
            await _orderRepository.DeleteAsync(orderId);
        }

        public async Task<List<OrderDetailDto>> GetAllOrderDetailsAsync()
        {
            var orders = await _orderRepository.GetAllAsync();

            return orders.Select(o => new OrderDetailDto
            {
                OrderId = o.Id,
                ClientName = o.Client.Name,
                ProductName = o.Product.Name,
                DistanceKm = o.DistanceKm,
                Cost = o.Cost,
                CreatedAt = o.CreatedAt
            }).ToList();
        }

        public async Task<List<ClientDistanceReportDto>> GetClientDistanceReportAsync()
        {
            var orders = await _orderRepository.GetAllAsync();

            var grouped = orders
                .GroupBy(o => o.Client.Name)
                .Select( g => new ClientDistanceReportDto
                {
                    ClientName = g.Key,
                    Orders_1_50_Km = g.Count(o => o.DistanceKm > 1 && o.DistanceKm <= 50),
                    Orders_51_200_Km = g.Count(o => o.DistanceKm > 50 && o.DistanceKm <= 200),
                    Orders_201_500_Km = g.Count(o => o.DistanceKm > 200 && o.DistanceKm <= 500),
                    Orders_501_1000_Km = g.Count(o => o.DistanceKm > 500 && o.DistanceKm <= 1000)
                })
                .OrderBy(r => r.ClientName)
                .ToList();

            return grouped;
        }
    }
}
