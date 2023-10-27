using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shate.DAL;
using Shate.DAL.EF;
using Shate.DAL.Entities;
using Shate.DAL.Repositorys;

namespace Back_end_mvc.Controllers
{
    public class ComputersController : Controller
    {
        private readonly ComputerRepository _context;

        public ComputersController(ComputerRepository context)
        {
            _context = context;
        }

        // GET: Computers
        public async Task<IActionResult> Index()
        {
              return _context != null ? 
                          Json(_context.FindAll()) :
                          Problem("Entity set 'PostgreDbContext.Computers'  is null.");
        }

        // GET: Computers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context == null)
            {
                return NotFound();
            }

            var computer = _context
                .FindByCondition(m => m.Id == id);
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
                _context.AddRecord(computer);
                _context.UnitOfWork.Save();
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
            if (id == null || _context == null)
            {
                return NotFound();
            }

            var computer = _context.FindByCondition(e => e.Id == id);
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
                    _context.UpdateRecord(computer);
                    _context.UnitOfWork.Save();
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
            if (id == null || _context == null)
            {
                return NotFound();
            }

            var computer = _context
                .FindByCondition(m => m.Id == id);
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
            if (_context.UnitOfWork.Save == null)
            {
                return Problem("Entity set 'PostgreDbContext.Computers'  is null.");
            }
            var computer = _context.FindByCondition(e => e.Id == id).FirstOrDefault();
            if (computer != null)
            {
                _context.DeleteRecord(computer);
            }
            
            _context.UnitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool ComputerExists(Guid id)
        {
	        return (_context?.Table.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
