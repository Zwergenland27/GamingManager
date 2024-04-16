using GamingManager.Application.Abstractions;
using GamingManager.Application.Features.Games.DTOs;

namespace GamingManager.Application.Features.Games.Queries.GetAllGames;

public record GetAllGamesQuery() : IQuery<IEnumerable<ShortenedGameDto>>;
