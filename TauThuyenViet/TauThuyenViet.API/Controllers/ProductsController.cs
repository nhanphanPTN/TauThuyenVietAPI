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
    public class ProductsController : ControllerBase
    {
        private readonly DBContext _context;

        public ProductsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                return await _context.Products.ToListAsync();
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                return product;
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // GET api/Product/related/1/5
        [HttpGet("related/{catid}/{id}")]
        public async Task<ActionResult<IEnumerable<Article>>> GetProductsRelated(int catid, int id)
        {
            try
            {
                var query = _context.Products
                                     .Where(x => x.Status == true)
                                     .OrderBy(x => Guid.NewGuid())
                                     .AsQueryable();

                if (id > 0)
                {
                    query = query.Where(x => x.ProductID != id);
                }
                if (catid > 0)
                {
                    query = query.Where(x => x.ProductCategoryID == catid);

                }

                var data = await query.Take(9).ToListAsync();

                return Ok(data);

            }
            catch (Exception)
            {

                return NotFound();
            }

        }

        // GET: api/Products
        [HttpGet("getlist/{mid}/{cid}")]
        public async Task<ActionResult<IEnumerable<Product>>> getlist(int mid, int cid)
        {
            try
            {
                var data = _context.Products
                                   .Where(x => x.Status == true)
                                   .OrderByDescending(x => x.CreateTime)
                                   .AsQueryable();

                //Nếu có cid, thì load sp theo menu cấp 2
                if (cid > 0)
                {
                    //Load list data của cate
                    data = data.Where(x => x.ProductCategoryID == cid);
                }

                //Nếu Không có cid, mà có mid thì load sp theo menu cấp 1
                else if (mid > 0)
                {
                    data = data.Include(x => x.ProductCategory)
                               .Where(x => x.ProductCategory.ProductMainCategoryID == mid);
                }

                var result = await data.ToListAsync();

                return result;
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductID)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return NoContent();
            }

            return CreatedAtAction("GetProduct", new { id = product.ProductID }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return NotFound();
            }

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductID == id)).GetValueOrDefault();
        }
    }
}
