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
    public class RolesController : ControllerBase
    {
        private readonly ProjectContext _context;
        private readonly IRepository<Role> GenericRepository;
        public readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RolesController(ProjectContext context, IRepository<Role> repository, IMediator mediator, IMapper mapper)
        {

            this.GenericRepository = repository;
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        
        [HttpGet("GetRole")]
        public async Task<IEnumerable<RoleDTO>> GetRole()
        {
            return _mediator.Send(new GetListGenericQuery<Role>())
                .Result.Select(v => _mapper.Map<RoleDTO>(v));
        }

        [HttpGet("GetRole{id}")]
        public async Task<ActionResult<Role>> GetRole(Guid id) =>
            await (new GetGenericHandler<Role>(GenericRepository)).Handle(new GetGenericQuery<Role>(condition: x => x.IdRole.Equals(id), null), new CancellationToken());

        [HttpPost("PostRole")]
        public async Task<string> PostRole([FromBody] Role action) =>
            await (new PostGenericHandler<Role>(GenericRepository)).Handle(new PostGenericCommand<Role>(action), new CancellationToken());


        [HttpPut("PutRole")]
        public async Task<string> PutRole([FromBody] Role projet) =>
             await (new PutGenericHandler<Role>(GenericRepository)).Handle(new PutGenericCommand<Role>(projet), new CancellationToken());

         [HttpDelete("DeleteRole")]
        public async Task<string> DeleteRole(Guid id) =>
           await (new DeleteGenericHandler<Role>(GenericRepository)).Handle(new DeleteGenericCommand<Role>(id), new CancellationToken());


        private bool RoleExists(Guid id)
        {
            return _context.Role.Any(e => e.IdRole == id);
        }
    }
}
