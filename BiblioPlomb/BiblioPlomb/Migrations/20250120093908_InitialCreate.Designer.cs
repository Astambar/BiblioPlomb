﻿// <auto-generated />
using BiblioPlomb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BiblioPlomb.Migrations
{
    [DbContext(typeof(BiblioPlombContext))]
    [Migration("20250120093908_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("BiblioPlomb.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("BiblioPlomb.Models.Utilisateur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MotDePasse")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Utilisateurs");
                });

            modelBuilder.Entity("BiblioPlomb.Models.UtilisateurRole", b =>
                {
                    b.Property<int>("UtilisateurId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("UtilisateurId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UtilisateurRoles");
                });

            modelBuilder.Entity("BiblioPlomb.Models.UtilisateurRole", b =>
                {
                    b.HasOne("BiblioPlomb.Models.Role", "Role")
                        .WithMany("UtilisateurRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BiblioPlomb.Models.Utilisateur", "Utilisateur")
                        .WithMany("UtilisateurRoles")
                        .HasForeignKey("UtilisateurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("Utilisateur");
                });

            modelBuilder.Entity("BiblioPlomb.Models.Role", b =>
                {
                    b.Navigation("UtilisateurRoles");
                });

            modelBuilder.Entity("BiblioPlomb.Models.Utilisateur", b =>
                {
                    b.Navigation("UtilisateurRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
