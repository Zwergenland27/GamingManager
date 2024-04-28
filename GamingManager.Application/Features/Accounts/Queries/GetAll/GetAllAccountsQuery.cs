using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Accounts.Queries.GetAllOfGame;
using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Application.Features.Accounts.Queries.GetAll;

public record GetAllAccountsQuery(GameName GameName) : IQuery<IEnumerable<GetAllAccountsResult>>;
