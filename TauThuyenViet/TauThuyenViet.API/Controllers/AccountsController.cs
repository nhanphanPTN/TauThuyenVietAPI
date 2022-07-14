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
    public class AccountsController : ControllerBase
    {
        private readonly DBContext _context;

        public AccountsController(DBContext context)
        {
            _context = context;
        }

        //Hàm lấy 1 danh sách dữ liệu
        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            try
            {
                return await _context.Accounts.ToListAsync();
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        //Hàm lấy nhiều danh sách dữ liệu theo điều kiện là ID
        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(string id)
        {
            try
            {
                var account = await _context.Accounts.FindAsync(id);
                if (account == null)
                {
                    return NotFound();
                }

                return account;
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        //Hàm Cập nhật với ID là Khóa của item cần cập nhật, 
        //và 1 item chứa dữ liệu cần cập nhật
        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(string id, Account account)
        {
            if (id != account.Username)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
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

        //Hàm thêm mới, với item chứa dữ liệu càn thêm mới
        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            try
            {
                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return NoContent();
            }

            return CreatedAtAction("GetAccount", new { id = account.Username }, account);
        }

        //Hàm xóa, với ID là khóa của item cần xóa
        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            try
            {
                var account = await _context.Accounts.FindAsync(id);
                if (account == null)
                {
                    return NotFound();
                }

                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return NotFound();
            }

            return NoContent();
        }

        private bool AccountExists(string id)
        {
            return (_context.Accounts?.Any(e => e.Username == id)).GetValueOrDefault();
        }
    }
}
