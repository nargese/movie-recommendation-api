////using AutoMapper;
////using MediatR;
////using Microsoft.AspNetCore.Authentication.JwtBearer;
////using Microsoft.AspNetCore.Http.Features;
////using Microsoft.EntityFrameworkCore;
////using Microsoft.IdentityModel.Tokens;
////using Microsoft.OpenApi.Models;
////using Newtonsoft.Json;
////using System.Text;
////using data.Context;

////var builder = WebApplication.CreateBuilder(args);

////builder.Services.AddDbContext<ProjectContext>(options =>
////    options.UseSqlServer(builder.Configuration.GetConnectionString("ProjectContext")));

////// Add services to the container.
////// Controllers + JSON
////builder.Services.AddControllers()
////    .AddNewtonsoftJson(options =>
////        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
////    );

////builder.Services.AddControllers();
////// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
////builder.Services.AddOpenApi();
////builder.Services.AddEndpointsApiExplorer();
////builder.Services.AddSwaggerGen();
////var app = builder.Build();
////app.UseSwagger();
////app.UseSwaggerUI();
////// Configure the HTTP request pipeline.
////if (app.Environment.IsDevelopment())
////{
////    app.MapOpenApi();
////}





////// Swagger
////builder.Services.AddEndpointsApiExplorer();
////builder.Services.AddSwaggerGen(c =>
////{
////    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CQRS", Version = "v1" });
////});

////// Authentication JWT
////builder.Services.AddAuthentication(opt =>
////{
////    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
////    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
////})
////.AddJwtBearer(options =>
////{
////    options.TokenValidationParameters = new TokenValidationParameters
////    {
////        ValidateIssuer = true,
////        ValidateAudience = true,
////        ValidateLifetime = true,
////        ValidateIssuerSigningKey = true,
////        ValidIssuer = "https://localhost:44323",
////        ValidAudience = "https://localhost:44323",
////        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
////    };
////});

////// CORS
////builder.Services.AddCors(options =>
////{
////    options.AddPolicy("AllowAll", policy =>
////    {
////        policy.AllowAnyOrigin()
////              .AllowAnyMethod()
////              .AllowAnyHeader();
////    });
////});

////// MediatR + AutoMapper
////builder.Services.AddMediatR(typeof(Program));
////builder.Services.AddAutoMapper(typeof(Program));

////// Limites des fichiers upload
////builder.Services.Configure<FormOptions>(o =>
////{
////    o.ValueLengthLimit = int.MaxValue;
////    o.MultipartBodyLengthLimit = int.MaxValue;
////    o.MemoryBufferThreshold = int.MaxValue;
////});

////// DbContext
////builder.Services.AddDbContext<ProjectContext>(options =>
////    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

////// Ici tu peux appeler ton DependencyContainer si tu l’as
////// DependencyContainer.RegisterServices(builder.Services);

//////var app = builder.Build();

////// ---------------- CONFIG MIDDLEWARE ----------------

////if (app.Environment.IsDevelopment())
////{
////    app.UseSwagger();
////    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CQRS v1"));
////}

////app.UseHttpsRedirection();
////app.UseStaticFiles();
////app.UseCors("AllowAll");

////app.UseAuthentication();
////app.UseAuthorization();

////app.MapControllers();

////app.Run();

//using AutoMapper;
//using data.Context;
//using data.Repositories;
//using domain.Interface;
//using domain.Queries;
//using MediatR;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Http.Features;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using Newtonsoft.Json;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);

//// 1. Ajouter DbContext UNE SEULE FOIS, avec la bonne chaîne de connexion
//builder.Services.AddDbContext<ProjectContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("ProjectContext")));

//// 2. Ajouter les services
//builder.Services.AddControllers()
//    .AddNewtonsoftJson(options =>
//        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddAutoMapper(typeof(Program));

//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CQRS", Version = "v1" });
//});

//builder.Services.AddAuthentication(opt =>
//{
//    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = "https://localhost:44323",
//        ValidAudience = "https://localhost:44323",
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
//    };
//});

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", policy =>
//    {
//        policy.AllowAnyOrigin()
//              .AllowAnyMethod()
//              .AllowAnyHeader();
//    });
//});

//builder.Services.AddMediatR(cfg =>
//{
//    cfg.RegisterServicesFromAssemblies(
//        typeof(GetListGenericQuery<>).Assembly,
//        typeof(Program).Assembly
//    );
//});

//builder.Services.Configure<FormOptions>(o =>
//{
//    o.ValueLengthLimit = int.MaxValue;
//    o.MultipartBodyLengthLimit = int.MaxValue;
//    o.MemoryBufferThreshold = int.MaxValue;
//});
//builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

//// 3. Construire l'application
//var app = builder.Build();

//// 4. Configurer le pipeline HTTP
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CQRS v1"));
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseCors("AllowAll");

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices(services =>
                    {
                        services.Configure<FormOptions>(options =>
                        {
                            options.ValueLengthLimit = int.MaxValue;
                            options.MultipartBodyLengthLimit = int.MaxValue;
                            options.MemoryBufferThreshold = int.MaxValue;
                        });
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
