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
    public class OrderMenuItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderMenuItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderMenuItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OrderMenuItems.Include(o => o.MenuItem).Include(o => o.Order);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OrderMenuItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderMenuItem = await _context.OrderMenuItems
                .Include(o => o.MenuItem)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderMenuItem == null)
            {
                return NotFound();
            }

            return View(orderMenuItem);
        }

        // GET: OrderMenuItems/Create
        public IActionResult Create()
        {
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Description");
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Description");
            return View();
        }

        // POST: OrderMenuItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderId,MenuItemId,Quantity")] OrderMenuItem orderMenuItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderMenuItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Description", orderMenuItem.MenuItemId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Description", orderMenuItem.OrderId);
            return View(orderMenuItem);
        }

        // GET: OrderMenuItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderMenuItem = await _context.OrderMenuItems.FindAsync(id);
            if (orderMenuItem == null)
            {
                return NotFound();
            }
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Description", orderMenuItem.MenuItemId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Description", orderMenuItem.OrderId);
            return View(orderMenuItem);
        }

        // POST: OrderMenuItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderId,MenuItemId,Quantity")] OrderMenuItem orderMenuItem)
        {
            if (id != orderMenuItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderMenuItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderMenuItemExists(orderMenuItem.Id))
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
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Description", orderMenuItem.MenuItemId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Description", orderMenuItem.OrderId);
            return View(orderMenuItem);
        }

        // GET: OrderMenuItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderMenuItem = await _context.OrderMenuItems
                .Include(o => o.MenuItem)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderMenuItem == null)
            {
                return NotFound();
            }

            return View(orderMenuItem);
        }

        // POST: OrderMenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderMenuItem = await _context.OrderMenuItems.FindAsync(id);
            _context.OrderMenuItems.Remove(orderMenuItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderMenuItemExists(int id)
        {
            return _context.OrderMenuItems.Any(e => e.Id == id);
        }
    }
}
