﻿using BiblioPlomb.Data;
using BiblioPlomb.DTO;
using BiblioPlomb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace BiblioPlomb.Services
{
    public class ServicesLivre
    {
        private readonly BiblioPlombDB _db;

        public ServicesLivre(BiblioPlombDB db)
        {
            _db = db;
        }

        // Crée un livre
        public async Task<IResult> AddLivre(LivreDTO livreDTO)
        {
            var livre = new Livre
            {
                Titre = livreDTO.Titre,
                Dispo = livreDTO.Dispo,
                Etat = livreDTO.Etat,
                GenreId = livreDTO.GenreId,
                AuteurId = livreDTO.AuteurId
            };

            _db.Livre.Add(livre);
            await _db.SaveChangesAsync();

            return TypedResults.Created($"/livres/{livre.Id}", livre);
        }

        // Récupérer  un livre par Id
        public async Task<IResult> GetLivre(int id)
        {
            var livre = await _db.Livre
                .Include(livre => livre.Genre)
                .Include(livre => livre.Auteur)
                .FirstOrDefaultAsync(livre => livre.Id == id);

            return livre == null ? TypedResults.NotFound() : TypedResults.Ok(livre);
        }

        //recherche par titre du livre insensible a la casse (Entity Framework - EF.Functions.Like)
        public async Task<IResult> LivreParTitre(string titre)
        {
            var livre = await _db.Livre
                .Include(livre => livre.Genre)
                .Include(livre => livre.Auteur)
                .FirstOrDefaultAsync(livre => EF.Functions.Like(livre.Titre, $"%{titre}%"));

            return livre == null ? TypedResults.NotFound() : TypedResults.Ok(livre);
        }


        // Affiche tous les livres
        public async Task<IResult> GetAllLivres()
        {
            var livres = await _db.Livre
            .Include(livre => livre.Genre)
            .Include(livre => livre.Auteur)
            .ToListAsync();

            return TypedResults.Ok(livres);
        }

        // Modifier un livre
        public async Task<IResult> UpdateLivre(int id, LivreDTO livreDTO)
        {
            var livre = await _db.Livre.FindAsync(id);
            if (livre == null)
            {
                return TypedResults.NotFound();
            }

            livre.Titre = livreDTO.Titre;
            livre.Dispo = livreDTO.Dispo;
            livre.Etat = livreDTO.Etat;
            livre.GenreId = livreDTO.GenreId;
            livre.AuteurId = livreDTO.AuteurId;
            livre.ISBN = livreDTO.ISBN;

            await _db.SaveChangesAsync();
            return TypedResults.NoContent();
        }

        // Supprimer un livre ( endommagé par exemple)
        public async Task<IResult> DeleteLivre(int id)
        {
            var livre = await _db.Livre.FindAsync(id);
            if (livre == null)
            {
                return TypedResults.NotFound();
            }

            _db.Livre.Remove(livre);
            await _db.SaveChangesAsync();
            return TypedResults.NoContent();
        }

        // Vérifie si livre est Dispo
        public async Task<IResult> VerifDispo(string titre)
        {
            var livres = await _db.Livre
                .Where(livre => livre.Titre == titre)
                .ToListAsync();

            if (livres.Any(livre => livre.Dispo))
            {
                return TypedResults.Ok("Le livre est disponible.");
            }
            else
            {
                return TypedResults.Ok("Le livre n'est pas disponible.");
            }
        }

        // Modifier l'état du livre
        public async Task<IResult> ModifierEtatLivre(int id, EtatLivre nouvelEtat)
        {
            var livre = await _db.Livre.FindAsync(id);
            if (livre == null)
            {
                return TypedResults.NotFound();
            }

            livre.Etat = nouvelEtat;

            if (nouvelEtat == EtatLivre.Dégradé)
            {
                livre.Dispo = false; // Inempruntable
            }
            else
            {
                livre.Dispo = true; // Empruntable
            }

            await _db.SaveChangesAsync();
            return TypedResults.Ok(livre);
        }
    }
}