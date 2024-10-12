using Microsoft.AspNetCore.Identity;
using Movi.Core.Domain.Interfaces;

namespace Movi.Core.Domain.Entities;

public class ApplicationUser : IdentityUser, IDatabaseModel 
{ 
    public ICollection<Reservation> Reservations { get; set; }
}
