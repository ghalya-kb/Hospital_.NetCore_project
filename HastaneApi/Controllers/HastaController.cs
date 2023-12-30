using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjeOdevi.Models;
namespace HastaneAPI.Controllers
{
    [ApiController]
    [Route("/api/hasta")]

    public class HastaController : ControllerBase
    {
        HastaneContext _context = new HastaneContext();
        [HttpGet("hastalar")]
        public async Task<IActionResult> GetHastalar()
        {
            var hastalar = await _context.Hastalar.ToListAsync();
            if (hastalar.Count > 0)
                return Ok(hastalar);
            return BadRequest();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> HastaById(int id)
        {
            var hasta = await _context.Hastalar.FirstOrDefaultAsync(x => x.Id == id);
            if (hasta != null)
                return Ok(hasta);
            return NotFound();
        }
        [HttpGet("randevular")]
        public async Task<IActionResult> HastaRandevulari(int id)
        {
            var hasta = await _context.Hastalar.Include(x => x.AlinanRandevular).FirstOrDefaultAsync(x => x.Id == id);
            if (hasta == null)
                return NotFound();

            var randevular = hasta.AlinanRandevular.ToList();
            if (randevular != null && randevular.Count() > 0)
                return Ok(randevular);
            return NotFound();
        }
    }
}