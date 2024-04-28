using GamingManager.Application.Features.Projects;
using GamingManager.Contracts.Features.Projects;
using GamingManager.Contracts.Features.Projects.Queries.Get;
using GamingManager.Contracts.Features.Projects.Queries.GetAll;
using GamingManager.Domain.Projects.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GamingManager.Infrastructure.Repositories;

public class ProjectDtoRepository(GamingManagerReadContext context) : IProjectDtoRepository
{
	public IAsyncEnumerable<GetAllProjectsResult> GetAllAsync()
	{
		return context.Projects
			.Select(project => new GetAllProjectsResult(
				project.Id.ToString(),
				project.Name))
			.AsAsyncEnumerable();
	}

	public async Task<GetProjectResult?> GetAsync(ProjectId projectId)
	{
		return await context.Projects
			.Include(project => project.Game)
			.Include(project => project.Server)
			.Include(project => project.Participants)
				.ThenInclude(participant => participant.Bans)
			.Include(project => project.Participants)
				.ThenInclude(participant => participant.Account)
			.Include(project => project.TeamMembers)
				.ThenInclude(teamMember => teamMember.User)
			.Where(project => project.Id == projectId.Value)
			.Select(project => new GetProjectResult(
				project.Id.ToString(),
				project.Name,
				new GetProjectGameResult(
					project.Game.Id.ToString(),
					project.Game.Name),
				ReferenceEquals(project.Server, null) ? null : new GetProjectGameServerResult(
					project.Server.Id.ToString(),
					project.Server.ServerName,
					project.Server.Status.ToString()
					),
				project.Participants.Select(participant => new GetProjectParticipantsResult(
					participant.Id.ToString(),
					new GetProjectParticipantAccountResult(
						participant.Account.Id.ToString(),
						participant.Account.Name
						),
					participant.Since,
					participant.Online,
					participant.PlayTime,
					participant.Bans.Select(ban => new BanResult(
						ban.Id.ToString(),
						ban.Reason,
						ban.BannedAtUtc,
						ban.Duration
						))
					.ToList()
					))
				.ToList(),
				project.TeamMembers.Select(teamMember => new GetProjectTeamMemberResult(
					teamMember.Id.ToString(),
					teamMember.Role.ToString(),
					teamMember.TeamMemberSince,
					new GetProjectTeamMemberUserResult(
						teamMember.User.Id.ToString(),
						teamMember.User.Username
						)))
				.ToList()
				))
			.FirstOrDefaultAsync();
	}
}