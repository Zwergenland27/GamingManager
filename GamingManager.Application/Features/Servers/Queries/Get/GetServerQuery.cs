using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Servers.DTOs;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Application.Features.Servers.Queries.Get;

public record GetServerQuery(Hostname Hostname) : IQuery<DetailedServerDto>;
