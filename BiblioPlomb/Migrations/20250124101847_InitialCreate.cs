﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiblioPlomb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auteurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Prenom = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auteurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Emprunts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateEmprunt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateRetour = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emprunts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Prenom = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    MotDePasse = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Livres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titre = table.Column<string>(type: "TEXT", nullable: false),
                    Auteur = table.Column<string>(type: "TEXT", nullable: false),
                    Dispo = table.Column<bool>(type: "INTEGER", nullable: false),
                    Etat = table.Column<int>(type: "INTEGER", nullable: false),
                    ISBN = table.Column<long>(type: "INTEGER", nullable: false),
                    GenreId = table.Column<int>(type: "INTEGER", nullable: false),
                    AuteurId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Livres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UtilisateurRoles",
                columns: table => new
                {
                    UtilisateurId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilisateurRoles", x => new { x.UtilisateurId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UtilisateurRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UtilisateurRoles_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuteurLivres",
                columns: table => new
                {
                    AuteurId = table.Column<int>(type: "INTEGER", nullable: false),
                    LivreId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuteurLivres", x => new { x.AuteurId, x.LivreId });
                    table.ForeignKey(
                        name: "FK_AuteurLivres_Auteurs_AuteurId",
                        column: x => x.AuteurId,
                        principalTable: "Auteurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuteurLivres_Livres_LivreId",
                        column: x => x.LivreId,
                        principalTable: "Livres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmpruntRelations",
                columns: table => new
                {
                    LivreId = table.Column<int>(type: "INTEGER", nullable: false),
                    UtilisateurId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmpruntId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmpruntId1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpruntRelations", x => new { x.LivreId, x.EmpruntId, x.UtilisateurId });
                    table.ForeignKey(
                        name: "FK_EmpruntRelations_Emprunts_EmpruntId",
                        column: x => x.EmpruntId,
                        principalTable: "Emprunts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpruntRelations_Emprunts_EmpruntId1",
                        column: x => x.EmpruntId1,
                        principalTable: "Emprunts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmpruntRelations_Livres_LivreId",
                        column: x => x.LivreId,
                        principalTable: "Livres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpruntRelations_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuteurLivres_LivreId",
                table: "AuteurLivres",
                column: "LivreId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpruntRelations_EmpruntId",
                table: "EmpruntRelations",
                column: "EmpruntId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpruntRelations_EmpruntId1",
                table: "EmpruntRelations",
                column: "EmpruntId1");

            migrationBuilder.CreateIndex(
                name: "IX_EmpruntRelations_UtilisateurId",
                table: "EmpruntRelations",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_Livres_GenreId",
                table: "Livres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_UtilisateurRoles_RoleId",
                table: "UtilisateurRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_Email",
                table: "Utilisateurs",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuteurLivres");

            migrationBuilder.DropTable(
                name: "EmpruntRelations");

            migrationBuilder.DropTable(
                name: "UtilisateurRoles");

            migrationBuilder.DropTable(
                name: "Auteurs");

            migrationBuilder.DropTable(
                name: "Emprunts");

            migrationBuilder.DropTable(
                name: "Livres");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Utilisateurs");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
