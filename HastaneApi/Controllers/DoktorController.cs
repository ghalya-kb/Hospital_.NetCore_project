using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjeOdevi.Models;
using System.Globalization;

namespace doktorneApi.Controllers
{
    [ApiController]
    [Route("/api/doktor")]
    public class DoktorController : ControllerBase
    {
        HastaneContext _context = new HastaneContext();
        [HttpGet("doktorlar")]
        public async Task<IActionResult> GetDoktorlar()
        {
            var doktorlar = await _context.Doktorlar.ToListAsync();
            if (doktorlar.Count > 0)
                return Ok(doktorlar);
            return BadRequest();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> DoktorById(int id)
        {
            var doktor = await _context.Doktorlar.FirstOrDefaultAsync(x => x.Id == id);
            if (doktor != null)
                return Ok(doktor);
            return NotFound();
        }
        [HttpGet("randevular")]
        public async Task<IActionResult> DoktorRandevulari(int id)
        {
            var doktor = await _context.Doktorlar.Include(x => x.RandevuListesi).FirstOrDefaultAsync(x => x.Id == id);
            if (doktor == null)
                return NotFound();

            var randevular = doktor.RandevuListesi.ToList();
            if (randevular != null && randevular.Count() > 0)
                return Ok(randevular);
            return NotFound();
        }
        [HttpGet("musait-randevu")]
        public async Task<IActionResult> GetUygunRandevuSaatlari(int doktorId, string secilenGun)
        {
            var doktor = _context.Doktorlar.Include(x => x.RandevuListesi).FirstOrDefault(x => x.Id == doktorId);
            if (doktor == null)
                return NotFound();

            var saatler = new List<string>()
            {
                "09:00",
                "10:00",
                "11:00",
                "12:00",
                "14:00",
                "15:00",
                "16:00",
                "17:00",
            };
            var randevuListesi = doktor.RandevuListesi;
            if (randevuListesi.Count == 0)
                return Ok(saatler);

            CultureInfo turkishCulture = new CultureInfo("tr-TR");


            foreach (var randevu in randevuListesi.Where(x => x.HastaId != null))
            {
                var gun = randevu.Tarih.ToString("dddd", turkishCulture);
                if (gun == secilenGun)
                {
                    saatler.Remove(randevu.Tarih.ToString("HH:mm"));
                }
            }
            if (saatler.Count > 0)
                return Ok(saatler);
            return Ok("Uygun Randevu Yok");
        }
    }
}
