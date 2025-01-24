using BiblioPlomb.Data;
using BiblioPlomb.DTO;
using BiblioPlomb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace BiblioPlomb.Services
{
    public class ServicesEmprunt
    {
        private readonly BiblioPlombDB _db;

        public ServicesEmprunt(BiblioPlombDB db)
        {
            _db = db;
        }

        // Nouveau emprunt
        public async Task<IResult> AddEmprunt(EmpruntDTO empruntDTO)
        {
            var emprunt = new Emprunt
            {
                EmpruntUtilisateurs = empruntDTO.EmpruntUtilisateurs,
                EmpruntLivres = empruntDTO.EmpruntLivres,
                DateRetour = empruntDTO.DateRetour
            };

            _db.Emprunts.Add(emprunt);
            await _db.SaveChangesAsync();
            return TypedResults.Created($"/emprunts/{emprunt.Id}", emprunt);
        }

        // Récupérer un emprunt par Id
        public async Task<IResult> GetEmprunt(int id)
        {
            var emprunt = await _db.Emprunts
                .FirstOrDefaultAsync(emprunt => emprunt.Id == id);

            return emprunt == null ? TypedResults.NotFound() : TypedResults.Ok(emprunt);
        }

        // Tous les emprunts
        public async Task<IResult> GetAllEmprunts()
        {
            var emprunts = await _db.Emprunts
                .ToListAsync();

            return TypedResults.Ok(emprunts);
        }

        // Modifier Emprunt
        public async Task<IResult> UpdateEmprunt(int id, EmpruntDTO empruntDTO)
        {
            var emprunt = await _db.Emprunts.FindAsync(id);
            if (emprunt == null)
            {
                return TypedResults.NotFound();
            }

            emprunt.DateRetour = empruntDTO.DateRetour;

            await _db.SaveChangesAsync();
            return TypedResults.NoContent();
        }

        // Supprimer emprunt
        public async Task<IResult> DeleteEmprunt(int id)
        {
            var emprunt = await _db.Emprunts.FindAsync(id);
            if (emprunt == null)
            {
                return TypedResults.NotFound();
            }

            _db.Emprunts.Remove(emprunt);
            await _db.SaveChangesAsync();
            return TypedResults.NoContent();
        }
    }
}

