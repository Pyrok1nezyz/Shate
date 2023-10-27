using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shate.DAL.EF;
using Shate.DAL.Entities;

namespace Back_end_mvc.Controllers
{
    public class ItemsController : Controller
    {
        private readonly PostgreDbContext _context;

        public ItemsController(PostgreDbContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        { 
	        return _context.Items != null ? 
                           Json( await _context.Items.Include(e => e.MainCategory).Include(e => e.SubCategory).ToListAsync()) :
                           Problem("Entity set 'PostgreDbContext.Items'  is null.");
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id != Guid.Empty || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items.Include(e => e.MainCategory).Include(e => e.SubCategory)
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
            if (item == null)
            {
                return NotFound();
            }

            return Json(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
	        return BadRequest();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Count,IsForceBuy,CountryId,IsDiscounted,IsDeleted,IsHided,CustomerId,id")] Item item)
        {
            if (ModelState.IsValid && !_context.Items.Any(e => e.Id == item.Id))
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
	            return StatusCode(StatusCodes.Status409Conflict);
            }
        }

        // GET: Items/Edit/5
        public IActionResult Edit(Guid id)
        {
	        return StatusCode(StatusCodes.Status405MethodNotAllowed);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Price,Count,IsForceBuy,CountryId,IsDiscounted,IsDeleted,IsHided,CustomerId,id")] Item item)
        {
            if (string.Equals(id, item.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty || _context.Items == null)
            {
                return NotFound();
            }

			var item = await _context.Items.Include(e => e.MainCategory).Include(e => e.SubCategory)
				.FirstOrDefaultAsync(m => m.Id.Equals(id));

			if (item == null)
            {
                return NotFound();
            }

            return Ok();
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'PostgreDbContext.Items'  is null.");
            }
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(Guid id)
        {
          return (_context.Items?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
