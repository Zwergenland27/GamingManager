using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Games.DTOs;
using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Application.Features.Games.Queries.Get;

public record GetGameQuery(GameName Name) : IQuery<DetailedGameDto>;
