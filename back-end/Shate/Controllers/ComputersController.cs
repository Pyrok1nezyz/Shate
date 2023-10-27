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
    public class ComputersController : Controller
    {
        private readonly PostgreDbContext _context;

        public ComputersController(PostgreDbContext context)
        {
            _context = context;
        }

        // GET: Computers
        public async Task<IActionResult> Index()
        {
              return _context.Computers != null ? 
                          Json(await _context.Computers.ToListAsync()) :
                          Problem("Entity set 'PostgreDbContext.Computers'  is null.");
        }

        // GET: Computers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Computers == null)
            {
                return NotFound();
            }

            var computer = await _context.Computers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (computer == null)
            {
                return NotFound();
            }

            return Json(computer);
        }

        // GET: Computers/Create
        public IActionResult Create()
        {
            return BadRequest();
        }

        // POST: Computers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Ip,Props,Notation,Id")] Computer computer)
        {
            if (ModelState.IsValid)
            {
                computer.Id = Guid.NewGuid();
                _context.Add(computer);
                await _context.SaveChangesAsync();
				return Ok();
            }
            else
            {
	            return StatusCode(StatusCodes.Status409Conflict);
            }
		}

        // GET: Computers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Computers == null)
            {
                return NotFound();
            }

            var computer = await _context.Computers.FindAsync(id);
            if (computer == null)
            {
                return NotFound();
            }
            return Json(computer);
        }

        // POST: Computers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Ip,Props,Notation,Id")] Computer computer)
        {
            if (id != computer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(computer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComputerExists(computer.Id))
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
            return Json(computer);
        }

        // GET: Computers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Computers == null)
            {
                return NotFound();
            }

            var computer = await _context.Computers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (computer == null)
            {
                return NotFound();
            }

            return Json(computer);
        }

        // POST: Computers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Computers == null)
            {
                return Problem("Entity set 'PostgreDbContext.Computers'  is null.");
            }
            var computer = await _context.Computers.FindAsync(id);
            if (computer != null)
            {
                _context.Computers.Remove(computer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComputerExists(Guid id)
        {
          return (_context.Computers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
