using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjeOdevi.Attributes;

namespace ProjeOdevi.Controllers
{
    [AdminAuthorizationAttribute]
    public class AdminController : Controller
    {
        public IActionResult AdminPanel()
        {
            return View();
        }
    }
}
