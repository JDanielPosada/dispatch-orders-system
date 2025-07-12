using DispatchOrderSystem.Application.Services.Interfaces;
using MediatR;

namespace DispatchOrderSystem.Application.Commands.Products
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, Guid>
    {
        private readonly IProductService _productService;

        public CreateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<Guid> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            return await _productService.CreateProductAsync(request.Name, request.Description);
        }
    }
}
