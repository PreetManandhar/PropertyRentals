using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rent.Models;

namespace Rent.Controllers
{
    public class StatusModelsController : Controller
    {
        private readonly RentalDb3Context _context;

        public StatusModelsController(RentalDb3Context context)
        {
            _context = context;
        }

        // GET: StatusModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.StatusModel.ToListAsync());
        }

        // GET: StatusModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusModel = await _context.StatusModel
                .FirstOrDefaultAsync(m => m.StatusId == id);
            if (statusModel == null)
            {
                return NotFound();
            }

            return View(statusModel);
        }

        // GET: StatusModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatusModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatusId,StatusType,ManagerId,ApartmentId")] StatusModel statusModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statusModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statusModel);
        }

        // GET: StatusModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusModel = await _context.StatusModel.FindAsync(id);
            if (statusModel == null)
            {
                return NotFound();
            }
            return View(statusModel);
        }

        // POST: StatusModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StatusId,StatusType,ManagerId,ApartmentId")] StatusModel statusModel)
        {
            if (id != statusModel.StatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statusModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusModelExists(statusModel.StatusId))
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
            return View(statusModel);
        }

        // GET: StatusModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusModel = await _context.StatusModel
                .FirstOrDefaultAsync(m => m.StatusId == id);
            if (statusModel == null)
            {
                return NotFound();
            }

            return View(statusModel);
        }

        // POST: StatusModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var statusModel = await _context.StatusModel.FindAsync(id);
            if (statusModel != null)
            {
                _context.StatusModel.Remove(statusModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusModelExists(int id)
        {
            return _context.StatusModel.Any(e => e.StatusId == id);
        }
    }
}
