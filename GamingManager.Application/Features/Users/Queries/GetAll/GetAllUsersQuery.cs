using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Users.DTOs;

namespace GamingManager.Application.Features.Users.Queries.GetAll;

public record GetAllUsersQuery() : IQuery<IEnumerable<ShortenedUserDto>>;
