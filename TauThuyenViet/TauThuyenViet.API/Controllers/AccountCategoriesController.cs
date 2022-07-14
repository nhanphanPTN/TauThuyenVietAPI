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
    public class AccountCategoriesController : ControllerBase
    {
        private readonly DBContext _context;

        public AccountCategoriesController(DBContext context)
        {
            _context = context;
        }

        // GET: api/AccountCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountCategory>>> GetAccountCategories()
        {
            try
            {
                return await _context.AccountCategories.ToListAsync();
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        // GET: api/AccountCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountCategory>> GetAccountCategory(string id)
        {
            try
            {
                var accountCategory = await _context.AccountCategories.FindAsync(id);

                if (accountCategory == null)
                {
                    return NotFound();
                }

                return accountCategory;
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // PUT: api/AccountCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountCategory(string id, AccountCategory accountCategory)
        {
            if (id != accountCategory.AccountCategoryID)
            {
                return BadRequest();
            }

            _context.Entry(accountCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountCategoryExists(id))
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

        // POST: api/AccountCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccountCategory>> PostAccountCategory(AccountCategory accountCategory)
        {
            try
            {
                _context.AccountCategories.Add(accountCategory);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                    return NoContent();
            }

            return CreatedAtAction("GetAccountCategory", new { id = accountCategory.AccountCategoryID }, accountCategory);
        }

        // DELETE: api/AccountCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountCategory(string id)
        {
            try
            {
                var accountCategory = await _context.AccountCategories.FindAsync(id);
                if (accountCategory == null)
                {
                    return NotFound();
                }

                _context.AccountCategories.Remove(accountCategory);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return NotFound();
            }

            return NoContent();
        }

        private bool AccountCategoryExists(string id)
        {
            return (_context.AccountCategories?.Any(e => e.AccountCategoryID == id)).GetValueOrDefault();
        }
    }
}
