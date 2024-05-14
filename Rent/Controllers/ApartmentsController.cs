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
    public class ApartmentsController : Controller
    {
        private readonly RentalDb3Context _context;

        public ApartmentsController(RentalDb3Context context)
        {
            _context = context;
        }

        // GET: Apartments
        public async Task<IActionResult> Index()
        {
            var apartments = await _context.Apartments.Include(a => a.Property).ToListAsync();
            return View(apartments);
        }
        // GET: Apartments/Search
        public IActionResult Search(int? rentPrice)
        {
            if (!rentPrice.HasValue)
            {
                // Handle the case where rentPrice is not provided
                // You may want to display an error message or redirect the user
                return View("Error");
            }

            // Search for apartments with rent price less than or equal to the specified rentPrice
            var apartments = _context.Apartments.Where(a => a.RentPrice <= rentPrice.Value).ToList();

            return View(apartments); // Passing the list of apartments as the model
        }

        // GET: Apartments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments
                .Include(a => a.Property)
                .FirstOrDefaultAsync(m => m.ApartmentId == id);

            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // GET: Apartments/Create
        public IActionResult Create()
        {
            ViewData["PropertyId"] = new SelectList(_context.Properties, "PropertyId", "PropertyId");
            return View();
        }

        // POST: Apartments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApartmentId,Bedrooms,Bathrooms,RentPrice,PropertyId,Photo,PhotoFile")] Apartment apartment)
        {
           
                if (apartment.PhotoFile != null && apartment.PhotoFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await apartment.PhotoFile.CopyToAsync(memoryStream);
                        apartment.Photo = memoryStream.ToArray();
                    }
                }
            if (ModelState.IsValid)
            {
                ViewData["PropertyId"] = new SelectList(_context.Properties, "PropertyId", "PropertyId", apartment.PropertyId);
                TempData["ErrorMessage"] = "Apartment was not created. Try again!";
                return View(apartment);
            }
            else
            {
                _context.Add(apartment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
                
                
               
        }

        // GET: Apartments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment == null)
            {
                return NotFound();
            }
            ViewData["PropertyId"] = new SelectList(_context.Properties, "PropertyId", "PropertyId", apartment.PropertyId);
            return View(apartment);
        }

        // POST: Apartments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApartmentId,Bedrooms,Bathrooms,RentPrice,PropertyId,Photo,PhotoFile")] Apartment apartment)
        {
            if (id != apartment.ApartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (apartment.PhotoFile != null && apartment.PhotoFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await apartment.PhotoFile.CopyToAsync(memoryStream);
                            apartment.Photo = memoryStream.ToArray();
                        }
                    }
                    //update
                    _context.Update(apartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartmentExists(apartment.ApartmentId))
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
            ViewData["PropertyId"] = new SelectList(_context.Properties, "PropertyId", "PropertyId", apartment.PropertyId);
            return View(apartment);
        }

        // GET: Apartments/ListApartments
        public async Task<IActionResult> ListApartments()
        {
            var apartments = await _context.Apartments.Include(a => a.Property).ToListAsync();
            return View(apartments);
        }

        // GET: Apartments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments
                .Include(a => a.Property)
                .FirstOrDefaultAsync(m => m.ApartmentId == id);

            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // POST: Apartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment != null)
            {
                _context.Apartments.Remove(apartment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ApartmentExists(int id)
        {
            return _context.Apartments.Any(e => e.ApartmentId == id);
        }
    }
}
