using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MediatR;
using GamingManager.Domain.Abstractions;

namespace GamingManager.Infrastructure.Interceptors;

public class PublishDomainEventsInterceptor(IPublisher publisher) : SaveChangesInterceptor
{
	private readonly IPublisher _publisher = publisher;

	public async override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
	{
		DbContext? dbContext = eventData.Context;

		await PublishDomainEvents(dbContext);
		return await base.SavingChangesAsync(eventData, result, cancellationToken);
	}

	private async Task PublishDomainEvents(DbContext? dbContext)
	{
		if (dbContext is null) return;

		var entitiesWithDomainEvents = dbContext.ChangeTracker.Entries<IHasDomainEvents>()
			.Where(entry => entry.Entity.DomainEvents.Any())
			.Select(entry => entry.Entity)
			.ToList();

		var domainEvents = entitiesWithDomainEvents
			.SelectMany(entry => entry.DomainEvents)
			.ToList();

		entitiesWithDomainEvents.ForEach(entity => entity.ClearDomainEvents());

		foreach (var domainEvent in domainEvents)
		{
			await _publisher.Publish(domainEvent);
		}
	}
}