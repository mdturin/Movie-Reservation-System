using Movie_Reservation_System.Interfaces;

namespace Movie_Reservation_System.Abstractions;

public abstract class ADatabaseModel : IDatabaseModel
{
    public string Id { get; set; }
    public ADatabaseModel() => Id = Guid.NewGuid().ToString("N");
}
