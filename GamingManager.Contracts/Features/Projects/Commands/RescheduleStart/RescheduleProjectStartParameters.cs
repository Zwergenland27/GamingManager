using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Projects.Commands.RescheduleStart;

/// <summary>
/// Parameters for rescheduling the start of a project
/// </summary>
public class RescheduleProjectStartParameters : IParameters
{
	/// <summary>
	/// Id of the user that is adding the member
	/// </summary>
	[JsonIgnore]
	public string? AuditorId { get; set; }

	///<summary>
	///Unique id of the project
	///</summary>
	///<example>00000000-0000-0000-0000-000000000000</example>
	[JsonIgnore]
    public string? ProjectId { get; set; }

    ///<summary>
    ///	The new start date of the project
    ///	</summary>
    [Required]
    public DateTime? PlannedStartUtc { get; set; }
}
