BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Roles" (
	"Id"	INTEGER NOT NULL,
	"Type"	TEXT NOT NULL,
	CONSTRAINT "PK_Roles" PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "UtilisateurRoles" (
	"UtilisateurId"	INTEGER NOT NULL,
	"RoleId"	INTEGER NOT NULL,
	CONSTRAINT "PK_UtilisateurRoles" PRIMARY KEY("UtilisateurId","RoleId"),
	CONSTRAINT "FK_UtilisateurRoles_Roles_RoleId" FOREIGN KEY("RoleId") REFERENCES "Roles"("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_UtilisateurRoles_Utilisateurs_UtilisateurId" FOREIGN KEY("UtilisateurId") REFERENCES "Utilisateurs"("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "Utilisateurs" (
	"Id"	INTEGER NOT NULL,
	"Nom"	TEXT NOT NULL,
	"Prenom"	TEXT NOT NULL,
	"Email"	TEXT NOT NULL,
	"MotDePasse"	TEXT NOT NULL,
	CONSTRAINT "PK_Utilisateurs" PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
	"MigrationId"	TEXT NOT NULL,
	"ProductVersion"	TEXT NOT NULL,
	CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY("MigrationId")
);
CREATE TABLE IF NOT EXISTS "__EFMigrationsLock" (
	"Id"	INTEGER NOT NULL,
	"Timestamp"	TEXT NOT NULL,
	CONSTRAINT "PK___EFMigrationsLock" PRIMARY KEY("Id")
);
INSERT INTO "__EFMigrationsHistory" VALUES ('20250120093908_InitialCreate','9.0.1');
CREATE INDEX IF NOT EXISTS "IX_UtilisateurRoles_RoleId" ON "UtilisateurRoles" (
	"RoleId"
);
COMMIT;
