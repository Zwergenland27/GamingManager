using GamingManager.Application.Abstractions;
using GamingManager.Contracts.Features.Accounts.DTOs;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Application.Features.Accounts.Queries.Get;

public record GetAccountQuery(
	GameName GameName,
	AccountName AccountName) : IQuery<DetailedAccountDto>;
