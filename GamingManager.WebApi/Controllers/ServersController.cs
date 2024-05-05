using CleanDomainValidation.Application;
using GamingManager.Application.Features.Servers.Commands.ChangeAddress;
using GamingManager.Application.Features.Servers.Commands.ChangeHostname;
using GamingManager.Application.Features.Servers.Commands.Create;
using GamingManager.Application.Features.Servers.Commands.Delete;
using GamingManager.Application.Features.Servers.Commands.ReceiveHeartbeat;
using GamingManager.Application.Features.Servers.Commands.Start;
using GamingManager.Application.Features.Servers.Queries.Get;
using GamingManager.Application.Features.Servers.Queries.GetAll;
using GamingManager.Contracts.Features.Servers.Commands.ChangeAddress;
using GamingManager.Contracts.Features.Servers.Commands.ChangeHostname;
using GamingManager.Contracts.Features.Servers.Commands.Create;
using GamingManager.Contracts.Features.Servers.Commands.Delete;
using GamingManager.Contracts.Features.Servers.Commands.ReceiveHeartbeat;
using GamingManager.Contracts.Features.Servers.Commands.Start;
using GamingManager.Contracts.Features.Servers.Queries.Get;
using GamingManager.Contracts.Features.Servers.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamingManager.WebApi.Controllers;

[Route("api/Servers")]
[Produces("application/json")]
public class ServersController(IMediator mediator) : ApiController
{
	/// <summary>
	/// Get all servers
	/// </summary>
	/// <response code="200">Returns servers</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Not authorized</response>
	/// <response code="403">Only admins are allowed to do this</response>
	[Authorize(Roles = "Admin")]
	[HttpGet]
	[ProducesResponseType(typeof(IEnumerable<GetAllServersResult>), 200)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<ActionResult<IEnumerable<GetAllServersResult>>> GetAll()
	{
		var result = await mediator.Send(new GetAllServersQuery());
		if (result.HasFailed) return Problem(result);


		return Ok(result.Value);
	}

	/// <summary>
	/// Get a server by its hostname
	/// </summary>
	/// <param name="hostname" example="mineserv">The hostname of the server</param>
	/// <resposnse code="200">Returns servers</resposnse>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Not authorized</response>
	/// <response code="403">Only admins are allowed to do this</response>
	/// <response code="404">Server not found</response>
	[Authorize(Roles = "Admin")]
	[HttpGet("{hostname}")]
	[ProducesResponseType(typeof(GetServerResult), 200)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<GetServerResult>> Get(string hostname)
	{
		var queryResult = Builder<GetServerQuery>
			.BindParameters(new GetServerParameters())
			.MapParameter(p => p.Hostname, hostname)
			.BuildUsing<GetServerQueryBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var result = await mediator.Send(queryResult.Value);
		if (result.HasFailed) return Problem(result);

		return Ok(result.Value);
	}

	/// <summary>
	/// Change the address of a server
	/// </summary>
	/// <param name="hostname" example="mineserv">The hostname of the server</param>
	/// <resposnse code="204">Server address updated</resposnse>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Not authorized</response>
	/// <response code="403">Only admins are allowed to do this</response>
	/// <response code="404">Server not found</response>
	/// <response code="409">Server with this adress already exists</response>
	[Authorize(Roles = "Admin")]
	[HttpPatch("{hostname}/Address")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<ActionResult> ChangeAddress(string hostname, ChangeServerAddressParameters parameters)
	{
		var queryResult = Builder<ChangeAddressCommand>
			.BindParameters(parameters)
			.MapParameter(p => p.Hostname, hostname)
			.BuildUsing<ChangeAddressCommandBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var result = await mediator.Send(queryResult.Value);
		if (result.HasFailed) return Problem(result);

		return NoContent();
	}

	/// <summary>
	/// Change the hostname of a server
	/// </summary>
	/// <param name="hostname" example="mineserv">The hostname of the server</param>
	/// <resposnse code="204">Server address updated</resposnse>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Not authorized</response>
	/// <response code="403">Only admins are allowed to do this</response>
	/// <response code="404">Server not found</response>
	/// <response code="409">Server with this hostname already exists</response>
	[Authorize(Roles = "Admin")]
	[HttpPatch("{hostname}/Hostname")]
	[ProducesResponseType(typeof(GetServerResult), 200)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<ActionResult<GetServerResult>> ChangeHostname(string hostname, ChangeHostnameParameters parameters)
	{
		var queryResult = Builder<ChangeHostnameCommand>
			.BindParameters(parameters)
			.MapParameter(p => p.CurrentHostname, hostname)
			.BuildUsing<ChangeHostnameCommandBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var result = await mediator.Send(queryResult.Value);
		if (result.HasFailed) return Problem(result);

		return NoContent();
	}

	/// <summary>
	/// Create a server
	/// </summary>
	/// <response code="200">Returns servers</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Not authorized</response>
	/// <response code="403">Only admins are allowed to do this</response>
	/// <response code="409">Server with this mac / hostname / adress already exists</response>
	[Authorize(Roles = "Admin")]
	[HttpPost]
	[ProducesResponseType(typeof(CreateServerResult), 201)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<ActionResult<CreateServerResult>> Create(CreateServerParameters parameters)
	{
		var queryResult = Builder<CreateServerCommand>
			.BindParameters(parameters)
			.BuildUsing<CreateServerCommandBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var result = await mediator.Send(queryResult.Value);
		if (result.HasFailed) return Problem(result);

		return CreatedAtAction(nameof(Get), new { hostname = result.Value.Hostname }, result.Value);
	}

	/// <summary>
	/// Delete a server
	/// </summary>
	/// <param name="hostname" example="mineserv">The hostname of the server</param>
	/// <response code="204">Server deleted succesfully</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Not authorized</response>
	/// <response code="403">Only admins are allowed to do this</response>
	/// <response code="404">Server not found</response>
	/// <response code="409">Server cannot be deleted</response>
	[Authorize(Roles = "Admin")]
	[HttpDelete("{hostname}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<ActionResult> Delete(string hostname)
	{
		var queryResult = Builder<DeleteServerCommand>
			.BindParameters(new DeleteServerParameters())
			.MapParameter(p => p.Hostname, hostname)
			.BuildUsing<DeleteServerCommandBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var result = await mediator.Send(queryResult.Value);
		if (result.HasFailed) return Problem(result);

		return NoContent();
	}

	/// <summary>
	/// Server has sent a heartbeat
	/// </summary>
	/// <param name="hostname" example="mineserv">The hostname of the server</param>
	/// <response code="204">Heartbeat received successfully</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Not authorized</response>
	/// <response code="403">Only the server itself is allowed</response>
	/// <response code="404">Server not found</response>
	/// <response code="409">Heartbeat invalid</response>
	[Authorize(Roles = "Server")]
	[HttpPut("{hostname}/Heartbeat")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult> Heartbeat(string hostname)
	{
		if (User.Identity!.Name != hostname) return Forbid();

		var queryResult = Builder<ReceiveHeartbeatCommand>
			.BindParameters(new ReceiveHeartbeatParameters())
			.MapParameter(p => p.Hostname, hostname)
			.BuildUsing<ReceiveHeartbeatCommandBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var result = await mediator.Send(queryResult.Value);
		if (result.HasFailed) return Problem(result);

		return NoContent();
	}

	/// <summary>
	/// Start a server
	/// </summary>
	/// <param name="hostname" example="mineserv">The hostname of the server</param>
	/// <response code="204">Server started successfully</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Not authorized</response>
	/// <response code="403">Only admins are allowed to do this</response>
	/// <response code="404">Server not found</response>
	/// <response code="409">Server cannot be started</response>
	[Authorize(Roles = "Admin")]
	[HttpPut("{hostname}/Start")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<ActionResult> Start(string hostname)
	{
		var queryResult = Builder<StartServerCommand>
			.BindParameters(new StartServerParameters())
			.MapParameter(p => p.Hostname, hostname)
			.BuildUsing<StartServerCommandBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var result = await mediator.Send(queryResult.Value);
		if (result.HasFailed) return Problem(result);

		return NoContent();
	}
}
