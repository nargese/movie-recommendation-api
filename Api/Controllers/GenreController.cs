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
    public class GenreController : ControllerBase
    {
        private readonly ProjectContext _context;
        private readonly IRepository<Genre> GenericRepository;
        public readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GenreController(ProjectContext context, IRepository<Genre> repository, IMediator mediator, IMapper mapper)
        {

            this.GenericRepository = repository;
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpGet("GetGenre")]
        public async Task<IEnumerable<GenreDTO>> GetRole()
        {
            return _mediator.Send(new GetListGenericQuery<Genre>())
                .Result.Select(v => _mapper.Map<GenreDTO>(v));
        }

        [HttpGet("GetGenre{id}")]
        public async Task<ActionResult<Genre>> GetGenree(Guid id) =>
            await (new GetGenericHandler<Genre>(GenericRepository)).Handle(new GetGenericQuery<Genre>(condition: x => x.IdGenre.Equals(id), null), new CancellationToken());

        [HttpPost("PostGenre")]
        public async Task<string> PostGenre([FromBody] Genre action) =>
            await (new PostGenericHandler<Genre>(GenericRepository)).Handle(new PostGenericCommand<Genre>(action), new CancellationToken());


        [HttpPut("PutGenre")]
        public async Task<string> PutGenre([FromBody] Genre projet) =>
             await (new PutGenericHandler<Genre>(GenericRepository)).Handle(new PutGenericCommand<Genre>(projet), new CancellationToken());

        
        [HttpDelete("DeleteGenre")]
        public async Task<string> DeleteGenre(Guid id) =>
           await (new DeleteGenericHandler<Genre>(GenericRepository)).Handle(new DeleteGenericCommand<Genre>(id), new CancellationToken());


        private bool GenreExists(Guid id)
        {
            return _context.Genre.Any(e => e.IdGenre == id);
        }
    }
}
