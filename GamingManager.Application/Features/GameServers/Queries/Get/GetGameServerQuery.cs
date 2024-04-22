using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.GameServers.DTOs;
using GamingManager.Domain.GameServers.ValueObjects;

namespace GamingManager.Application.Features.GameServers.Queries.Get;

public record GetGameServerQuery(GameServerName ServerName) : IQuery<DetailedGameServerDto>;
