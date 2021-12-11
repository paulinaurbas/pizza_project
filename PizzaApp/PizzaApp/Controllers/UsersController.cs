using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Data;
using PizzaApp.Models.Authorization;
using PizzaApp.ViewModels.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<ApplicationRole> _roleManager;

        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;

            _userManager = userManager;

            _roleManager = roleManager;
        }

        // GET: UserController
        public async Task<ActionResult> Index()
        {
            var users = await _context.Users
                .Include(e => e.UserRoles).ThenInclude(x => x.Role)
                .OrderBy(e => e.UserName)
                .ToListAsync();

            var userViewModels = new List<ApplicationUserViewModel>();

            foreach (var user in users)
            {
                var userViewModel = new ApplicationUserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                };

                foreach (var userRole in user.UserRoles)
                {
                    userViewModel.ApplicationRoleViewModels.Add(new ApplicationRoleViewModel
                    {
                        Id = userRole.RoleId,
                        Name = userRole.Role.Name
                    });
                }

                userViewModels.Add(userViewModel);
            }

            var roles = await _context.Roles.Select(e => new ApplicationRoleViewModel
            {
                Id = e.Id,
                Name = e.Name
            }).ToListAsync();

            ViewData["Roles"] = roles;

            return View(userViewModels);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var user = await _context.Users.Select(e => new ApplicationUserViewModel
            {
                Id = e.Id,
                UserName = e.UserName,
                Email = e.Email,
            }).FirstOrDefaultAsync(e => e.Id == id);

            return View(user);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                var user = _context.Users.Find(id);

                await _userManager.DeleteAsync(user);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/AddUserRole
        public async Task<ActionResult> AddUserRole(string userId, string roleId)
        {
            var user = _context.Users.Find(userId);

            var role = _context.Roles.Find(roleId);

            await _userManager.AddToRoleAsync(user, role.Name);

            return RedirectToAction(nameof(Index));
        }

        // GET: UserController/RemoveUserRole
        public async Task<ActionResult> RemoveUserRole(string userId, string roleId)
        {
            var user = _context.Users.Find(userId);

            var role = _context.Roles.Find(roleId);

            await _userManager.RemoveFromRoleAsync(user, role.Name);

            return RedirectToAction(nameof(Index));
        }
    }
}
