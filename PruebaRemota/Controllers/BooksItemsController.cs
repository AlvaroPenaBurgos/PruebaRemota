using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaRemota.Models;

namespace PruebaRemota.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksItemsController : ControllerBase
    {
        private readonly BooksContext _context;

        public BooksItemsController(BooksContext context)
        {
            _context = context;
        }

        // GET: api/BooksItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BooksItem>>> GetBooks()
        {
          if (_context.Books == null)
          {
              return NotFound();
          }
            return await _context.Books.ToListAsync();
        }

        // GET: api/BooksItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BooksItem>> GetBooksItem(int id)
        {
          if (_context.Books == null)
          {
              return NotFound();
          }
            var booksItem = await _context.Books.FindAsync(id);

            if (booksItem == null)
            {
                return NotFound();
            }

            return booksItem;
        }

        // PUT: api/BooksItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooksItem(int id, BooksItem booksItem)
        {
            if (id != booksItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(booksItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BooksItemExists(id))
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

        // POST: api/BooksItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BooksItem>> PostBooksItem(BooksItem booksItem)
        {
          if (_context.Books == null)
          {
              return Problem("Entity set 'BooksContext.Books'  is null.");
          }
            _context.Books.Add(booksItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooksItem", new { id = booksItem.Id }, booksItem);
        }

        // DELETE: api/BooksItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooksItem(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var booksItem = await _context.Books.FindAsync(id);
            if (booksItem == null)
            {
                return NotFound();
            }

            _context.Books.Remove(booksItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BooksItemExists(int id)
        {
            return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
