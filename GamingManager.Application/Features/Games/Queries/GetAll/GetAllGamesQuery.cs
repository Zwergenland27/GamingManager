using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Games.Queries;

namespace GamingManager.Application.Features.Games.Queries.GetAll;

public record GetAllGamesQuery() : IQuery<IEnumerable<GetAllGamesResult>>;
