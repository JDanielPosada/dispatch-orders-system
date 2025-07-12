using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Utils;
using DispatchOrderSystem.Domain.Interfaces;
using FluentValidation;

namespace DispatchOrderSystem.Application.Validators.Orders
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;

        public CreateOrderRequestValidator(IClientRepository clientRepository, IProductRepository productRepository)
        {
            _clientRepository = clientRepository;
            _productRepository = productRepository;

            RuleFor(x => x.ClientId)
                .NotEmpty().WithMessage("El cliente es obligatorio.")
                .MustAsync(ClientExists).WithMessage("El cliente no existe.");

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("El producto es obligatorio.")
                .MustAsync(ProductExists).WithMessage("El producto no existe.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0.");

            RuleFor(x => x.OriginLatitude)
                .InclusiveBetween(-90, 90).WithMessage("La latitud de origen debe estar entre -90 y 90.");

            RuleFor(x => x.OriginLongitude)
                .InclusiveBetween(-180, 180).WithMessage("La longitud de origen debe estar entre -180 y 180.");

            RuleFor(x => x.DestinationLatitude)
                .InclusiveBetween(-90, 90).WithMessage("La latitud de destino debe estar entre -90 y 90.");

            RuleFor(x => x.DestinationLongitude)
                .InclusiveBetween(-180, 180).WithMessage("La longitud de destino debe estar entre -180 y 180.");

            RuleFor(x => new { x.OriginLatitude, x.OriginLongitude, x.DestinationLatitude, x.DestinationLongitude })
                .Must(coords => !(coords.OriginLatitude == coords.DestinationLatitude && coords.OriginLongitude == coords.DestinationLongitude))
                .WithMessage("El origen y destino no pueden ser iguales.");

            RuleFor(x => x)
                .Must(HaveValidDistance)
                .WithMessage("La distancia debe estar entre 1 y 1000 kilómetros.");
        }

        private async Task<bool> ClientExists(Guid clientId, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(clientId);
            return client != null;
        }

        private async Task<bool> ProductExists(Guid productId, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            return product != null;
        }

        private bool HaveValidDistance(CreateOrderRequest request)
        {
            double distance = HaversineCalculator.CalculateDistanceKm(
                request.OriginLatitude, request.OriginLongitude,
                request.DestinationLatitude, request.DestinationLongitude
            );

            return distance >= 1 && distance <= 1000;
        }
    }
}
