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
    public class OrderMenuItemDishesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderMenuItemDishesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderMenuItemDishes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OrderMenuItemDishes.Include(o => o.MenuItemDish).Include(o => o.OrderMenuItem);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OrderMenuItemDishes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderMenuItemDish = await _context.OrderMenuItemDishes
                .Include(o => o.MenuItemDish)
                .Include(o => o.OrderMenuItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderMenuItemDish == null)
            {
                return NotFound();
            }

            return View(orderMenuItemDish);
        }

        // GET: OrderMenuItemDishes/Create
        public IActionResult Create()
        {
            ViewData["MenuItemDishId"] = new SelectList(_context.MenuItemDishes, "Id", "Id");
            ViewData["OrderMenuItemId"] = new SelectList(_context.OrderMenuItems, "Id", "Id");
            return View();
        }

        // POST: OrderMenuItemDishes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderMenuItemId,MenuItemDishId")] OrderMenuItemDish orderMenuItemDish)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderMenuItemDish);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuItemDishId"] = new SelectList(_context.MenuItemDishes, "Id", "Id", orderMenuItemDish.MenuItemDishId);
            ViewData["OrderMenuItemId"] = new SelectList(_context.OrderMenuItems, "Id", "Id", orderMenuItemDish.OrderMenuItemId);
            return View(orderMenuItemDish);
        }

        // GET: OrderMenuItemDishes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderMenuItemDish = await _context.OrderMenuItemDishes.FindAsync(id);
            if (orderMenuItemDish == null)
            {
                return NotFound();
            }
            ViewData["MenuItemDishId"] = new SelectList(_context.MenuItemDishes, "Id", "Id", orderMenuItemDish.MenuItemDishId);
            ViewData["OrderMenuItemId"] = new SelectList(_context.OrderMenuItems, "Id", "Id", orderMenuItemDish.OrderMenuItemId);
            return View(orderMenuItemDish);
        }

        // POST: OrderMenuItemDishes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderMenuItemId,MenuItemDishId")] OrderMenuItemDish orderMenuItemDish)
        {
            if (id != orderMenuItemDish.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderMenuItemDish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderMenuItemDishExists(orderMenuItemDish.Id))
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
            ViewData["MenuItemDishId"] = new SelectList(_context.MenuItemDishes, "Id", "Id", orderMenuItemDish.MenuItemDishId);
            ViewData["OrderMenuItemId"] = new SelectList(_context.OrderMenuItems, "Id", "Id", orderMenuItemDish.OrderMenuItemId);
            return View(orderMenuItemDish);
        }

        // GET: OrderMenuItemDishes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderMenuItemDish = await _context.OrderMenuItemDishes
                .Include(o => o.MenuItemDish)
                .Include(o => o.OrderMenuItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderMenuItemDish == null)
            {
                return NotFound();
            }

            return View(orderMenuItemDish);
        }

        // POST: OrderMenuItemDishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderMenuItemDish = await _context.OrderMenuItemDishes.FindAsync(id);
            _context.OrderMenuItemDishes.Remove(orderMenuItemDish);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderMenuItemDishExists(int id)
        {
            return _context.OrderMenuItemDishes.Any(e => e.Id == id);
        }
    }
}
