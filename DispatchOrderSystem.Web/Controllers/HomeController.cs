using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DispatchOrderSystem.Web.Models;

namespace DispatchOrderSystem.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
