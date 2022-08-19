using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    public class PerchaseModelsController : Controller
    {
        private readonly OrderDbConenction _context;

        public PerchaseModelsController(OrderDbConenction context)
        {
            _context = context;
        }

        // GET: PerchaseModels
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
              return _context.AllPerchaseItems != null ? 
                          View(await _context.AllPerchaseItems.ToListAsync()) :
                          Problem("Entity set 'OrderDbConenction.AllPerchaseItems'  is null.");
        }

        // GET: PerchaseModels/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AllPerchaseItems == null)
            {
                return NotFound();
            }

            var perchaseModel = await _context.AllPerchaseItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perchaseModel == null)
            {
                return NotFound();
            }

            return View(perchaseModel);
        }

        // GET: PerchaseModels/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: PerchaseModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Brend,Price,Discount,Image,Colour,Size,Page,Type,Property,MinMaxPrice")] PurchaseModel perchaseModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(perchaseModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(perchaseModel);
        }

        // GET: PerchaseModels/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AllPerchaseItems == null)
            {
                return NotFound();
            }

            var perchaseModel = await _context.AllPerchaseItems.FindAsync(id);
            if (perchaseModel == null)
            {
                return NotFound();
            }
            return View(perchaseModel);
        }

        // POST: PerchaseModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Brend,Price,Discount,Image,Colour,Size,Page,Type,Property,MinMaxPrice")] PurchaseModel perchaseModel)
        {
            if (id != perchaseModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(perchaseModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerchaseModelExists(perchaseModel.Id))
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
            return View(perchaseModel);
        }

        // GET: PerchaseModels/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AllPerchaseItems == null)
            {
                return NotFound();
            }

            var perchaseModel = await _context.AllPerchaseItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perchaseModel == null)
            {
                return NotFound();
            }

            return View(perchaseModel);
        }

        // POST: PerchaseModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AllPerchaseItems == null)
            {
                return Problem("Entity set 'OrderDbConenction.AllPerchaseItems'  is null.");
            }
            var perchaseModel = await _context.AllPerchaseItems.FindAsync(id);
            if (perchaseModel != null)
            {
                _context.AllPerchaseItems.Remove(perchaseModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerchaseModelExists(int id)
        {
          return (_context.AllPerchaseItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
