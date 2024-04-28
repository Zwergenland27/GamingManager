using GamingManager.Application.Features.Users;
using GamingManager.Contracts.Features.Users.Queries.Get;
using GamingManager.Contracts.Features.Users.Queries.GetAll;
using GamingManager.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GamingManager.Infrastructure.Repositories;

public class UserDtoRepository(GamingManagerReadContext context) : IUserDtoRepository
{
	public IAsyncEnumerable<GetAllUsersResult> GetAllAsync()
	{
		return context.Users
			.Select(user => new GetAllUsersResult(
				user.Id.ToString(),
				user.Username,
				user.Email,
				user.Role.ToString(),
				user.Firstname,
				user.Lastname,
				user.EmailConfirmed))
			.AsAsyncEnumerable();
	}

	public Task<GetUserResult?> GetAsync(Username username)
	{
		return context.Users
			.Include(user => user.Accounts)
				.ThenInclude(account => account.Game)
			.Include(user => user.MemberOfTeam)
				.ThenInclude(member => member.Project)
			.Where(user => user.Username == username.Value)
			.Select(user => new GetUserResult(
				user.Id.ToString(),
				user.Username,
				user.Email,
				user.Role.ToString(),
				user.Firstname,
				user.Lastname,
				user.EmailConfirmed,
				user.Accounts.Select(account => new GetUserAccountResult(
					account.Id.ToString(),
					account.Name,
					new GetUserAccountGameResult(
						account.Game.Id.ToString(),
						account.Game.Name)))
				.ToList(),
				user.MemberOfTeam.Select(teamMember => new GetUserProjectsResult(
					teamMember.Project.Id.ToString(),
					teamMember.Project.Name))
				.ToList()))
			.FirstOrDefaultAsync();
	}
}
