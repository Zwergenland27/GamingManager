using GamingManager.Application.Abstractions;
using GamingManager.Application.Features.Projects.DTOs;
using GamingManager.Domain.Projects.ValueObjects;

namespace GamingManager.Application.Features.Projects.Queries.Get;

public record GetProjectQuery(ProjectId ProjectId) : IQuery<DetailedProjectDto>;
