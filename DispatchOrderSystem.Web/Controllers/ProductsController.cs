using DispatchOrderSystem.Web.Models.Products;
using DispatchOrderSystem.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DispatchOrderSystem.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: /Products
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _productService.CreateAsync(model);
                TempData["SuccessMessage"] = "Producto creado exitosamente.";
                return RedirectToAction("Index");
            }
            catch (ApplicationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(model);
            }
        }
    }
}
