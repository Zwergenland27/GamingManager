using CleanDomainValidation.Domain;
using GamingManager.Domain.DomainErrors;
using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Domain.GameServerRequests.ValueObjects;

/// <summary>
/// Strongly typed id for <see cref="GameServerTicket"/>
/// </summary>
public sealed record GameServerTicketId(Guid Value)
{
	/// <summary>
	/// Creates new unique <see cref="GameId"/>
	/// </summary>
	public static GameServerTicketId CreateNew()
	{
		return new GameServerTicketId(Guid.NewGuid());
	}

	/// <summary>
	/// Creates <see cref="GameId"/> instance from <paramref name="value"/>
	/// </summary>
	public static CanFail<GameServerTicketId> Create(string value)
	{
		if (Guid.TryParse(value, out var guid))
		{
			return new GameServerTicketId(guid);
		}

		return Errors.GameServerTickets.Id.Invalid;
	}
}
