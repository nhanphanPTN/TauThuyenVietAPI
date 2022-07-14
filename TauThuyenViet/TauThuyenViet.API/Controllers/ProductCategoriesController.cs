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
    public class ProductCategoriesController : ControllerBase
    {
        private readonly DBContext _context;

        public ProductCategoriesController(DBContext context)
        {
            _context = context;
        }

        // GET: api/ProductCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetProductCategories()
        {
            try
            {
                return await _context.ProductCategories.ToListAsync();
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        // GET: api/ProductCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategory>> GetProductCategory(int id)
        {
            try
            {
                var productCategory = await _context.ProductCategories.FindAsync(id);

                if (productCategory == null)
                {
                    return NotFound();
                }

                return productCategory;
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // PUT: api/ProductCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductCategory(int id, ProductCategory productCategory)
        {
            if (id != productCategory.ProductCategoryID)
            {
                return BadRequest();
            }

            _context.Entry(productCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCategoryExists(id))
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

        // POST: api/ProductCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductCategory>> PostProductCategory(ProductCategory productCategory)
        {
            try
            {
                _context.ProductCategories.Add(productCategory);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return NoContent();
            }

            return CreatedAtAction("GetProductCategory", new { id = productCategory.ProductCategoryID }, productCategory);
        }

        // DELETE: api/ProductCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductCategory(int id)
        {
            try
            {
                var productCategory = await _context.ProductCategories.FindAsync(id);
                if (productCategory == null)
                {
                    return NotFound();
                }

                _context.ProductCategories.Remove(productCategory);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return NotFound();
            }

            return NoContent();
        }

        private bool ProductCategoryExists(int id)
        {
            return (_context.ProductCategories?.Any(e => e.ProductCategoryID == id)).GetValueOrDefault();
        }
    }
}
