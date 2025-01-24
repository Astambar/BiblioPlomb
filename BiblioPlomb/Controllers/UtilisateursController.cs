using System.Data;
using BiblioPlomb.Models;
using BiblioPlomb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BiblioPlomb.Controllers
{
    public class UtilisateurController : Controller
    {
        private readonly IUtilisateurService _utilisateurService;
        private readonly IRoleService _roleService;

        public UtilisateurController(IUtilisateurService utilisateurService, IRoleService roleService)
        {
            _utilisateurService = utilisateurService;
            _roleService = roleService;
        }

        public async Task<IActionResult> Index(string searchTerm)
        {
            var utilisateurs = string.IsNullOrWhiteSpace(searchTerm)
                ? await _utilisateurService.GetAllUtilisateursAsync()
                : await _utilisateurService.SearchUtilisateursAsync(searchTerm);

            ViewBag.SearchTerm = searchTerm;
            return View(utilisateurs);
        }

        public async Task<IActionResult> Details(int id)
        {
            var utilisateur = await _utilisateurService.GetUtilisateurByIdAsync(id);
            if (utilisateur == null)
                return NotFound();

            return View(utilisateur);
        }

        public async Task<IActionResult> Create()
        {
            await LoadRolesInViewBag();
            return View();
        }

        [HttpPost("Utilisateur/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string nom, string prenom, string email, string motDePasse, int[] selectedRoles)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var utilisateur = await _utilisateurService.CreateUtilisateurAsync(nom, prenom, email, motDePasse, selectedRoles);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException dbEx)
            {
                ModelState.AddModelError("", dbEx.InnerException?.Message ?? dbEx.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            await LoadRolesInViewBag();
            return View();
        }


        public async Task<IActionResult> Edit(int id)
        {
            var utilisateur = await _utilisateurService.GetUtilisateurByIdAsync(id);
            if (utilisateur == null)
                return NotFound();

            var utilisateurRoles = await _utilisateurService.GetUtilisateurRolesByUtilisateurIdAsync(id);
            ViewBag.UtilisateurRoles = utilisateurRoles.Select(ur => ur.RoleId).ToList();

            await LoadRolesInViewBag();
            return View(utilisateur);
        }

        [HttpPost("Utilisateur/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string nom, string prenom, string email, string motdepasse, int[] selectedRoles)
        {
            try
            {
                await _utilisateurService.UpdateUtilisateurAsync(id, nom, prenom, email, motdepasse, selectedRoles);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dbEx)
            {
                ModelState.AddModelError("", dbEx.InnerException?.Message ?? dbEx.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            var utilisateurRoles = await _utilisateurService.GetUtilisateurRolesByUtilisateurIdAsync(id);
            ViewBag.UtilisateurRoles = utilisateurRoles.Select(ur => ur.RoleId).ToList();

            await LoadRolesInViewBag();
            return View();
        }

        [HttpPost("Utilisateur/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedId = await _utilisateurService.DeleteUtilisateurAsync(id);
            if (!deletedId)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet("{id}/roles")]
        public async Task<IActionResult> GetRolesByUtilisateurId(int id)
        {
            var roles = await _utilisateurService.GetUtilisateurRolesByUtilisateurIdAsync(id);
            if (roles == null)
                return NotFound();
            return Ok(roles);
        }

        [HttpGet("{id}/roles-v2")]
        public async Task<IActionResult> GetRolesByUtilisateurIdNew(int id)
        {
            var roles = await _utilisateurService.GetUtilisateurRolesByUtilisateurIdAsync(id);
            if (roles == null)
                return NotFound();
            return Ok(roles);
        }

        private async Task LoadRolesInViewBag()
        {
            var roles = await _roleService.GetAllRolesAsync();
            if (roles == null)
            {
                throw new InvalidOperationException("Roles could not be loaded.");
            }
            ViewBag.Roles = new SelectList(roles, "Id", "Type");
        }

    }
}