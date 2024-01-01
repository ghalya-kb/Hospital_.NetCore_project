using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjeOdevi.Models;
using System.Diagnostics;
using System.Security.Claims;
using System.Net.Http.Headers;

namespace ProjeOdevi.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HastaneContext _context = new HastaneContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task <IActionResult> Index()
        {
            HastaneContext hastaneContext = new HastaneContext();
            hastaneContext.Adminler.Add(new Admin()
            {
                Adi = "Admin1",
                Soyadi = "",
                KullaniciAdi = "b201210571@sakarya.edu.tr",
                Sifre = "sau",
                TCNo = "1234567890",
            });
            hastaneContext.SaveChanges();
            List<Birim> birimler = new List<Birim>();
            List<Doktor> doktorlar = new List<Doktor>();
            HttpClient client = new HttpClient();

            var response = await client.GetAsync("https://localhost:7090/api/birim/birimler");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            birimler = JsonConvert.DeserializeObject<List<Birim>>(jsonResponse);

            response = await client.GetAsync("https://localhost:7090/api/doktor/musait-randevu?doktorId=3&secilenGun=Pazartesi");
            jsonResponse = await response.Content.ReadAsStringAsync();
            var liste = JsonConvert.DeserializeObject<List<string>>(jsonResponse);

            ViewData["Birimler"] = birimler;
        
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RandevuAl(RandevuAlModeli randevu)
        {
            int userIdFromProperties = Convert.ToInt32(HttpContext.User.Identity.Name);
            
            Randevu yeniRandevu = new Randevu()
            {
                DoktorId = randevu.DoktorId,
                HastaId = userIdFromProperties,
                Tarih = GetDateByDay(randevu.Gun,randevu.Saat),
            };
            _context.Randevular.Add(yeniRandevu);
            _context.SaveChanges();
            TempData["RandevuAlindi"] = $"Randevu Basari ile Alindi.";
            return RedirectToAction("Index", "Home");
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
        private DateTime GetDateByDay(string gun,string saat)
        {
            DateTime result =DateTime.Today;
            int saatInt = Convert.ToInt32(saat.Split(":")[0]);
            result = new DateTime(result.Year, result.Month, result.Day, saatInt, 0, 0);
            while(result.DayOfWeek.ToString().ToLower() != gun.ToLower()){
                result = result.AddDays(1);
            }
            return result;
        }

    }
}