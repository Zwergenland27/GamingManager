using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Games.DTOs;

namespace GamingManager.Application.Features.Games.Queries.GetAll;

public record GetAllGamesQuery() : IQuery<IEnumerable<ShortenedGameDto>>;
