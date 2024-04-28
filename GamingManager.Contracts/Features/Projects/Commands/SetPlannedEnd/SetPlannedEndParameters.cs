using CleanDomainValidation.Application;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamingManager.Contracts.Features.Projects.Commands.SetPlannedEnd;

/// <summary>
/// Parameters for setting the planned end of a project
/// </summary>
public class SetPlannedEndParameters : IParameters
{
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
    public DateTime? PlannedEndUtc { get; set; }
}
