using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Projects.Queries.GetAll;

namespace GamingManager.Application.Features.Projects.Queries.GetAll;

public record GetAllProjectsQuery : IQuery<IEnumerable<GetAllProjectsResult>>;
