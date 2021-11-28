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
    public class MenuItemDishesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenuItemDishesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MenuItemDishes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MenuItemDishes.Include(m => m.Dish).Include(m => m.MenuItem);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MenuItemDishes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItemDish = await _context.MenuItemDishes
                .Include(m => m.Dish)
                .Include(m => m.MenuItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuItemDish == null)
            {
                return NotFound();
            }

            return View(menuItemDish);
        }

        // GET: MenuItemDishes/Create
        public IActionResult Create()
        {
            ViewData["DishId"] = new SelectList(_context.Dishes, "Id", "Name");
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Description");
            return View();
        }

        // POST: MenuItemDishes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DishId,MenuItemId,Quantity,IsOptional,AdditionalPrice")] MenuItemDish menuItemDish)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuItemDish);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DishId"] = new SelectList(_context.Dishes, "Id", "Name", menuItemDish.DishId);
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Description", menuItemDish.MenuItemId);
            return View(menuItemDish);
        }

        // GET: MenuItemDishes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItemDish = await _context.MenuItemDishes.FindAsync(id);
            if (menuItemDish == null)
            {
                return NotFound();
            }
            ViewData["DishId"] = new SelectList(_context.Dishes, "Id", "Name", menuItemDish.DishId);
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Description", menuItemDish.MenuItemId);
            return View(menuItemDish);
        }

        // POST: MenuItemDishes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DishId,MenuItemId,Quantity,IsOptional,AdditionalPrice")] MenuItemDish menuItemDish)
        {
            if (id != menuItemDish.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuItemDish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuItemDishExists(menuItemDish.Id))
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
            ViewData["DishId"] = new SelectList(_context.Dishes, "Id", "Name", menuItemDish.DishId);
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Description", menuItemDish.MenuItemId);
            return View(menuItemDish);
        }

        // GET: MenuItemDishes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItemDish = await _context.MenuItemDishes
                .Include(m => m.Dish)
                .Include(m => m.MenuItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuItemDish == null)
            {
                return NotFound();
            }

            return View(menuItemDish);
        }

        // POST: MenuItemDishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuItemDish = await _context.MenuItemDishes.FindAsync(id);
            _context.MenuItemDishes.Remove(menuItemDish);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuItemDishExists(int id)
        {
            return _context.MenuItemDishes.Any(e => e.Id == id);
        }
    }
}
