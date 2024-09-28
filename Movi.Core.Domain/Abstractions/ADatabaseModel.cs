using Movi.Core.Domain.Interfaces;

namespace Movi.Core.Domain.Abstractions;

public abstract class ADatabaseModel : IDatabaseModel
{
    public string Id { get; set; }
    public ADatabaseModel() => Id = Guid.NewGuid().ToString("N");
}
