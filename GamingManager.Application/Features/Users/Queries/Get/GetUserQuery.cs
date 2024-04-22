using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Users.DTOs;
using GamingManager.Domain.Users.ValueObjects;

namespace GamingManager.Application.Features.Users.Queries.Get;

public record GetUserQuery(Username Username) : IQuery<DetailedUserDto>;
