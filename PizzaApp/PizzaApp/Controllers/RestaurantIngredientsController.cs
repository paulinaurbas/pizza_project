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
    public class RestaurantIngredientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RestaurantIngredientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RestaurantIngredients
        public async Task<IActionResult> Index(int restaurantId)
        {
            var restaurantIngredients = await _context.RestaurantIngredients
                .Include(r => r.Ingredient)
                .Include(r => r.Restaurant)
                .Where(r => r.RestaurantId == restaurantId)
                .ToListAsync();

            var resturant = await _context.Restaurants.FindAsync(restaurantId);

            ViewData["CurrentRestaurant"] = resturant;

            return View(restaurantIngredients);
        }

        // GET: RestaurantIngredients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantIngredient = await _context.RestaurantIngredients
                .Include(r => r.Ingredient)
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurantIngredient == null)
            {
                return NotFound();
            }

            return View(restaurantIngredient);
        }

        // GET: RestaurantIngredients/Create
        public IActionResult Create(int restaurantId)
        {
            var restaurantIngredient = new RestaurantIngredient()
            {
                RestaurantId = restaurantId
            };

            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name");

            return View(restaurantIngredient);
        }

        // POST: RestaurantIngredients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RestaurantId,IngredientId,Quantity")] RestaurantIngredient restaurantIngredient)
        {
            if (ModelState.IsValid)
            {
                var existingRestaurantIngredient = await _context.RestaurantIngredients
                    .Where(e => e.IngredientId == restaurantIngredient.IngredientId && e.RestaurantId == restaurantIngredient.RestaurantId).FirstOrDefaultAsync();

                if (existingRestaurantIngredient != null)
				{
                    existingRestaurantIngredient.Quantity += restaurantIngredient.Quantity;
                }
				else
				{
                    _context.Add(restaurantIngredient);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { restaurantId = restaurantIngredient.RestaurantId });
            }

            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name", restaurantIngredient.IngredientId);

            return View(restaurantIngredient);
        }

        // GET: RestaurantIngredients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantIngredient = await _context.RestaurantIngredients.FindAsync(id);

            if (restaurantIngredient == null)
            {
                return NotFound();
            }

            return View(restaurantIngredient);
        }

        // POST: RestaurantIngredients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RestaurantId,IngredientId,Quantity")] RestaurantIngredient restaurantIngredient)
        {
            if (id != restaurantIngredient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurantIngredient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantIngredientExists(restaurantIngredient.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { restaurantId = restaurantIngredient.RestaurantId });
            }

            return View(restaurantIngredient);
        }

        // GET: RestaurantIngredients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantIngredient = await _context.RestaurantIngredients
                .Include(r => r.Ingredient)
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurantIngredient == null)
            {
                return NotFound();
            }

            return View(restaurantIngredient);
        }

        // POST: RestaurantIngredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restaurantIngredient = await _context.RestaurantIngredients.FindAsync(id);
            _context.RestaurantIngredients.Remove(restaurantIngredient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { restaurantId = restaurantIngredient.RestaurantId });
        }

        private bool RestaurantIngredientExists(int id)
        {
            return _context.RestaurantIngredients.Any(e => e.Id == id);
        }
    }
}
