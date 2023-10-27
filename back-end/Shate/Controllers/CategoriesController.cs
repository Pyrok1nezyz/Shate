using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shate.DAL;
using Shate.DAL.Entities;
using Shate.DAL.Repositorys;

namespace Back_end_mvc.Controllers
{
    public class CategoriesController : Controller
    {
	    private CategoryRepository _context;

	    public CategoriesController(CategoryRepository context)
	    {
		    _context = context;
	    }

		// GET: Categories
		public async Task<IActionResult> Index()
        {
              return _context != null ? 
                          Json(_context.FindAll()) :
                          Problem("Entity set 'PostgreDbContext.Categories'  is null.");
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context == null)
            {
                return NotFound();
            }

            var category = _context
                .FindByCondition(e => e.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return Json(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
	        return BadRequest();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ParentId,IsMain,Id")] Category category)
        {
            if (ModelState.IsValid)
            {
	            await _context.AddRecord(category);
                return RedirectToAction(nameof(Index));
            }
            return Json(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context == null)
            {
                return NotFound();
            }

            var category = _context.FindByCondition(e => e.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return Json(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,ParentId,IsMain,Id")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.UpdateRecord(category);
                    _context.UnitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return Json(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context == null)
            {
                return NotFound();
            }

            var category = _context
	            .FindByCondition(e => e.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return Json(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context == null)
            {
                return Problem("Entity set 'PostgreDbContext.Categories'  is null.");
            }

            var category = await _context.FindByCondition(e => e.Id == id).FirstOrDefaultAsync();
            if (category != null)
            {
                _context.DeleteRecord(category);
            }

            _context.UnitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(Guid id)
        {
          return (_context?.Table.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
