namespace GamingManager.Application.Abstractions;

/// <summary>
/// Unit of work
/// </summary>
public interface IUnitOfWork : IDisposable
{
	/// <summary>
	/// Saves all changes to the database
	/// </summary>
	Task<int> SaveAsync(CancellationToken cancellationToken);
}
