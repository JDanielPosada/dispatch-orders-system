using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Queries.Clients;
using DispatchOrderSystem.Application.Services.Interfaces;
using DispatchOrderSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatchOrderSystem.Application.Queries.Products
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, IEnumerable<ProductDto>>
    {
        private readonly IProductService _producService;

        public GetAllProductsQueryHandler(IProductService productService)
        {
            _producService = productService;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            return await _producService.GetAllProductsAsync();
        }
    }
}
