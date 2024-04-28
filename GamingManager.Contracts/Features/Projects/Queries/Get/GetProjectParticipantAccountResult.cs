using GamingManager.Contracts.Features.Accounts;

namespace GamingManager.Contracts.Features.Projects.Queries.Get;

public class GetProjectParticipantAccountResult(
	string Id,
	string Name) : AccountCoreResult(Id, Name)
{
}
