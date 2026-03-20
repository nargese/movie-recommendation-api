using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using data.Context;
using MediatR;
using AutoMapper;
using domain.Interface;
using domain.DTOs;
using domain.Queries;
using System.Threading;
using domain.Handlers;
using domain.Commands;
using domain.Models;
using JwtApp.Models;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComptesController : ControllerBase
    {
        private readonly ProjectContext _context;
        private IConfiguration _config;
        private readonly IRepository<Compte> GenericRepository;
        public readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ComptesController(ProjectContext context, IRepository<Compte> repository, IMediator mediator, IMapper mapper, IConfiguration config)
        {
            this.GenericRepository = repository;
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
            _config = config;
        }


        [HttpGet("GetCompte")]
        public async Task<IEnumerable<CompteDTO>> GetCompte()
        {
            var comptes = await _mediator.Send(
                new GetListGenericQuery<Compte>(
                    includes: i => i.Include(c => c.Role)
                )
            );

            return comptes.Select(v => _mapper.Map<CompteDTO>(v));
        }


        [HttpGet("GetCompte/{id}")]
        public async Task<ActionResult<CompteDTO>> GetCompte(Guid id)
        {
            var compte = await (new GetGenericHandler<Compte>(GenericRepository)).Handle(new GetGenericQuery<Compte>(condition: x => x.IdCompte == id,
                    includes: q => q.Include(c => c.Role)
                ), new CancellationToken());

            if (compte == null) return NotFound();

            return Ok(_mapper.Map<CompteDTO>(compte));
        }

        [HttpGet("GetCompteByCin")]
        public async Task<ActionResult<CompteDTO>> GetCompteByCin(string cin)
        {
            var compte = await (new GetGenericHandler<Compte>(GenericRepository)).Handle(new GetGenericQuery<Compte>(
                    condition: x => x.CIN == cin,
                    includes: q => q.Include(c => c.Role)
                ), new CancellationToken());

            if (compte == null) return NotFound();

            return Ok(_mapper.Map<CompteDTO>(compte));
        }


        //   [HttpGet("GetCompte/{id}")]
        //   public async Task<ActionResult<Compte>> GetCompte(Guid id) =>
        //       await (new GetGenericHandler<Compte>(GenericRepository)).Handle(new GetGenericQuery<Compte>(condition: x => x.IdCompte.Equals(id), null), new CancellationToken());


        //   [HttpGet("GetCompteByCin")]
        //   public async Task<ActionResult<Compte>> GetCompteByCin(string cin) =>
        //await (new GetGenericHandler<Compte>(GenericRepository)).Handle(new GetGenericQuery<Compte>(condition: x => x.CIN.Equals(cin), null), new CancellationToken());



        //[HttpGet("GetCompte")]

        //public async Task<IEnumerable<CompteDTO>> GetCompte()
        //{
        //    return (IEnumerable<CompteDTO>)_mediator.Send(new GetListGenericQuery<Compte>())
        //        //(null, includes: i => i.Include(c => c.Filiale))

        //        .Result.Select(v => _mapper.Map<CompteDTO>(v));
        //}
        

       
        //[HttpPost("PostCompte")]
        //public async Task<string> PostCompte([FromBody] Compte action) =>
        //    await (new PostGenericHandler<Compte>(GenericRepository)).Handle(new PostGenericCommand<Compte>(action), new CancellationToken());

        //[HttpPost("PostCompte")]
        //public async Task<string> PostCompte([FromBody] CreateCompteDTO dto)
        //{
        //    var compte = _mapper.Map<Compte>(dto);
        //    return await (new PostGenericHandler<Compte>(GenericRepository))
        //        .Handle(new PostGenericCommand<Compte>(compte), new CancellationToken());
        //}


        [HttpPost("PostCompte")]
        public async Task<IActionResult> PostCompte([FromBody] CreateCompteDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Map DTO to entity
                var compte = _mapper.Map<Compte>(dto);

                // Fill RoleName from existing Role
                var role = await _context.Role.FindAsync(dto.FK_Role);
                if (role == null)
                    return BadRequest($"Role with Id {dto.FK_Role} does not exist.");

                compte.RoleName = role.RoleName;

                var result = await (new PostGenericHandler<Compte>(GenericRepository))
                                .Handle(new PostGenericCommand<Compte>(compte), new CancellationToken());

                return Ok(result);
            }
            catch (DbUpdateException ex)
            {
                string innerMessages = "";
                Exception inner = ex.InnerException;
                while (inner != null)
                {
                    innerMessages += inner.Message + " | ";
                    inner = inner.InnerException;
                }
                return BadRequest(new { message = ex.Message, innerExceptions = innerMessages });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("PutCompte")]
        public async Task<string> PutCompte([FromBody] Compte projet) =>
          await (new PutGenericHandler<Compte>(GenericRepository)).Handle(new PutGenericCommand<Compte>(projet), new CancellationToken());




        [HttpDelete("DeleteCompte")]
        public async Task<string> DeleteCompte(Guid id) =>
           await (new DeleteGenericHandler<Compte>(GenericRepository)).Handle(new DeleteGenericCommand<Compte>(id), new CancellationToken());


        [HttpDelete("DeleteUser")]
        public async Task<ActionResult<Compte>> DeleteUser(Guid id)
        {
            var user = await _context.Compte.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            if (user.access == false)
            {
                user.access = true;
            }



            else if (user.access == true)
            {
                user.access = false;

            }
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return user;
        }


        



        [HttpPost("login/")]
        public IActionResult Login([FromBody] Login user)
        {
            if (user == null)
            {
                return Unauthorized();
            }

            var currentUser = _context.Compte.FirstOrDefault(o => o.CIN == user.cin.ToLower() && o.Motdepasse == user.motdepasse && o.access == true);

            if (currentUser == null || user.cin != currentUser.CIN || user.motdepasse != currentUser.Motdepasse)
            {
                return Unauthorized();
            }

            // Get the key from configuration
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.cin),
        new Claim(ClaimTypes.Role, currentUser.RoleName),
    };

            var tokenOptions = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new AuthenticatedResponse { Token = tokenString });
        }


        [HttpDelete("logout")]
        public async Task<IActionResult> Logout([FromBody] UserLogout userLogout)
        {
            var currentUser = _context.Compte.FirstOrDefault(o => o.CIN.ToLower() == userLogout.cin.ToLower());

            if (currentUser != null)
            {
                //currentUser.access = false;
                await _context.SaveChangesAsync();
                return Ok("User logged out successfully");
            }

            return NotFound("User not found");
        }



        private Compte GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new Compte
                {
                    CIN = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                };
            }
            return null;
        }
        private string Generate(Compte user)
        {
            var key = _config["Jwt:Key"] ?? "default_secret_key";
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.CIN)

            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangerMotDePasse changePasswordModel)
        {
            var currentUser = _context.Compte.FirstOrDefault(o => o.CIN.ToLower() == changePasswordModel.Cin.ToLower() && o.Motdepasse == changePasswordModel.Password);
           // var currentUser = _context.Compte.FirstOrDefault(o => o.CIN.ToLower() == changePasswordModel.Cin.ToLower() && o.Motdepasse == changePasswordModel.Password);

            if (currentUser != null)
            {
                if (IsValidPassword(changePasswordModel.newPassword))
                {
                    currentUser.Motdepasse = changePasswordModel.newPassword;

                    await _context.SaveChangesAsync();
                    return Ok("Password changed successfully");
                }
                else
                {
                    return BadRequest("Invalid new password format");
                }
            }

            return NotFound("User not found");
        }
        private bool IsValidPassword(string password)
        {
            return !string.IsNullOrEmpty(password) && password.Length >= 6;
        }


       
        private bool CompteExists(Guid id)
        {
            return _context.Compte.Any(e => e.IdCompte == id);
        }
    }
}

