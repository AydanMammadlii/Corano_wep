using Microsoft.AspNetCore.Mvc;

namespace Corona.MVC.Controllers;

public class ContactController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
