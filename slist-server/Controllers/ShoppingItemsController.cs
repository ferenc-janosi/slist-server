#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using slist_server.Data;
using slist_server.Data.Models;

namespace slist_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingItemsController : ControllerBase
    {
        private readonly SListDbContext _context;

        public ShoppingItemsController(SListDbContext context)
        {
            _context = context;
        }

        // GET: api/ShoppingItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingItem>>> GetShoppingItem()
        {
            return await _context.ShoppingItem.ToListAsync();
        }

        // GET: api/ShoppingItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingItem>> GetShoppingItem(Guid id)
        {
            var shoppingItem = await _context.ShoppingItem.FindAsync(id);

            if (shoppingItem == null)
            {
                return NotFound();
            }

            return shoppingItem;
        }

        // PUT: api/ShoppingItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingItem(Guid id, ShoppingItem shoppingItem)
        {
            if (id != shoppingItem.ID)
            {
                return BadRequest();
            }

            _context.Entry(shoppingItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ShoppingItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShoppingItem>> PostShoppingItem(ShoppingItem shoppingItem)
        {
            _context.ShoppingItem.Add(shoppingItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoppingItem", new { id = shoppingItem.ID }, shoppingItem);
        }

        // DELETE: api/ShoppingItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingItem(Guid id)
        {
            var shoppingItem = await _context.ShoppingItem.FindAsync(id);
            if (shoppingItem == null)
            {
                return NotFound();
            }

            _context.ShoppingItem.Remove(shoppingItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShoppingItemExists(Guid id)
        {
            return _context.ShoppingItem.Any(e => e.ID == id);
        }
    }
}
