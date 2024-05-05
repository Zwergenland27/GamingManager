using CleanDomainValidation.Application;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Projects.Commands.Pardon;

/// <summary>
/// Parameters for pardoning an account
/// </summary>
public class PardonParticipantParameters : IParameters
{
	/// <summary>
	/// Id of the user that is adding the member
	/// </summary>
	[JsonIgnore]
	public string? AuditorId { get; set; }

	///<summary>
	/// Unique id of the project
	/// </summary>
	/// <example>00000000-0000-0000-0000-000000000000</example>
	[JsonIgnore]
	public string? ProjectId { get; set; }

	///<summary>
	/// Unique id of the participant
	/// </summary>
	/// <example>00000000-0000-0000-0000-000000000000</example>
	[JsonIgnore]
	public string? ParticipantId { get; set; }
}
