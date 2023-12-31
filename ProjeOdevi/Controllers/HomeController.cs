using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjeOdevi.Models;
using System.Diagnostics;

namespace ProjeOdevi.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task <IActionResult> Index()
        {
            //HastaneContext hastaneContext = new HastaneContext();
            //List<Doktor> doktorlar = new List<Doktor>();
            //HttpClient client = new HttpClient();

            //var response = await client.GetAsync("https://localhost:7090/api/doktor/doktorlar");
            //var jsonResponse = await response.Content.ReadAsStringAsync();
            //doktorlar = JsonConvert.DeserializeObject<List<Doktor>>(jsonResponse);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Identity");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}