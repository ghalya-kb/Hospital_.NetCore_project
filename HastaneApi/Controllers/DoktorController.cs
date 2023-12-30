using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjeOdevi.Models;

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
    }
}
