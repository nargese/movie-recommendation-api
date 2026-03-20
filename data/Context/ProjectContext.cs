using domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace data.Context 
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {


        }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Like> Like { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Compte> Compte { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<MovieGenre> MovieGenre { get; set; }


        public object UserModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Clé primaire Movie
            modelBuilder.Entity<Movie>()
                .HasKey(m => m.IdMovie);

            // Clé primaire Genre
            modelBuilder.Entity<Genre>()
                .HasKey(g => g.IdGenre);

            // Clé primaire Comment
            modelBuilder.Entity<Comment>()
                .HasKey(c => c.IdComment);

            // Clé primaire Like
            modelBuilder.Entity<Like>()
                .HasKey(l => l.IdLike);

            // Clé primaire Rating
            modelBuilder.Entity<Rating>()
                .HasKey(r => r.IdRating);

            modelBuilder.Entity<Compte>()
                    .HasKey(c => c.IdCompte);

            modelBuilder.Entity<Role>()
                    .HasKey(c => c.IdRole);

            modelBuilder.Entity<MovieGenre>()
                .HasKey(mg => new { mg.IdMovie, mg.IdGenre });



            // Relation Role -> Compte (One-to-Many)
            modelBuilder.Entity<Role>()
                        .HasMany(c => c.Comptes)
                        .WithOne(e => e.Role)
                        .HasForeignKey(c => c.FK_Role);


            // Relation Movie -> Comments (One-to-Many)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Movie)
                .WithMany(m => m.Comments)
                .HasForeignKey(c => c.FK_Movie);

            // Relation User -> Comments (One-to-Many)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Compte)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.FK_Compte);

            // Relation Movie -> Likes (One-to-Many)
            modelBuilder.Entity<Like>()
                .HasOne(l => l.Movie)
                .WithMany(m => m.Likes)
                .HasForeignKey(l => l.FK_Movie);

            // Relation User -> Likes (One-to-Many)
            modelBuilder.Entity<Like>()
                .HasOne(l => l.Compte)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.FK_Compte);

            // Relation Movie -> Ratings (One-to-Many)
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Movie)
                .WithMany(m => m.Ratings)
                .HasForeignKey(r => r.FK_Movie);

            // Relation User -> Ratings (One-to-Many)
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Compte)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.FK_Compte);

            // Relation Movie <-> Genre (Many-to-Many with explicit entity)
           

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Movie)
                .WithMany(m => m.MovieGenres)
                .HasForeignKey(mg => mg.IdMovie);

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(mg => mg.IdGenre);

            // Compte -> Movie (One-to-Many)
            modelBuilder.Entity<Compte>()
                .HasMany(c => c.Movies)       // Un compte a plusieurs films
                .WithOne(m => m.Compte)       // Chaque film appartient à un compte
                .HasForeignKey(m => m.FK_Compte)  // La clé étrangère dans Movie
                .OnDelete(DeleteBehavior.NoAction); // Si un compte est supprimé, ses films le seront aussi


        }

    }
}



