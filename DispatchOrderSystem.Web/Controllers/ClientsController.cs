using DispatchOrderSystem.Web.Models.Clients;
using DispatchOrderSystem.Web.Models.Products;
using DispatchOrderSystem.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DispatchOrderSystem.Web.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // GET: /Products
        public async Task<IActionResult> Index()
        {
            var products = await _clientService.GetAllClientsAsync();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClientViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _clientService.CreateAsync(model);
                TempData["SuccessMessage"] = "Cliente creado exitosamente.";
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
