using AutoMapper;
using AutoMapper.Configuration;
using data.Context;
using domain.Commands;
using domain.DTOs;
using domain.Handlers;
using domain.Interface;
using domain.Models;
using domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly ProjectContext _context;
        private readonly IRepository<Rating> GenericRepository;
        public readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RatingController(ProjectContext context, IRepository<Rating> repository, IMediator mediator, IMapper mapper)
        {

            this.GenericRepository = repository;
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        
        [HttpGet("GetRating")]
        public async Task<IEnumerable<RatingDTO>> GetRating()
        {
            var ratings = await _mediator.Send(
        new GetListGenericQuery<Rating>(
            includes: i => i.Include(r => r.Compte)
                            .Include(r => r.Movie)
        )
    );

            return ratings.Select(r => _mapper.Map<RatingDTO>(r));
        
        }

        [HttpGet("GetRating/{id}")]
        public async Task<ActionResult<RatingDTO>> GetRating(Guid id)
        {
            var rating = await _mediator.Send(
                new GetGenericQuery<Rating>(
                    condition: r => r.IdRating == id,
                    includes: i => i.Include(r => r.Compte)
                                    .Include(r => r.Movie)
                )
            );

            if (rating == null) return NotFound();

            // Map to DTO
            var ratingDTO = new RatingDTO
            {
                IdRating = rating.IdRating,
                FK_Compte = rating.FK_Compte,
                Nom = rating.Compte?.Nom,
                FK_Movie = rating.FK_Movie,
                Title = rating.Movie?.Title,
                RatingValue = rating.RatingValue,
                CreatedAt = rating.CreatedAt
            };

            return Ok(ratingDTO);
        }

        [HttpGet("GetRatingByMovie")]
        public async Task<ActionResult<double>> GetRatingByMovie(Guid movieId)
        {
            // Vérifie qu'il y a des ratings pour ce film
            var ratings = await _context.Rating
                .Where(r => r.FK_Movie == movieId)
                .ToListAsync();

            if (ratings.Count == 0)
                return Ok(0); // si pas de rating, retourne 0

            // Calcule la moyenne
            var average = ratings.Average(r => r.RatingValue);
            return Ok(average);
        }
        //[HttpPost("PostRating")]
        //public async Task<string> PostRating([FromBody] Rating action) =>
        //    await (new PostGenericHandler<Rating>(GenericRepository)).Handle(new PostGenericCommand<Rating>(action), new CancellationToken());

        [HttpPost("PostRating")]
        public async Task<Rating> PostRating([FromBody] Rating action)
        {
            Console.WriteLine($"Received Rating: {action.RatingValue} from {action.FK_Compte}");

            // Ajoute le commentaire à la base
            await (new PostGenericHandler<Rating>(GenericRepository))
                .Handle(new PostGenericCommand<Rating>(action), new CancellationToken());

            // Renvoie le commentaire complet en JSON
            return action;
        }

        [HttpPut("PutRating")]
        public async Task<string> PutRating([FromBody] Rating projet) =>
             await (new PutGenericHandler<Rating>(GenericRepository)).Handle(new PutGenericCommand<Rating>(projet), new CancellationToken());

        
        [HttpDelete("DeleteRating")]
        public async Task<string> DeleteRating(Guid id) =>
           await (new DeleteGenericHandler<Rating>(GenericRepository)).Handle(new DeleteGenericCommand<Rating>(id), new CancellationToken());


        private bool RatingExists(Guid id)
        {
            return _context.Rating.Any(e => e.IdRating == id);
        }
    }
}
