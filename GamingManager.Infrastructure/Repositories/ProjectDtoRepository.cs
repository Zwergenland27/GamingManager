using GamingManager.Application.Features.Accounts;
using GamingManager.Application.Features.Games;
using GamingManager.Application.Features.Projects;
using GamingManager.Application.Features.Projects.DTOs;
using GamingManager.Application.Features.Users;
using GamingManager.Contracts.Features.GameServers.DTOs;
using GamingManager.Domain.Projects.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GamingManager.Infrastructure.Repositories;

internal class ProjectDtoRepository(GamingManagerContext context) : IProjectDtoRepository
{
	public IAsyncEnumerable<ShortenedProjectDto> GetAllAsync()
	{
		return context.Projects
			.AsNoTracking()
			.IgnoreAutoIncludes()
			.Join(context.Games.AsNoTracking(),
				project => project.Game,
				game => game.Id,
				(project, game) => new ShortenedProjectDto(
					project.Id.Value.ToString(),
					project.Name.Value,
					game.ToDto()))
			.AsAsyncEnumerable();
	}

	public async Task<DetailedProjectDto?> GetDetailedAsync(ProjectId projectId)
	{
		var project = await context.Projects
			.AsNoTracking()
			.Where(project => project.Id == projectId)
			.Join(context.Games.AsNoTracking(),
				project => project.Game,
				game => game.Id,
				(project, game) => new {
					project.Id,
					project.Name,
					Game = game.ToDto(),
					project.Start,
					project.End,
					Members = project.Team,
					project.Participants
				})
			.Join(context.GameServers.AsNoTracking(),
				prevJoin => prevJoin.Id,
				gameServer => gameServer.Project,
				(prevJoin, gameServer) => new
				{
					prevJoin.Id,
					prevJoin.Name,
					prevJoin.Game,
					prevJoin.Start,
					prevJoin.End,
					prevJoin.Members,
					prevJoin.Participants,
					GameServer = ReferenceEquals(gameServer, null) ? null : new ShortenedGameServerForProjectDto(
						gameServer.Id.Value.ToString(),
						gameServer.ServerName.Value)
				})
			.FirstOrDefaultAsync();

		if (project is null) return null;

		var members = project.Members
			.Join(context.Users.AsNoTracking(),
				member => member.User,
				user => user.Id,
				(member, user) => new DetailedMemberDto(
					member.Id.Value.ToString(),
					member.Role.ToString(),
					member.Since.Value,
					user.ToDto()))
			.ToList();

		var participants = project.Participants
			.Join(context.Accounts.AsNoTracking(),
				participant => participant.Account,
				account => account.Id,
				(participant, account) => new DetailedParticipantDto(
					participant.Id.Value.ToString(),
					account.ToDto(),
					participant.Since.Value,
					participant.Online,
					participant.PlayTime))
			.ToList();

		return new DetailedProjectDto(
			project.Id.Value.ToString(),
			project.Name.Value,
			project.Game,
			project.GameServer,
			project.Start.Value,
			project.End?.Value,
			members,
			participants);

	}
}
