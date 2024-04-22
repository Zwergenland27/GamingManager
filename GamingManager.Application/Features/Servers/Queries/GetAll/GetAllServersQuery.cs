using GamingManager.Application.Abstractions;
using GamingManager.Application.Features.Servers.DTOs;

namespace GamingManager.Application.Features.Servers.Queries.GetAll;

public record GetAllServersQuery() : IQuery<IEnumerable<ShortenedServerDto>>;
