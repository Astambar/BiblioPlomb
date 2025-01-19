using BiblioPlomb.Services;
using Biblioplomb.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BiblioPlomb.DTO;

namespace BiblioPlomb.Controllers
{
    public class BibliothequeController : Controller
    {
        private readonly LivreService _livreService;
        private readonly GenreService _genreService;

        public BibliothequeController(LivreService livreService, GenreService genreService)
        {
            _livreService = livreService;
            _genreService = genreService;
        }

        // Lister tous les livres
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var livres = await _livreService.GetAllLivres();
            return View(livres);
        }

        // Détails du livre
        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var livre = await _livreService.GetLivre(id);
            return livre == null ? NotFound() : View(livre);
        }

        // Formulaire de création
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // Créer un livre
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LivreDTO livreDTO)
        {
            if (ModelState.IsValid)
            {
                await _livreService.AddLivre(livreDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(livreDTO);
        }

        // Formulaire de modification
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var livre = await _livreService.GetLivre(id);
            return livre == null ? NotFound() : View(livre);
        }

        // Modifier un livre
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LivreDTO livreDTO)
        {
            if (ModelState.IsValid)
            {
                await _livreService.UpdateLivre(id, livreDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(livreDTO);
        }

        // Formulaire de suppression
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var livre = await _livreService.GetLivre(id);
            return livre == null ? NotFound() : View(livre);
        }

        // Supprimer un livre
        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _livreService.DeleteLivre(id);
            return RedirectToAction(nameof(Index));
        }

        // Gestion des genres
        [HttpGet("genres")]
        public async Task<IActionResult> Genres()
        {
            var genres = await _genreService.GetAllGenres();
            return View(genres);
        }

        // Ajouter un genre
        [HttpGet("genres/create")]
        public IActionResult CreateGenre()
        {
            return View();
        }

        [HttpPost("genres/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGenre(GenreDTO genreDTO)
        {
            if (ModelState.IsValid)
            {
                await _genreService.AddGenre(genreDTO);
                return RedirectToAction(nameof(Genres));
            }
            return View(genreDTO);
        }

        // Modifier un genre
        [HttpGet("genres/edit/{id}")]
        public async Task<IActionResult> EditGenre(int id)
        {
            var genre = await _genreService.GetGenre(id);
            return genre == null ? NotFound() : View(genre);
        }

        [HttpPost("genres/edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGenre(int id, GenreDTO genreDTO)
        {
            if (ModelState.IsValid)
            {
                await _genreService.UpdateGenre(id, genreDTO);
                return RedirectToAction(nameof(Genres));
            }
            return View(genreDTO);
        }

        // Supprimer un genre
        [HttpGet("genres/delete/{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genre = await _genreService.GetGenre(id);
            return genre == null ? NotFound() : View(genre);
        }

        [HttpPost("genres/delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteGenreConfirmed(int id)
        {
            await _genreService.DeleteGenre(id);
            return RedirectToAction(nameof(Genres));
        }
    }
}
