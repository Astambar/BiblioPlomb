﻿using BiblioPlomb.Services;
using Microsoft.AspNetCore.Mvc;
using BiblioPlomb.Models;

namespace BiblioPlomb.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: Role
        public async Task<IActionResult> Index(string searchTerm)
        {
            var roles = string.IsNullOrWhiteSpace(searchTerm)
            ? await _roleService.GetAllRolesAsync()
            : await _roleService.SearchRolesByTypeAsync(searchTerm);

            ViewBag.SearchTerm = searchTerm;
            return View(roles);
        }

        // GET: Role/Details/id
        public async Task<IActionResult> Details(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // GET: Role/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Type")] string type)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _roleService.CreateRoleAsync(type);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedId = await _roleService.DeleteRoleAsync(id);
            if (!deletedId)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
