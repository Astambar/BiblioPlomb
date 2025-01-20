using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BiblioPlomb.Data;
using BiblioPlomb.Models;

namespace BiblioPlomb.Controllers
{
    public class RolesController : Controller
    {
        private readonly BiblioPlombContext _context;

        public RolesController(BiblioPlombContext context)
        {
            _context = context;
        }

        // GET: Roles
        public async Task<IActionResult> Index()
        {
            var roles = await _context.Roles.ToListAsync();
            return View(roles);
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Type")] Role role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Vérifier si le rôle existe déjà
                    var roleExists = await _context.Roles
                        .AnyAsync(r => r.Type.ToLower() == role.Type.ToLower());

                    if (roleExists)
                    {
                        ModelState.AddModelError("Type", "Ce type de rôle existe déjà");
                        return View(role);
                    }

                    _context.Add(role);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = $"Le rôle '{role.Type}' a été créé avec succès.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Une erreur s'est produite lors de la création du rôle. Veuillez réessayer.");
                // Log l'erreur ici si nécessaire
            }
            return View(role);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type")] Role role)
        {
            if (id != role.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Vérifier si le nouveau type existe déjà pour un autre rôle
                    var existingRole = await _context.Roles
                        .FirstOrDefaultAsync(r => r.Type.ToLower() == role.Type.ToLower() && r.Id != role.Id);

                    if (existingRole != null)
                    {
                        ModelState.AddModelError("Type", "Ce type de rôle existe déjà");
                        return View(role);
                    }

                    _context.Update(role);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = $"Le rôle '{role.Type}' a été modifié avec succès.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(role.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(role);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"Le rôle '{role.Type}' a été supprimé avec succès.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(int id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}
