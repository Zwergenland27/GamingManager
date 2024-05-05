using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Games.Queries.GetAll;

namespace GamingManager.Application.Features.Games.Queries.GetAll;

public record GetAllGamesQuery() : IQuery<IEnumerable<GetAllGamesResult>>;
