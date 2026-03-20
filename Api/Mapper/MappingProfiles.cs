using AutoMapper;
using domain.DTOs;
using domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace Api.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap <Comment, CommentDTO> ()
            .ForMember(d => d.Title, i => i.MapFrom(src => src.Movie.Title))
            .ForMember(d => d.Nom, i => i.MapFrom(src => src.Compte.Nom))
            .ReverseMap();

           
            CreateMap<CreateCommentDTO, Comment>()
                .ForMember(dest => dest.Movie, opt => opt.Ignore()) // évite la création d’un Role complet
                .ForMember(dest => dest.Compte, opt => opt.Ignore()) // évite la création d’un Role complet
                        .ReverseMap();

            CreateMap<Genre, GenreDTO>()
            .ReverseMap();

            CreateMap<Like, LikeDTO>()
            .ForMember(d => d.Title, i => i.MapFrom(src => src.Movie.Title))
            .ForMember(d => d.Nom, i => i.MapFrom(src => src.Compte.Nom))
            .ReverseMap();

            CreateMap<CreateLikeDTO, Like>()
                .ForMember(dest => dest.Movie, opt => opt.Ignore()) 
                .ForMember(dest => dest.Compte, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<Movie, MovieDTO>()
            .ForMember(d => d.Nom, i => i.MapFrom(src => src.Compte.Nom))
            .ForMember(d => d.Genres,i => i.MapFrom(src => src.MovieGenres.Select(mg => mg.Genre.Name)))
             .ReverseMap();

            CreateMap<CreateMovieDTO, Movie>()
    .ForMember(dest => dest.Compte, opt => opt.Ignore()) // éviter de charger Compte
    .ForMember(dest => dest.MovieGenres, opt => opt.MapFrom(src => src.GenreIds.Select(id => new MovieGenre { IdGenre = id })));

            CreateMap<CreateMovieDTO, Movie>()
                .ForMember(dest => dest.Compte, opt => opt.Ignore()) // évite la création d’un Role complet
            .ReverseMap();

            CreateMap<Rating, RatingDTO>()
            .ForMember(d => d.Title, i => i.MapFrom(src => src.Movie.Title))
            .ForMember(d => d.Nom, i => i.MapFrom(src => src.Compte.Nom))
            .ReverseMap();

            CreateMap<CreateRatingDTO, Rating>()
                .ForMember(dest => dest.Movie, opt => opt.Ignore()) // évite la création d’un Role complet
                .ForMember(dest => dest.Compte, opt => opt.Ignore()) // évite la création d’un Role complet
            .ReverseMap();

            CreateMap<Role, RoleDTO>()
           .ReverseMap();

            CreateMap<Compte, CompteDTO>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName));

            CreateMap<CreateCompteDTO, Compte>()
                .ForMember(dest => dest.Role, opt => opt.Ignore()); // évite la création d’un Role complet



        }
    }
}
