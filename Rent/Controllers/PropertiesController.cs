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
    public class PropertiesController : Controller
    {
        private readonly RentalDb3Context _context;

        public PropertiesController(RentalDb3Context context)
        {
            _context = context;
        }

        // GET: Properties
        public async Task<IActionResult> Index()
        {
            var rentalDb3Context = _context.Properties.Include(x => x.Manager).Include(x => x.Owner);
            return View(await rentalDb3Context.ToListAsync());
        }

        // GET: Properties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @property = await _context.Properties
                .Include(x => x.Manager)
                .Include(x => x.Owner)
                .FirstOrDefaultAsync(m => m.PropertyId == id);
            if (@property == null)
            {
                return NotFound();
            }

            return View(@property);
        }

        // GET: Properties/Create
        public IActionResult Create()
        {
            ViewData["ManagerId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        
        // POST: Properties/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PropertyId,Description,Address,City,PostalCode,OwnerId,ManagerId,PhotoFile")] Property property)
        {
            
                // Handle photo upload
                if (property.PhotoFile != null && property.PhotoFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await property.PhotoFile.CopyToAsync(memoryStream);
                        property.Photo = memoryStream.ToArray();
                    }
                }

                if (ModelState.IsValid)
                {

                ViewData["ManagerId"] = new SelectList(_context.Users, "Id", "Id", property.ManagerId);
                ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", property.OwnerId);
                return View(property);
            }
                else
                {
                    // Save the property
                    _context.Add(property);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
           
        }



        // GET: Properties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @property = await _context.Properties.FindAsync(id);
            if (@property == null)
            {
                return NotFound();
            }
            ViewData["ManagerId"] = new SelectList(_context.Users, "Id", "Id", @property.ManagerId);
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", @property.OwnerId);
            return View(@property);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PropertyId,Description,Address,City,PostalCode,OwnerId,ManagerId,PhotoFile")] Property property)
        {
            if (id != property.PropertyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Handle photo upload
                    if (property.PhotoFile != null && property.PhotoFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await property.PhotoFile.CopyToAsync(memoryStream);
                            property.Photo = memoryStream.ToArray();
                        }
                    }

                    // Update the property
                    _context.Update(property);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyExists(property.PropertyId))
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
            ViewData["ManagerId"] = new SelectList(_context.Users, "Id", "Id", property.ManagerId);
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", property.OwnerId);
            return View(property);
        }

        // GET: Properties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @property = await _context.Properties
                .Include(x => x.Manager)
                .Include(x => x.Owner)
                .FirstOrDefaultAsync(m => m.PropertyId == id);
            if (@property == null)
            {
                return NotFound();
            }

            return View(@property);
        }
        // GET: Properties/ListProperties
        public async Task<IActionResult> ListProperties()
        {
            var properties = await _context.Properties
                .Include(x => x.Manager)
                .Include(x => x.Owner)
                .ToListAsync();

            return View(properties);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @property = await _context.Properties.FindAsync(id);
            if (@property != null)
            {
                _context.Properties.Remove(@property);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyExists(int id)
        {
            return _context.Properties.Any(e => e.PropertyId == id);
        }
    }
}
