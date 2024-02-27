using MediatR;

namespace GamingManager.Domain.Abstractions;

/// <summary>
/// Marker interface for an domain event
/// </summary>
public interface IDomainEvent : INotification
{
}
