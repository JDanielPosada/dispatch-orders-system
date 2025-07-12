using DispatchOrderSystem.Web.Models.Orders;
using DispatchOrderSystem.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DispatchOrderSystem.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IClientService _clientService;
        private readonly IProductService _productService;
        public OrdersController(
            IOrderService orderService,
            IClientService clientService,
            IProductService productService)
        {
            _orderService = orderService;
            _clientService = clientService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateOrderViewModel
            {
                Clients = await _clientService.GetAllClientsAsync(),
                Products = await _productService.GetAllProductsAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Clients = await _clientService.GetAllClientsAsync();
                model.Products = await _productService.GetAllProductsAsync();

                return View(model);
            }

            try
            {
                var request = new CreateOrderRequest
                {
                    ClientId = model.ClientId,
                    ProductId = model.ProductId,
                    Quantity = model.Quantity,
                    OriginLatitude = model.OriginLatitude,
                    OriginLongitude = model.OriginLongitude,
                    DestinationLatitude = model.DestinationLatitude,
                    DestinationLongitude = model.DestinationLongitude
                };

                await _orderService.CreateOrderAsync(request);
                TempData["SuccessMessage"] = "La orden fue creada exitosamente.";
                return RedirectToAction("Index", "Orders");
            }
            catch (ApplicationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;

                model.Clients = await _clientService.GetAllClientsAsync();
                model.Products = await _productService.GetAllProductsAsync();
                return View(model);
            }
        }
        public IActionResult Success() => View();

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }
    }
}
