using AutoMapper;
using data.Context;
using data.Repositories;
using domain.Commands;
using domain.DTOs;
using domain.Handlers;
using domain.Interface;
using domain.Models;
using domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly ProjectContext _context;
        private readonly IRepository<Movie> GenericRepository;
        public readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public MovieController(ProjectContext context, IRepository<Movie> repository, IMediator mediator, IMapper mapper)
        {
            this.GenericRepository = repository;
            _context = context;
            _mediator = mediator;
            _mapper = mapper;

        }

        #region Standard CRUD methods
        //[HttpGet("GetMovie")]
        //public async Task<IEnumerable<MovieDTO>> GetAllOIStation()
        //{

        //    return _mediator.Send(new GetListGenericQuery<Movie>(null, includes: i => i.Include(c => c.Compte)))
        //         .Result.Select(v => _mapper.Map<MovieDTO>(v));
        //}

        //[HttpGet("GetMovieById")]
        //public async Task<Movie> GetMovieByID(Guid id)
        //{
        //    return await _mediator.Send(new GetGenericQuery<Movie>(condition: c => c.IdMovie == id));
        //}


        //[HttpGet("GetMovieByCompte")]
        //public async Task<Movie> GetMovieByCompte(Guid id)
        //{
        //    return await _mediator.Send(new GetGenericQuery<Movie>(condition: c => c.FK_Compte == id));
        //}

        [HttpGet("GetMovie")]
        public async Task<IEnumerable<MovieDTO>> GetAllMovies()
        {
            var movies = await _mediator.Send(new GetListGenericQuery<Movie>(
                null,
                includes: i => i
                    .Include(m => m.Compte)                 // include Nom via Compte
                    .Include(m => m.MovieGenres)            // include junction table
                        .ThenInclude(mg => mg.Genre)       // include Genre
            ));

            return movies.Select(m => _mapper.Map<MovieDTO>(m));
        }

        [HttpGet("GetMovieById/{id}")]
        public async Task<MovieDTO> GetMovieByID(Guid id)
        {
            var movie = await _mediator.Send(new GetGenericQuery<Movie>(
                condition: c => c.IdMovie == id,
                includes: i => i
                    .Include(m => m.Compte)
                    .Include(m => m.MovieGenres)
                        .ThenInclude(mg => mg.Genre)
            ));

            return _mapper.Map<MovieDTO>(movie);
        }

        [HttpGet("GetMovieByCompte")]
        public async Task<IEnumerable<MovieDTO>> GetMovieByCompte([FromQuery] Guid id)
        {
            var movies = await _mediator.Send(new GetListGenericQuery<Movie>(
                condition: c => c.FK_Compte == id,
                includes: i => i
                    .Include(m => m.Compte)
                    .Include(m => m.MovieGenres)
                        .ThenInclude(mg => mg.Genre)
            ));

            return movies.Select(m => _mapper.Map<MovieDTO>(m));
        }


        [HttpGet("GetMoviesLikedByUser/{userId}")]
        public async Task<IActionResult> GetMoviesLikedByUser(Guid userId)
        {
            var movies = await _context.Like
                .Where(l => l.FK_Compte == userId)
                .Select(l => l.Movie) // Assure-toi que la relation Movie est bien configurée dans ton modèle Like
                .ToListAsync();

            return Ok(movies);
        }

        // GET: api/Movie/{id}/comments
        [HttpGet("{id}/comments")]
        public async Task<IActionResult> GetCommentsByMovie(Guid id)
        {
            var comments = await _context.Comment
                .Where(c => c.FK_Movie == id)
                .Include(c => c.Compte) // Inclure info utilisateur si besoin
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new
                {
                    c.IdComment,
                    c.Content,
                    c.CreatedAt,
                    Username = c.Compte != null ? c.Compte.Nom : "User" // Ajuste selon ton modèle
                })
                .ToListAsync();

            return Ok(comments);
        }


        //[HttpPost("PostMovie")]
        //public async Task<string> PostMovie([FromBody] CreateMovieDTO dto)
        //{
        //    var movie = _mapper.Map<Movie>(dto);

        //    // Attach genres via MovieGenre
        //    movie.MovieGenres = dto.GenreIds.Select(id => new MovieGenre
        //    {
        //        IdMovie = movie.IdMovie,
        //        IdGenre = id
        //    }).ToList();

        //    return await _mediator.Send(new PostGenericCommand<Movie>(movie));
        //}

        [HttpPost("upload-poster")]
        public async Task<IActionResult> UploadPoster(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Aucun fichier envoyé");

            // chemin de stockage local (wwwroot/posters)
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/posters");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // Nom unique
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // URL publique
            var fileUrl = $"{Request.Scheme}://{Request.Host}/posters/{fileName}";

            return Ok(new { url = fileUrl });
        }

        [HttpPost("PostMovie")]
        public async Task<string> PostMovie([FromBody] CreateMovieDTO dto)
        {
            var movie = _mapper.Map<Movie>(dto);

            // Set Nom from the related Compte
            if (movie.FK_Compte.HasValue)
            {
                var compte = await _context.Compte.FindAsync(movie.FK_Compte.Value);
                if (compte == null) return "Compte not found";
                movie.Nom = compte.Nom; // <-- required by the database
            }
            else
            {
                movie.Nom = "Unknown"; // optional default
            }

            // Attach genres via MovieGenre
            movie.MovieGenres = dto.GenreIds.Select(id => new MovieGenre
            {
                IdMovie = movie.IdMovie,
                IdGenre = id
            }).ToList();

            return await _mediator.Send(new PostGenericCommand<Movie>(movie));
        }


        [HttpPut("PutMovie")]
        public async Task<string> PutMovie(Movie Movie)
        {
            return await _mediator.Send(new PutGenericCommand<Movie>(Movie));
        }

        [HttpPut("PutMovie/{id}")]
        public async Task<IActionResult> PutMovie(Guid id, [FromBody] CreateMovieDTO dto)
        {
            if (dto == null || dto.IdMovie != id)
                return BadRequest("Invalid movie data");

            var existingMovie = await _context.Movie.FindAsync(id);
            if (existingMovie == null)
                return NotFound("Movie not found");

            // Mise à jour des champs de base
            existingMovie.Title = dto.Title;
            existingMovie.Overview = dto.Overview;
            existingMovie.PosterPath = dto.PosterPath;
            existingMovie.ReleaseDate = dto.ReleaseDate;
            existingMovie.Description = dto.Description;
            existingMovie.ReleaseYear = dto.ReleaseYear;
            existingMovie.PosterUrl = dto.PosterUrl;
            existingMovie.AverageRating = dto.AverageRating;
            existingMovie.CreatedAt = dto.CreatedAt;
            existingMovie.FK_Compte = dto.FK_Compte;

            // ✅ Gestion des genres (beaucoup plus léger)
            if (dto.GenreIds != null && dto.GenreIds.Any())
            {
                var movieGenres = _context.MovieGenre.Where(mg => mg.IdMovie == id);
                _context.MovieGenre.RemoveRange(movieGenres);

                foreach (var genreId in dto.GenreIds)
                {
                    _context.MovieGenre.Add(new MovieGenre
                    {
                        IdMovie = id,
                        IdGenre = genreId
                    });
                }
            }

            await _context.SaveChangesAsync();

            return Ok("Movie updated successfully");
        }



        [HttpDelete("DeleteMovie")]
        public async Task<string> DeleteMovie(Guid MovieId)
        {
            return await _mediator.Send(new DeleteGenericCommand<Movie>(MovieId));
        }
        #endregion

        
    }
}
