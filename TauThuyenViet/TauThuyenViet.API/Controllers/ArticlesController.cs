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
    public class ArticlesController : ControllerBase
    {
        private readonly DBContext _context;

        public ArticlesController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Articles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
        {
            try
            {
                return await _context.Articles.ToListAsync();
            }
            catch (Exception)
            {

                return NotFound();
            }

        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            try
            {
                var article = await _context.Articles.FindAsync(id);
                if (article == null)
                {
                    return NotFound();
                }

                return article;
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        // GET api/Aticles/related/1/5
        [HttpGet("related/{catid}/{id}")]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticlesRelated(int catid, int id)
        {
            try
            {
                return await _context.Articles
                                     .Where(x => x.Status == true && x.ArticleCategoryID == catid && x.ArticleID != id)
                                     .OrderBy(x => Guid.NewGuid())
                                     .Take(6)
                                     .ToListAsync();
            }
            catch (Exception)
            {

                return NotFound();
            }

        }



        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(int id, Article article)
        {
            if (id != article.ArticleID)
            {
                return BadRequest();
            }

            _context.Entry(article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
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

        // POST: api/Articles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Article>> PostArticle(Article article)
        {
            try
            {
                _context.Articles.Add(article);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return NoContent();
            }

            return CreatedAtAction("GetArticle", new { id = article.ArticleID }, article);
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {

            try
            {
                var article = await _context.Articles.FindAsync(id);
                if (article == null)
                {
                    return NotFound();
                }

                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return NotFound();
            }

            return NoContent();
        }

        private bool ArticleExists(int id)
        {
            return (_context.Articles?.Any(e => e.ArticleID == id)).GetValueOrDefault();
        }
    }
}
