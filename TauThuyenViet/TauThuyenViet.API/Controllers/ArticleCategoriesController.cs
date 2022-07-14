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
    public class ArticleCategoriesController : ControllerBase
    {
        private readonly DBContext _context;

        public ArticleCategoriesController(DBContext context)
        {
            _context = context;
        }

        // GET: api/ArticleCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleCategory>>> GetArticleCategories()
        {
            try
            {
                return await _context.ArticleCategories.ToListAsync();
            }
            catch (Exception)
            {

                return NotFound();
            }

        }

        // GET: api/ArticleCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleCategory>> GetArticleCategory(int id)
        {
            try
            {
                var articleCategory = await _context.ArticleCategories.FindAsync(id);

                if (articleCategory == null)
                {
                    return NotFound();
                }

                return articleCategory;
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // PUT: api/ArticleCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticleCategory(int id, ArticleCategory articleCategory)
        {
            if (id != articleCategory.ArticleCategoryID)
            {
                return BadRequest();
            }

            _context.Entry(articleCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleCategoryExists(id))
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

        // POST: api/ArticleCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArticleCategory>> PostArticleCategory(ArticleCategory articleCategory)
        {
            try
            {
                _context.ArticleCategories.Add(articleCategory);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return NoContent();
            }

            return CreatedAtAction("GetArticleCategory", new { id = articleCategory.ArticleCategoryID }, articleCategory);
        }

        // DELETE: api/ArticleCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticleCategory(int id)
        {
            try
            {
                var articleCategory = await _context.ArticleCategories.FindAsync(id);
                if (articleCategory == null)
                {
                    return NotFound();
                }

                _context.ArticleCategories.Remove(articleCategory);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return NotFound();
            }

            return NoContent();
        }

        private bool ArticleCategoryExists(int id)
        {
            return (_context.ArticleCategories?.Any(e => e.ArticleCategoryID == id)).GetValueOrDefault();
        }
    }
}
