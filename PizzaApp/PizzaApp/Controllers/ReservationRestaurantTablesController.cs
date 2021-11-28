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
    public class ReservationRestaurantTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationRestaurantTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReservationRestaurantTables
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ReservationRestaurantTables.Include(r => r.Reservation).Include(r => r.RestaurantTable);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ReservationRestaurantTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationRestaurantTable = await _context.ReservationRestaurantTables
                .Include(r => r.Reservation)
                .Include(r => r.RestaurantTable)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservationRestaurantTable == null)
            {
                return NotFound();
            }

            return View(reservationRestaurantTable);
        }

        // GET: ReservationRestaurantTables/Create
        public IActionResult Create()
        {
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Id");
            ViewData["RestaurantTableId"] = new SelectList(_context.RestaurantTables, "Id", "Name");
            return View();
        }

        // POST: ReservationRestaurantTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReservationId,RestaurantTableId")] ReservationRestaurantTable reservationRestaurantTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservationRestaurantTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Id", reservationRestaurantTable.ReservationId);
            ViewData["RestaurantTableId"] = new SelectList(_context.RestaurantTables, "Id", "Name", reservationRestaurantTable.RestaurantTableId);
            return View(reservationRestaurantTable);
        }

        // GET: ReservationRestaurantTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationRestaurantTable = await _context.ReservationRestaurantTables.FindAsync(id);
            if (reservationRestaurantTable == null)
            {
                return NotFound();
            }
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Id", reservationRestaurantTable.ReservationId);
            ViewData["RestaurantTableId"] = new SelectList(_context.RestaurantTables, "Id", "Name", reservationRestaurantTable.RestaurantTableId);
            return View(reservationRestaurantTable);
        }

        // POST: ReservationRestaurantTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReservationId,RestaurantTableId")] ReservationRestaurantTable reservationRestaurantTable)
        {
            if (id != reservationRestaurantTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservationRestaurantTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationRestaurantTableExists(reservationRestaurantTable.Id))
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
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Id", reservationRestaurantTable.ReservationId);
            ViewData["RestaurantTableId"] = new SelectList(_context.RestaurantTables, "Id", "Name", reservationRestaurantTable.RestaurantTableId);
            return View(reservationRestaurantTable);
        }

        // GET: ReservationRestaurantTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationRestaurantTable = await _context.ReservationRestaurantTables
                .Include(r => r.Reservation)
                .Include(r => r.RestaurantTable)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservationRestaurantTable == null)
            {
                return NotFound();
            }

            return View(reservationRestaurantTable);
        }

        // POST: ReservationRestaurantTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservationRestaurantTable = await _context.ReservationRestaurantTables.FindAsync(id);
            _context.ReservationRestaurantTables.Remove(reservationRestaurantTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationRestaurantTableExists(int id)
        {
            return _context.ReservationRestaurantTables.Any(e => e.Id == id);
        }
    }
}
