using CleanDomainValidation.Application;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Projects.Queries;

/// <summary>
/// Parameters for quering a project.
/// </summary>
public class GetProjectParameters : IParameters
{
	///<summary>
	///Unique id of the project
	///</summary>
	///<example>00000000-0000-0000-0000-000000000000</example>
	[JsonIgnore]
	public string? ProjectId { get; set; }
}
