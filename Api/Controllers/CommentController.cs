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
using System.Xml.Linq;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ProjectContext _context;
        private readonly IRepository<Comment> GenericRepository;
        public readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CommentController(ProjectContext context, IRepository<Comment> repository, IMediator mediator, IMapper mapper)
        {

            this.GenericRepository = repository;
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpGet("GetComment")]
        public async Task<IEnumerable<CommentDTO>> GetComment()
        {
            var comments = await _mediator.Send(
         new GetListGenericQuery<Comment>(
             includes: i => i.Include(c => c.Compte)
                             .Include(c => c.Movie)
         )
     );

            return comments.Select(c => new CommentDTO
            {
                IdComment = c.IdComment,
                FK_Compte = c.FK_Compte,
                Nom = c.Compte?.Nom,
                FK_Movie = c.FK_Movie,
                Title = c.Movie?.Title,
                Content = c.Content,
                CreatedAt = c.CreatedAt
            });
        }

        [HttpGet("GetComment{id}")]
        public async Task<ActionResult<Comment>> GetComment(Guid id)
        { var comment = await _mediator.Send(
        new GetGenericQuery<Comment>(
            condition: c => c.IdComment == id,
            includes: i => i.Include(c => c.Compte)
                            .Include(c => c.Movie)
        )
    );

    if (comment == null) return NotFound();

        var commentDTO = new CommentDTO
        {
            IdComment = comment.IdComment,
            FK_Compte = comment.FK_Compte,
            Nom = comment.Compte?.Nom,
            FK_Movie = comment.FK_Movie,
            Title = comment.Movie?.Title,     // now populated
            Content = comment.Content,
            CreatedAt = comment.CreatedAt     // should be set when created
        };
            return Ok(commentDTO);
        }

        [HttpPut("PutComment")]
        public async Task<string> PutComment([FromBody] Comment projet) =>
             await (new PutGenericHandler<Comment>(GenericRepository)).Handle(new PutGenericCommand<Comment>(projet), new CancellationToken());

        //[HttpPost("PostComment")]
        //public async Task<string> PostComment([FromBody] Comment action) =>
        //    await (new PostGenericHandler<Comment>(GenericRepository)).Handle(new PostGenericCommand<Comment>(action), new CancellationToken());

        [HttpPost("PostComment")]
        public async Task<Comment> PostComment([FromBody] Comment action)
        {
            Console.WriteLine($"Received comment: {action.Content} from {action.FK_Compte}");

            // Ajoute le commentaire à la base
            await (new PostGenericHandler<Comment>(GenericRepository))
                .Handle(new PostGenericCommand<Comment>(action), new CancellationToken());

            // Renvoie le commentaire complet en JSON
            return action;
        }



        //[HttpPost("PostComment")]
        //public async Task<Comment> PostComment([FromBody] Comment action)
        //{
        //    // Vérifie que l'objet n'est pas null
        //    if (action == null) throw new ArgumentNullException(nameof(action));

        //    // Ajoute le commentaire dans la base
        //    GenericRepository.Add(action); // ou _context.Comments.Add(action) selon ton repo
        //    await  _context.SaveChangesAsync();

        //    return action; // renvoie le commentaire ajouté
        //}

        [HttpDelete("DeleteComment")]
        public async Task<string> DeleteComment(Guid id) =>
           await (new DeleteGenericHandler<Comment>(GenericRepository)).Handle(new DeleteGenericCommand<Comment>(id), new CancellationToken());


        private bool CommentExists(Guid id)
        {
            return _context.Comment.Any(e => e.IdComment == id);
        }
    }
}
