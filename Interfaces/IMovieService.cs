using Movie_Reservation_System.Dtos;

namespace Movie_Reservation_System.Interfaces;

public interface IMovieService
{
    Task<int> AddAsync(AddMovieDto dto);
}
