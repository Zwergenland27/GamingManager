using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.GameServerTickets.Queries.GetAllOpen;

namespace GamingManager.Application.Features.GameServerTickets.Queries.GetAllOpen;

public record GetAllOpenTickersQuery() : IQuery<IEnumerable<GetAllOpenTicketsResult>>;
