using AutoMapper;
using Movi.Core.Domain.Dtos;
using Movi.Core.Domain.Entities;

namespace Movi.WebAPI.Configurations;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddMovieDto, Movie>();
    }
}
