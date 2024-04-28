using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Users.Queries.GetAll;

namespace GamingManager.Application.Features.Users.Queries.GetAll;

public record GetAllUsersQuery() : IQuery<IEnumerable<GetAllUsersResult>>;
