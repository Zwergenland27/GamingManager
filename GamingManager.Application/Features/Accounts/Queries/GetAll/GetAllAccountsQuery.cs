using GamingManager.Application.Abstractions;
using GamingManager.Application.Features.Accounts.DTOs;
using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Application.Features.Accounts.Queries.GetAll;

public record GetAllAccountsQuery(GameName GameName) : IQuery<IEnumerable<ShortenedAccountDto>>;
