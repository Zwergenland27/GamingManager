using GamingManager.Application.Abstractions;
using GamingManager.Application.Features.Projects.DTOs;

namespace GamingManager.Application.Features.Projects.Queries.GetAll;

public record GetAllProjectsQuery : IQuery<IEnumerable<ShortenedProjectDto>>;
