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
    public class InformationAbautNewProdactsController : Controller
    {
        private readonly OrderDbConenction _context;

        public InformationAbautNewProdactsController(OrderDbConenction context)
        {
            _context = context;
        }

        // GET: InformationAbautNewProdacts
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
              return _context.InformationAbautNewProdact != null ? 
                          View(await _context.InformationAbautNewProdact.ToListAsync()) :
                          Problem("Entity set 'OrderDbConenction.InformationAbautNewProdact'  is null.");
        }

        // GET: InformationAbautNewProdacts/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InformationAbautNewProdact == null)
            {
                return NotFound();
            }

            var informationAbautNewProdact = await _context.InformationAbautNewProdact
                .FirstOrDefaultAsync(m => m.Id == id);
            if (informationAbautNewProdact == null)
            {
                return NotFound();
            }

            return View(informationAbautNewProdact);
        }

        // GET: InformationAbautNewProdacts/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: InformationAbautNewProdacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Brend,Name,Image,Page,AditionalInformation")] InformationAbautNewProdact informationAbautNewProdact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(informationAbautNewProdact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(informationAbautNewProdact);
        }

        // GET: InformationAbautNewProdacts/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InformationAbautNewProdact == null)
            {
                return NotFound();
            }

            var informationAbautNewProdact = await _context.InformationAbautNewProdact.FindAsync(id);
            if (informationAbautNewProdact == null)
            {
                return NotFound();
            }
            return View(informationAbautNewProdact);
        }

        // POST: InformationAbautNewProdacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Brend,Name,Image,Page,AditionalInformation")] InformationAbautNewProdact informationAbautNewProdact)
        {
            if (id != informationAbautNewProdact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(informationAbautNewProdact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InformationAbautNewProdactExists(informationAbautNewProdact.Id))
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
            return View(informationAbautNewProdact);
        }

        // GET: InformationAbautNewProdacts/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InformationAbautNewProdact == null)
            {
                return NotFound();
            }

            var informationAbautNewProdact = await _context.InformationAbautNewProdact
                .FirstOrDefaultAsync(m => m.Id == id);
            if (informationAbautNewProdact == null)
            {
                return NotFound();
            }

            return View(informationAbautNewProdact);
        }

        // POST: InformationAbautNewProdacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InformationAbautNewProdact == null)
            {
                return Problem("Entity set 'OrderDbConenction.InformationAbautNewProdact'  is null.");
            }
            var informationAbautNewProdact = await _context.InformationAbautNewProdact.FindAsync(id);
            if (informationAbautNewProdact != null)
            {
                _context.InformationAbautNewProdact.Remove(informationAbautNewProdact);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InformationAbautNewProdactExists(int id)
        {
          return (_context.InformationAbautNewProdact?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
