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
    public class ProductMainCategoriesController : ControllerBase
    {
        private readonly DBContext _context;

        public ProductMainCategoriesController(DBContext context)
        {
            _context = context;
        }

        // GET: api/ProductMainCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductMainCategory>>> GetProductMainCategories()
        {
            try
            {
                return await _context.ProductMainCategories.ToListAsync();
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        // GET: api/ProductMainCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductMainCategory>> GetProductMainCategory(int id)
        {
            try
            {
                var productMainCategory = await _context.ProductMainCategories.FindAsync(id);

                if (productMainCategory == null)
                {
                    return NotFound();
                }

                return productMainCategory;
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("NestedMenu")]
        public async Task<ActionResult<IEnumerable<Article>>> GetNestedMenu(int catid, int id)
        {
            try
            {
                var data = await _context.ProductMainCategories
                                         .Where(x => x.Status == true)
                                         .Include(x => x.ProductCategories)
                                         .ToListAsync();
                return Ok(data);
            }
            catch (Exception)
            {

                return NotFound();
            }

        }

        [HttpGet("NestedProduct")]
        public async Task<ActionResult<IEnumerable<Article>>> GetNestedProduct(int catid, int id)
        {
            try
            {
                var data = await _context.ProductMainCategories
                                         .Where(x => x.Status == true)
                                         .Include(x => x.ProductCategories)
                                         .ThenInclude(y => y.Products.OrderBy(y => Guid.NewGuid()).Take(9))
                                         .Where(x => x.Status == true && x.Code != "hide")
                                         .OrderBy(x => x.Position)
                                         .ToListAsync();
                return Ok(data);
            }
            catch (Exception)
            {

                return NotFound();
            }

        }



        // PUT: api/ProductMainCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductMainCategory(int id, ProductMainCategory productMainCategory)
        {
            if (id != productMainCategory.ProductMainCategoryID)
            {
                return BadRequest();
            }

            _context.Entry(productMainCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductMainCategoryExists(id))
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

        // POST: api/ProductMainCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductMainCategory>> PostProductMainCategory(ProductMainCategory productMainCategory)
        {
            try
            {
                _context.ProductMainCategories.Add(productMainCategory);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return NoContent();
            }

            return CreatedAtAction("GetProductMainCategory", new { id = productMainCategory.ProductMainCategoryID }, productMainCategory);
        }

        // DELETE: api/ProductMainCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductMainCategory(int id)
        {
            try
            {
                var productMainCategory = await _context.ProductMainCategories.FindAsync(id);
                if (productMainCategory == null)
                {
                    return NotFound();
                }

                _context.ProductMainCategories.Remove(productMainCategory);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return NotFound();
            }

            return NoContent();
        }

        private bool ProductMainCategoryExists(int id)
        {
            return (_context.ProductMainCategories?.Any(e => e.ProductMainCategoryID == id)).GetValueOrDefault();
        }
    }
}
