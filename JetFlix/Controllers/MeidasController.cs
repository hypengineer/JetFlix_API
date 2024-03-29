using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JetFlix_API.Data;
using JetFlix_API.Models;

namespace JetFlix_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeidasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MeidasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Meidas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Media>>> GetMedias()
        {
          if (_context.Medias == null)
          {
              return NotFound();
          }
            return await _context.Medias.ToListAsync();
        }

        // GET: api/Meidas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Media>> GetMedia(int id)
        {
          if (_context.Medias == null)
          {
              return NotFound();
          }
            var media = await _context.Medias.FindAsync(id);

            if (media == null)
            {
                return NotFound();
            }

            return media;
        }

        // PUT: api/Meidas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedia(int id, Media media)
        {
            if (id != media.Id)
            {
                return BadRequest();
            }

            _context.Entry(media).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MediaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Meidas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Media> PostMedia(Media media, short catId)
        {
            MediaCategory mediaCategory = new MediaCategory();
            _context.Medias.Add(media);
            _context.SaveChanges();

            mediaCategory.CategoryId = catId;
            mediaCategory.MediaId = media.Id;
          
           
            _context.MediaCategories.Add(mediaCategory);
            _context.SaveChanges();

            return Ok();
        }

        // DELETE: api/Meidas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedia(int id)
        {
            if (_context.Medias == null)
            {
                return NotFound();
            }
            var media = await _context.Medias.FindAsync(id);
            if (media == null)
            {
                return NotFound();
            }

            _context.Medias.Remove(media);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MediaExists(int id)
        {
            return (_context.Medias?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
