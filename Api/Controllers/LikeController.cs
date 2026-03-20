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
    public class LikeController : ControllerBase
    {
        private readonly ProjectContext _context;
        private readonly IRepository<Like> GenericRepository;
        public readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public LikeController(ProjectContext context, IRepository<Like> repository, IMediator mediator, IMapper mapper)
        {

            this.GenericRepository = repository;
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpGet("GetLike")]
        public async Task<IEnumerable<LikeDTO>> GetLike()
        {
            var likes = await _mediator.Send(
                new GetListGenericQuery<Like>(
                    includes: i => i.Include(l => l.Compte)
                                    .Include(l => l.Movie)
                )
            );

            return likes.Select(v => _mapper.Map<LikeDTO>(v));
        }


        [HttpGet("GetLike{id}")]
        public async Task<ActionResult<Like>> GetLike(Guid id) =>
    await (new GetGenericHandler<Like>(GenericRepository))
        .Handle(new GetGenericQuery<Like>(
            condition: x => x.IdLike.Equals(id), null),
            new CancellationToken());

        [HttpGet("GetUserLike")]
        public async Task<ActionResult<LikeDTO>> GetUserLike(Guid movieId, Guid userId)
        {
            // Récupère le like correspondant
            var like = await _mediator.Send(
                new GetGenericQuery<Like>(
                    condition: x => x.FK_Movie == movieId && x.FK_Compte == userId,
                    includes: i => i.Include(l => l.Compte).Include(l => l.Movie)
                )
            );

            if (like == null)
                return NotFound();

            return _mapper.Map<LikeDTO>(like);
        }

        //[HttpPost("PostLike")]
        //public async Task<string> PostLike([FromBody] Like action) =>
        //    await (new PostGenericHandler<Like>(GenericRepository)).Handle(new PostGenericCommand<Like>(action), new CancellationToken());

        [HttpPost("PostLike")]
        public async Task<Like> PostLike([FromBody] Like action)
        {
            Console.WriteLine($"Received Like for Movie: {action.FK_Movie} from Account: {action.FK_Compte} at {action.CreatedAt}");

            // Ajoute le commentaire à la base
            await (new PostGenericHandler<Like>(GenericRepository))
                .Handle(new PostGenericCommand<Like>(action), new CancellationToken());

            // Renvoie le commentaire complet en JSON
            return action;
        }

        [HttpPut("PutLike")]
        public async Task<string> PutLike([FromBody] Like projet) =>
             await (new PutGenericHandler<Like>(GenericRepository)).Handle(new PutGenericCommand<Like>(projet), new CancellationToken());

        
        [HttpDelete("DeleteLike")]
        public async Task<string> DeleteLike(Guid id) =>
           await (new DeleteGenericHandler<Like>(GenericRepository)).Handle(new DeleteGenericCommand<Like>(id), new CancellationToken());


        private bool LikeExists(Guid id)
        {
            return _context.Like.Any(e => e.IdLike == id);
        }
    }
}
