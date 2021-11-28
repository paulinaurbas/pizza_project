using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Data;
using PizzaApp.Models;

namespace PizzaApp.Controllers
{
    public class WholesalersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WholesalersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Wholesalers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Wholesalers.ToListAsync());
        }

        // GET: Wholesalers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wholesaler = await _context.Wholesalers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wholesaler == null)
            {
                return NotFound();
            }

            return View(wholesaler);
        }

        // GET: Wholesalers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wholesalers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,Email,Telephone")] Wholesaler wholesaler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wholesaler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wholesaler);
        }

        // GET: Wholesalers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wholesaler = await _context.Wholesalers.FindAsync(id);
            if (wholesaler == null)
            {
                return NotFound();
            }
            return View(wholesaler);
        }

        // POST: Wholesalers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,Email,Telephone")] Wholesaler wholesaler)
        {
            if (id != wholesaler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wholesaler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WholesalerExists(wholesaler.Id))
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
            return View(wholesaler);
        }

        // GET: Wholesalers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wholesaler = await _context.Wholesalers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wholesaler == null)
            {
                return NotFound();
            }

            return View(wholesaler);
        }

        // POST: Wholesalers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wholesaler = await _context.Wholesalers.FindAsync(id);
            _context.Wholesalers.Remove(wholesaler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WholesalerExists(int id)
        {
            return _context.Wholesalers.Any(e => e.Id == id);
        }
    }
}
