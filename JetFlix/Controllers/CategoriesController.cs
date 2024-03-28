using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JetFlix_API.Data;
using JetFlix_API.Models;
using Microsoft.AspNetCore.Authorization;

namespace JetFlix_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public  ActionResult<List<Category>> GetCategories()
        {

            //return _context.Categories.ToList();
            return _context.Categories.AsNoTracking().ToList(); 
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public ActionResult<Category> GetCategory(short id)
        {
          
            Category? category = _context.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public void PutCategory(Category category)//IActionResulltu kaldırdık. Nocontent dönen bir şeye ActionResulta ihtiyaç yok. id'yi de kaldırdık
        {

            _context.Categories.Update(category);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception )
            {
                
            }

            
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Authorize(Roles ="ContentAdmin")]  
        public short PostCategory(Category category)
        {
          
          
            _context.Categories.Add(category);
            _context.SaveChanges();

            //return CreatedAtAction("GetCategory", new { id = category.Id }, category); // GetCategory Actionunu çağırıyor ve GetCategory buraya id ile dönyüyor. Gereksiz

            return category.Id;
         }

        //// DELETE: api/Categories/5  // Hiçbir şey silmeyeceğimiz  için Delete Actionuna gerek yok
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCategory(short id)
        //{
            
        //    Category? category = _context.Categories.Find(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Categories.Remove(category);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        
    }
}
