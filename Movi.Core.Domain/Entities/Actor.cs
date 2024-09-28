using Movi.Core.Domain.Abstractions;

namespace Movi.Core.Domain.Entities;

public class Actor : ADatabaseModel
{
    public string Name { get; set; } // Actor's name
    public string Role { get; set; } // Actor's role in the movie
    public string PhotoUrl { get; set; } // Actor's photo (optional)
}
