using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjeOdevi.Models;

namespace HastaneApi.Controllers
{
    [ApiController]
    [Route("/api/birim")]
    public class BirimController : ControllerBase
    {
        HastaneContext _context = new HastaneContext();
        [HttpGet("birimler")]
        public async Task<IActionResult> GetBirimler()
        {
            var birimler = await _context.Birimler.ToListAsync();
            if (birimler.Count > 0)
                return Ok(birimler);
            return BadRequest();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> BirimById(int id)
        {
            var birim = await _context.Birimler.FirstOrDefaultAsync(x => x.Id == id);
            if (birim != null)
                return Ok(birim);
            return NotFound();
        }
        [HttpGet("doktorlar")]
        public async Task<IActionResult> BirimDoktorlari(int id)
        {
            var birim = await _context.Birimler.Include(x => x.doktorlar).FirstOrDefaultAsync(x => x.Id == id);
            if (birim == null)
                return NotFound();

            var doktorlar = birim.doktorlar.ToList();
            if (doktorlar != null && doktorlar.Count() > 0)
                return Ok(doktorlar);
            return NotFound();
        }
    }
}
