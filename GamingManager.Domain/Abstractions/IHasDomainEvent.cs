namespace GamingManager.Domain.Abstractions;

/// <summary>
/// Marker interface
/// </summary>
public interface IHasDomainEvents
{
	/// <summary>
	/// Events which occured on this entity
	/// </summary>
	IReadOnlyList<IDomainEvent> DomainEvents { get; }

	/// <summary>
	/// Removes all domain events
	/// </summary>
	void ClearDomainEvents();
}
