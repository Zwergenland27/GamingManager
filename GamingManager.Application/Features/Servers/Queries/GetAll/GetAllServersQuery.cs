using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Servers.Queries.GetAll;

namespace GamingManager.Application.Features.Servers.Queries.GetAll;

public record GetAllServersQuery() : IQuery<IEnumerable<GetAllServersResult>>;
