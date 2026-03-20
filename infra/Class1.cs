using data.Context;
using domain.Handlers;
using domain.Models;
using domain.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using data.Repositories;
using domain.Commands;
using domain.Interface;
using System;
using System.Collections.Generic;

namespace infra
{
    public class Class1
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ProjectContext>();



            #region Comment

            services.AddTransient<IRepository<Comment>, Repository<Comment>>();

            services.AddTransient<IRequestHandler<PostGenericCommand<Comment>, string>, PostGenericHandler<Comment>>();
            services.AddTransient<IRequestHandler<PutGenericCommand<Comment>, string>, PutGenericHandler<Comment>>();
            services.AddTransient<IRequestHandler<DeleteGenericCommand<Comment>, string>, DeleteGenericHandler<Comment>>();
            services.AddTransient<IRequestHandler<DeleteObjectCommand<Comment>, string>, DeleteObjectHandler<Comment>>();

            services.AddTransient<IRequestHandler<GetListGenericQuery<Comment>, IEnumerable<Comment>>, GetListGenericHandler<Comment>>();
            services.AddTransient<IRequestHandler<GetGenericQuery<Comment>, Comment>, GetGenericHandler<Comment>>();

            #endregion

            #region Compte

            services.AddTransient<IRepository<Compte>, Repository<Compte>>();

            services.AddTransient<IRequestHandler<PostGenericCommand<Compte>, string>, PostGenericHandler<Compte>>();
            services.AddTransient<IRequestHandler<PutGenericCommand<Compte>, string>, PutGenericHandler<Compte>>();
            services.AddTransient<IRequestHandler<DeleteGenericCommand<Compte>, string>, DeleteGenericHandler<Compte>>();
            services.AddTransient<IRequestHandler<DeleteObjectCommand<Compte>, string>, DeleteObjectHandler<Compte>>();

            services.AddTransient<IRequestHandler<GetListGenericQuery<Compte>, IEnumerable<Compte>>, GetListGenericHandler<Compte>>();
            services.AddTransient<IRequestHandler<GetGenericQuery<Compte>, Compte>, GetGenericHandler<Compte>>();

            #endregion

            #region Genre

            services.AddTransient<IRepository<Genre>, Repository<Genre>>();

            services.AddTransient<IRequestHandler<PostGenericCommand<Genre>, string>, PostGenericHandler<Genre>>();
            services.AddTransient<IRequestHandler<PutGenericCommand<Genre>, string>, PutGenericHandler<Genre>>();
            services.AddTransient<IRequestHandler<DeleteGenericCommand<Genre>, string>, DeleteGenericHandler<Genre>>();
            services.AddTransient<IRequestHandler<DeleteObjectCommand<Genre>, string>, DeleteObjectHandler<Genre>>();

            services.AddTransient<IRequestHandler<GetListGenericQuery<Genre>, IEnumerable<Genre>>, GetListGenericHandler<Genre>>();
            services.AddTransient<IRequestHandler<GetGenericQuery<Genre>, Genre>, GetGenericHandler<Genre>>();

            #endregion

            #region Like

            services.AddTransient<IRepository<Like>, Repository<Like>>();

            services.AddTransient<IRequestHandler<PostGenericCommand<Like>, string>, PostGenericHandler<Like>>();
            services.AddTransient<IRequestHandler<PutGenericCommand<Like>, string>, PutGenericHandler<Like>>();
            services.AddTransient<IRequestHandler<DeleteGenericCommand<Like>, string>, DeleteGenericHandler<Like>>();
            services.AddTransient<IRequestHandler<DeleteObjectCommand<Like>, string>, DeleteObjectHandler<Like>>();

            services.AddTransient<IRequestHandler<GetListGenericQuery<Like>, IEnumerable<Like>>, GetListGenericHandler<Like>>();
            services.AddTransient<IRequestHandler<GetGenericQuery<Like>, Like>, GetGenericHandler<Like>>();

            #endregion

            #region Movie

            services.AddTransient<IRepository<Movie>, Repository<Movie>>();

            services.AddTransient<IRequestHandler<PostGenericCommand<Movie>, string>, PostGenericHandler<Movie>>();
            services.AddTransient<IRequestHandler<PutGenericCommand<Movie>, string>, PutGenericHandler<Movie>>();
            services.AddTransient<IRequestHandler<DeleteGenericCommand<Movie>, string>, DeleteGenericHandler<Movie>>();
            services.AddTransient<IRequestHandler<DeleteObjectCommand<Movie>, string>, DeleteObjectHandler<Movie>>();

            services.AddTransient<IRequestHandler<GetListGenericQuery<Movie>, IEnumerable<Movie>>, GetListGenericHandler<Movie>>();
            services.AddTransient<IRequestHandler<GetGenericQuery<Movie>, Movie>, GetGenericHandler<Movie>>();

            #endregion

            #region Rating

            services.AddTransient<IRepository<Rating>, Repository<Rating>>();

            services.AddTransient<IRequestHandler<PostGenericCommand<Rating>, string>, PostGenericHandler<Rating>>();
            services.AddTransient<IRequestHandler<PutGenericCommand<Rating>, string>, PutGenericHandler<Rating>>();
            services.AddTransient<IRequestHandler<DeleteGenericCommand<Rating>, string>, DeleteGenericHandler<Rating>>();
            services.AddTransient<IRequestHandler<DeleteObjectCommand<Rating>, string>, DeleteObjectHandler<Rating>>();

            services.AddTransient<IRequestHandler<GetListGenericQuery<Rating>, IEnumerable<Rating>>, GetListGenericHandler<Rating>>();
            services.AddTransient<IRequestHandler<GetGenericQuery<Rating>, Rating>, GetGenericHandler<Rating>>();

            #endregion

            #region Role

            services.AddTransient<IRepository<Role>, Repository<Role>>();

            services.AddTransient<IRequestHandler<PostGenericCommand<Role>, string>, PostGenericHandler<Role>>();
            services.AddTransient<IRequestHandler<PutGenericCommand<Role>, string>, PutGenericHandler<Role>>();
            services.AddTransient<IRequestHandler<DeleteGenericCommand<Role>, string>, DeleteGenericHandler<Role>>();
            services.AddTransient<IRequestHandler<DeleteObjectCommand<Role>, string>, DeleteObjectHandler<Role>>();

            services.AddTransient<IRequestHandler<GetListGenericQuery<Role>, IEnumerable<Role>>, GetListGenericHandler<Role>>();
            services.AddTransient<IRequestHandler<GetGenericQuery<Role>, Role>, GetGenericHandler<Role>>();

            #endregion

            #region Login

            services.AddTransient<IRepository<Login>, Repository<Login>>();

            services.AddTransient<IRequestHandler<PostGenericCommand<Login>, string>, PostGenericHandler<Login>>();
            services.AddTransient<IRequestHandler<PutGenericCommand<Login>, string>, PutGenericHandler<Login>>();
            services.AddTransient<IRequestHandler<DeleteGenericCommand<Login>, string>, DeleteGenericHandler<Login>>();
            services.AddTransient<IRequestHandler<DeleteObjectCommand<Login>, string>, DeleteObjectHandler<Login>>();

            services.AddTransient<IRequestHandler<GetListGenericQuery<Login>, IEnumerable<Login>>, GetListGenericHandler<Login>>();
            services.AddTransient<IRequestHandler<GetGenericQuery<Login>, Login>, GetGenericHandler<Login>>();

            #endregion

        }

    }

}
