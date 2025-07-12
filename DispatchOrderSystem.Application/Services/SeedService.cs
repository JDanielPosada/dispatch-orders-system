using DispatchOrderSystem.Application.Interfaces;
using DispatchOrderSystem.Application.Services.Interfaces;
using DispatchOrderSystem.Application.Utils;
using DispatchOrderSystem.Domain.Aggregates.Entities;
using DispatchOrderSystem.Domain.Aggregates.ValueObjects;
using DispatchOrderSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatchOrderSystem.Application.Services
{
    public class SeedService : ISeedService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;

        public SeedService(
            IOrderRepository orderRepository,
            IClientRepository clientRepository,
            IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
            _productRepository = productRepository;
        }

        public async Task<string> SeedOrdersAsync()
        {
            // Verify if Clients exist, if not, create some
            var clients = await _clientRepository.GetAllAsync();
            if (!clients.Any())
            {
                var newClients = new List<Client>
                {
                    new Client { Name = "Client A" },
                    new Client { Name = "Client B" },
                    new Client { Name = "Client C" }
                };

                foreach (var client in newClients)
                {
                    await _clientRepository.AddAsync(client);
                }

                clients = newClients;
            }

            // Verify if Products exist, if not, create some
            var products = await _productRepository.GetAllAsync();
            if (!products.Any())
            {
                var newProducts = new List<Product>
                {
                    new Product { Name = "Product A", Description = "Description 1" },
                    new Product { Name = "Product B", Description = "Description 2" },
                    new Product { Name = "Product C", Description = "Description 3" }
                };

                foreach (var product in newProducts)
                {
                    await _productRepository.AddAsync(product);
                }

                products = newProducts;
            }


            if (!clients.Any() || !products.Any())
                return "Debe registrar al menos un cliente y un producto.";

            var orders = GenerateOrders(clients, products);

            foreach (var order in orders)
            {
                await _orderRepository.AddAsync(order);
            }

            return $"{orders.Count} órdenes, {clients.Count} clientes y {products.Count} productos creados o verificados.";

        }

        private List<Order> GenerateOrders(List<Client> clients, List<Product> products)
        {
            var random = new Random();
            var coordinates = GetSampleCoordinates();
            var orders = new List<Order>();

            foreach (var (origin, destination) in coordinates)
            {
                var client = clients[random.Next(clients.Count)];
                var product = products[random.Next(products.Count)];

                double distance = HaversineCalculator.CalculateDistanceKm(
                    origin.Latitude, origin.Longitude,
                    destination.Latitude, destination.Longitude);

                decimal cost = GetCostByDistance(distance);

                orders.Add(new Order
                {
                    ClientId = client.Id,
                    ProductId = product.Id,
                    Quantity = random.Next(1, 10),
                    Origin = origin,
                    Destination = destination,
                    DistanceKm = distance,
                    Cost = cost,
                    CreatedAt = DateTime.UtcNow.AddDays(-random.Next(30))
                });
            }

            return orders;
        }

        private List<(Coordinates origin, Coordinates destination)> GetSampleCoordinates() => new()
        {
            // 1–50 km
            (new(4.624335, -74.063644), new(4.85, -74.10)),
            (new(6.25184, -75.56359), new(6.5, -75.5)),
            (new(10.96854, -74.78132), new(11.2, -74.8)),
            (new(3.43722, -76.5225), new(3.6, -76.6)),
            (new(7.11935, -73.12274), new(7.3, -73.1)),

            // 51–200 km
            (new(4.624335, -74.063644), new(5.75, -73.1)),
            (new(6.25184, -75.56359), new(7.0, -74.3)),
            (new(10.96854, -74.78132), new(9.2, -75.8)),
            (new(3.43722, -76.5225), new(4.5, -75.5)),
            (new(7.11935, -73.12274), new(5.6, -72.2)),

            // 201–500 km
            (new(4.624335, -74.063644), new(8.75, -75.9)),
            (new(6.25184, -75.56359), new(10.5, -74.8)),
            (new(10.96854, -74.78132), new(7.1, -73.1)),
            (new(3.43722, -76.5225), new(8.4, -75.3)),
            (new(7.11935, -73.12274), new(3.1, -76.2)),

            // 501–1000 km
            (new(4.624335, -74.063644), new(11.2, -74.2)),
            (new(6.25184, -75.56359), new(11.5, -72.5)),
            (new(10.96854, -74.78132), new(1.5, -77.0)),
            (new(3.43722, -76.5225), new(10.4, -74.3)),
            (new(7.11935, -73.12274), new(1.5, -78.0))
        };

        private decimal GetCostByDistance(double distance)
        {
            if (distance <= 50) return 100;
            if (distance <= 200) return 300;
            if (distance <= 500) return 1000;
            return 1500;
        }
    }
}
