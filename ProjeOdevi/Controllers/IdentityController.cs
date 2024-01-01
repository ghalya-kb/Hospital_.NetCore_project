using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ProjeOdevi.Models;

namespace ProjeOdevi.Controllers
{
    public class IdentityController : Controller
    {
        HastaneContext _context = new HastaneContext();
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel modelLogin)
        {
            var kul = _context.Kuallanicilar.FirstOrDefault(x=>x.KullaniciAdi == modelLogin.KullaniciAdi && x.Sifre == modelLogin.Sifre);

            if (kul != null)
            {
                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.KullaniciAdi),
                    new Claim(ClaimTypes.Name, kul.Id.ToString())
                };
                if (kul is Hasta)
                    claims.Add(new Claim("Type", "Hasta"));
                else if (kul is Admin)
                    claims.Add(new Claim("Type", "Admin"));
                else
                    claims.Add(new Claim("Type", "Doktor"));

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {

                    AllowRefresh = true,
                    IsPersistent = true
                };
                properties.Items["UserId"] = kul.Id.ToString();

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Home");
            }



            ViewData["ValidateMessage"] = "user not found";
            return View();
        }
    }
}
