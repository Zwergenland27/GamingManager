using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.GameServers.DTOs;

namespace GamingManager.Application.Features.GameServers.Queries.GetAll;

public record GetAllGameServersQuery() : IQuery<IEnumerable<ShortenedGameServerDto>>;
