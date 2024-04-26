using CleanDomainValidation.Application;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Projects.Commands;

/// <summary>
/// Parameters for finishing a project
/// </summary>
public class FinishProjectParameters : IParameters
{
	///<summary>
	/// Unique id of the project
	/// </summary>
	/// <example>00000000-0000-0000-0000-000000000000</example>
	[JsonIgnore]
	public string? ProjectId { get; set; }
}
