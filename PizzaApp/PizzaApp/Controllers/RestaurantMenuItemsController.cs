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
    public class RestaurantMenuItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RestaurantMenuItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RestaurantMenuItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RestaurantMenuItems.Include(r => r.MenuItem).Include(r => r.Restaurant);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RestaurantMenuItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantMenuItem = await _context.RestaurantMenuItems
                .Include(r => r.MenuItem)
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurantMenuItem == null)
            {
                return NotFound();
            }

            return View(restaurantMenuItem);
        }

        // GET: RestaurantMenuItems/Create
        public IActionResult Create()
        {
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Description");
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Address");
            return View();
        }

        // POST: RestaurantMenuItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RestaurantId,MenuItemId")] RestaurantMenuItem restaurantMenuItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restaurantMenuItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Description", restaurantMenuItem.MenuItemId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Address", restaurantMenuItem.RestaurantId);
            return View(restaurantMenuItem);
        }

        // GET: RestaurantMenuItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantMenuItem = await _context.RestaurantMenuItems.FindAsync(id);
            if (restaurantMenuItem == null)
            {
                return NotFound();
            }
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Description", restaurantMenuItem.MenuItemId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Address", restaurantMenuItem.RestaurantId);
            return View(restaurantMenuItem);
        }

        // POST: RestaurantMenuItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RestaurantId,MenuItemId")] RestaurantMenuItem restaurantMenuItem)
        {
            if (id != restaurantMenuItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurantMenuItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantMenuItemExists(restaurantMenuItem.Id))
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
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Description", restaurantMenuItem.MenuItemId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Address", restaurantMenuItem.RestaurantId);
            return View(restaurantMenuItem);
        }

        // GET: RestaurantMenuItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantMenuItem = await _context.RestaurantMenuItems
                .Include(r => r.MenuItem)
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurantMenuItem == null)
            {
                return NotFound();
            }

            return View(restaurantMenuItem);
        }

        // POST: RestaurantMenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restaurantMenuItem = await _context.RestaurantMenuItems.FindAsync(id);
            _context.RestaurantMenuItems.Remove(restaurantMenuItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantMenuItemExists(int id)
        {
            return _context.RestaurantMenuItems.Any(e => e.Id == id);
        }
    }
}
