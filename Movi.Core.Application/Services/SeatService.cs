using Movi.Core.Application.Conditions;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;

namespace Movi.Core.Application.Services;

public class SeatService(IBulkRepository context) : ISeatService
{
    private readonly IBulkRepository _context = context;
    public Task<List<Seat>> GetAvailableSeatsAsync(IEnumerable<string> seatIds)
    {
        var inCondition = new InCondition<Seat>(nameof(Seat.Id), seatIds);
        var availableCondition = new FieldCondition<Seat>(nameof(Seat.IsAvailable), true);
        var condition = new AndCondition<Seat>(inCondition, availableCondition);
        return _context.GetItemsAsync(condition.ToExpression());
    }
}
