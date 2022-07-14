using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TauThuyenViet.Models;

namespace TauThuyenViet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleMainCategoriesController : ControllerBase
    {
        private readonly DBContext _context;

        public ArticleMainCategoriesController(DBContext context)
        {
            _context = context;
        }

        // GET: api/ArticleMainCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleMainCategory>>> GetArticleMainCategories()
        {
            try
            {
                return await _context.ArticleMainCategories.ToListAsync();
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        // GET: api/ArticleMainCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleMainCategory>> GetArticleMainCategory(int id)
        {
            try
            {
                var articleMainCategory = await _context.ArticleMainCategories.FindAsync(id);
                if (articleMainCategory == null)
                {
                    return NotFound();
                }

                return articleMainCategory;
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // PUT: api/ArticleMainCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticleMainCategory(int id, ArticleMainCategory articleMainCategory)
        {

            if (id != articleMainCategory.ArticleMainCategoryID)
            {
                return BadRequest();
            }

            _context.Entry(articleMainCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleMainCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }

            return Ok();
        }

        // POST: api/ArticleMainCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArticleMainCategory>> PostArticleMainCategory(ArticleMainCategory articleMainCategory)
        {

            try
            {
                _context.ArticleMainCategories.Add(articleMainCategory);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return NoContent();
            }

            return CreatedAtAction("GetArticleMainCategory", new { id = articleMainCategory.ArticleMainCategoryID }, articleMainCategory);
        }

        // DELETE: api/ArticleMainCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticleMainCategory(int id)
        {
            try
            {
                var articleMainCategory = await _context.ArticleMainCategories.FindAsync(id);
                if (articleMainCategory == null)
                {
                    return NotFound();
                }

                _context.ArticleMainCategories.Remove(articleMainCategory);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return NotFound();
            }

            return NoContent();
        }

        private bool ArticleMainCategoryExists(int id)
        {
            return (_context.ArticleMainCategories?.Any(e => e.ArticleMainCategoryID == id)).GetValueOrDefault();
        }
    }
}
