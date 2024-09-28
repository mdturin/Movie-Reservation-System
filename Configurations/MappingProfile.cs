using AutoMapper;
using Movie_Reservation_System.Dtos;
using Movie_Reservation_System.Models;

namespace Movie_Reservation_System.Configurations;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddMovieDto, Movie>();
    }
}
