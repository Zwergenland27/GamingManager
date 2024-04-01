using GamingManager.Application.Abstractions;

namespace GamingManager.Infrastructure.Repositories;

public class UnitOfWork(GamingManagerContext context) : IUnitOfWork
{
	private readonly GamingManagerContext _context = context;

	public async Task<int> SaveAsync(CancellationToken cancellationToken)
	{
		return await _context.SaveChangesAsync(cancellationToken);
	}

	private bool disposed = false;

	protected virtual void Dispose(bool disposing)
	{
		if (!disposed)
		{
			if (disposing)
			{
				_context.Dispose();
			}
		}
		disposed = true;
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
}
