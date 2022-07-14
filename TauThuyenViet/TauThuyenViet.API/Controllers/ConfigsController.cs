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
    public class ConfigsController : ControllerBase
    {
        private readonly DBContext _context;

        public ConfigsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Configs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Config>>> GetConfigs()
        {
            try
            {
                return await _context.Configs.ToListAsync();
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        // GET: api/Configs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Config>> GetConfig(string id)
        {
            try
            {
                var config = await _context.Configs.FindAsync(id);

                if (config == null)
                {
                    return NotFound();
                }

                return config;
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // PUT: api/Configs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConfig(string id, Config config)
        {
            if (id != config.Key)
            {
                return BadRequest();
            }

            _context.Entry(config).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfigExists(id))
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

        // POST: api/Configs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Config>> PostConfig(Config config)
        {
            try
            {
                _context.Configs.Add(config);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return NoContent();
            }

            return CreatedAtAction("GetConfig", new { id = config.Key }, config);
        }

        // DELETE: api/Configs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfig(string id)
        {
            try
            {
                var config = await _context.Configs.FindAsync(id);
                if (config == null)
                {
                    return NotFound();
                }

                _context.Configs.Remove(config);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return NotFound();
            }

            return NoContent();
        }

        private bool ConfigExists(string id)
        {
            return (_context.Configs?.Any(e => e.Key == id)).GetValueOrDefault();
        }
    }
}
