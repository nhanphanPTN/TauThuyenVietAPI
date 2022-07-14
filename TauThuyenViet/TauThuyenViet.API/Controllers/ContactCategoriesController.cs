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
    public class ContactCategoriesController : ControllerBase
    {
        private readonly DBContext _context;

        public ContactCategoriesController(DBContext context)
        {
            _context = context;
        }

        // GET: api/ContactCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactCategory>>> GetContactCategories()
        {
            try
            {
                return await _context.ContactCategories.ToListAsync();
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        // GET: api/ContactCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactCategory>> GetContactCategory(int id)
        {
            try
            {
                var contactCategory = await _context.ContactCategories.FindAsync(id);

                if (contactCategory == null)
                {
                    return NotFound();
                }

                return contactCategory;
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // PUT: api/ContactCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactCategory(int id, ContactCategory contactCategory)
        {
            if (id != contactCategory.ContactCategoryID)
            {
                return BadRequest();
            }

            _context.Entry(contactCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactCategoryExists(id))
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

        // POST: api/ContactCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContactCategory>> PostContactCategory(ContactCategory contactCategory)
        {
            try
            {
                _context.ContactCategories.Add(contactCategory);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return NoContent();
            }

            return CreatedAtAction("GetContactCategory", new { id = contactCategory.ContactCategoryID }, contactCategory);
        }

        // DELETE: api/ContactCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactCategory(int id)
        {
            try
            {
                var contactCategory = await _context.ContactCategories.FindAsync(id);
                if (contactCategory == null)
                {
                    return NotFound();
                }

                _context.ContactCategories.Remove(contactCategory);
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {

                return NotFound();
            }


            return NoContent();
        }

        private bool ContactCategoryExists(int id)
        {
            return (_context.ContactCategories?.Any(e => e.ContactCategoryID == id)).GetValueOrDefault();
        }
    }
}
