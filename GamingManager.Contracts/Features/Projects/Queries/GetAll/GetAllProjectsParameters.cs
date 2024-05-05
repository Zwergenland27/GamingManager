using CleanDomainValidation.Application;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Projects.Queries.GetAll;

/// <summary>
/// Parameters for quering all projects
/// </summary>
public class GetAllProjectsParameters : IParameters
{
	/// <summary>
	/// Id of the user that is adding the member
	/// </summary>
	[JsonIgnore]
	public string? AuditorId { get; set; }
}
