using System.ComponentModel.DataAnnotations;

namespace GamingManager.Contracts.Features.Servers.Queries.GetAll;

public class GetAllServersResult(
	string Id,
	string Hostname,
	string Status,
	string Mac,
	string Address) : ServerCoreResult(Id, Hostname, Status)
{

	/// <summary>
	/// Mac address of the server
	/// </summary>
	/// <example>AF:FE:DE:AD:DE:AD</example>
	[Required]
	public string Mac { get; init; } = Mac;

	/// <summary>
	/// Address address of the server
	/// </summary>
	/// <example>192.168.1.1</example>
	[Required]
	public string Address { get; init; } = Address;
}
