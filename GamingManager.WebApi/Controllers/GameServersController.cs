using CleanDomainValidation.Application;
using GamingManager.Application.Features.GameServers.Commands.ChangeShutdownDelay;
using GamingManager.Application.Features.GameServers.Commands.ShuttedDown;
using GamingManager.Application.Features.GameServers.Commands.Start;
using GamingManager.Application.Features.GameServers.Commands.Started;
using GamingManager.Application.Features.GameServers.Commands.UseServer;
using GamingManager.Application.Features.GameServers.Events.Crashed;
using GamingManager.Application.Features.GameServers.Events.Create;
using GamingManager.Application.Features.GameServers.Events.Delete;
using GamingManager.Application.Features.GameServers.Queries.Get;
using GamingManager.Contracts.Features.GameServers.Commands.ChangeShutdownDelay;
using GamingManager.Contracts.Features.GameServers.Commands.Crashed;
using GamingManager.Contracts.Features.GameServers.Commands.Create;
using GamingManager.Contracts.Features.GameServers.Commands.Delete;
using GamingManager.Contracts.Features.GameServers.Commands.ShuttedDown;
using GamingManager.Contracts.Features.GameServers.Commands.Start;
using GamingManager.Contracts.Features.GameServers.Commands.Started;
using GamingManager.Contracts.Features.GameServers.Commands.UseServer;
using GamingManager.Contracts.Features.GameServers.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamingManager.WebApi.Controllers;

[Route("api/GameServers")]
[Produces("application/json")]
public class GameServersController(IMediator mediator) : ApiController
{
	/// <summary>
	/// Get a game server by its name
	/// </summary>
	/// <param name="name" example="mineserv">name of the game server</param>
	/// <response code="200">Get the game server</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only admins are allowed to access this request</response>
	/// <response code="404">Game server not found</response>
	[Authorize(Roles = "Admin")]
	[HttpGet("{name}")]
	[ProducesResponseType(typeof(GetGameServerResult), 200)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<GetGameServerResult>> GetGameServer(string name)
	{
		var queryResult = Builder<GetGameServerQuery>
			.BindParameters(new GetGameServerParameters())
			.MapParameter(p => p.Name, name)
			.BuildUsing<GetGameServerQueryBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var result = await mediator.Send(queryResult.Value);
		if (result.HasFailed) return Problem(result);

		return Ok(result.Value);
	}

	/// <summary>
	/// cancel the shutdown of a game server
	/// </summary>
	/// <param name="name" example="mineserv">Name of the game server</param>
	/// <response code="204">Shutdown cancelled</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only admins are allowed to access this request</response>
	/// <response code="404">Game server not found</response>
	[Authorize(Roles = "Admin")]
	[HttpPatch("{name}/Shutdown/cancel")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult> CancelShutdown(string name)
	{
		var queryResult = Builder<GetGameServerQuery>
			.BindParameters(new GetGameServerParameters())
			.MapParameter(p => p.Name, name)
			.BuildUsing<GetGameServerQueryBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var result = await mediator.Send(queryResult.Value);
		if (result.HasFailed) return Problem(result);

		return NoContent();
	}

	/// <summary>
	/// Change the shutdown delay of a serer
	/// </summary>
	/// <param name="name" example="mineserv">Name of the game server</param>
	/// <param name="parameters"></param>
	/// <response code="204">Shutdown delay changed</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only admins are allowed to access this request</response>
	/// <response code="404">Game server not found</response>
	[Authorize(Roles = "Admin")]
	[HttpPatch("{name}/Shutdown/Delay")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult> DelayShutdown(string name, ChangeGameServerShutdownDelayParameters parameters)
	{
		var queryResult = Builder<ChangeShutdownDelayCommand>
			.BindParameters(parameters)
			.MapParameter(p => p.Name, name)
			.BuildUsing<ChangeShutdownDelayCommandBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var result = await mediator.Send(queryResult.Value);
		if (result.HasFailed) return Problem(result);

		return NoContent();
	}

	/// <summary>
	/// Mark that the game server crashed
	/// </summary>
	/// <param name="name" example="mineserv">Name of the game server</param>
	/// <response code="204">Shutdown delay changed</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only the game server and an admin is allowed to do that</response>
	/// <response code="404">Game server not found</response>
	[Authorize(Roles = "GameServer, Admin")]
	[HttpPatch("{name}/crashed")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult> Crashed(string name)
	{
		if (User.IsInRole("GameServer") && User.Identity!.Name != name) return Forbid();

		var queryResult = Builder<GameServerCrashedCommand>
			.BindParameters(new GameServerCrashedParameters())
			.MapParameter(p => p.Name, name)
			.BuildUsing<GameServerCrashedCommandBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var result = await mediator.Send(queryResult.Value);
		if (result.HasFailed) return Problem(result);

		return NoContent();
	}

	/// <summary>
	/// Mark that the game server crashed
	/// </summary>
	/// <response code="201">Game server has been created</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only admins can call this request</response>
	/// <response code="404">Game server not found</response>
	[Authorize(Roles = "Admin")]
	[HttpPost]
	[ProducesResponseType(typeof(CreateGameServerResult), 201)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<CreateGameServerResult>> CreateGameServer(CreateGameServerParameters parameters)
	{
		var queryResult = Builder<CreateGameServerCommand>
			.BindParameters(parameters)
			.BuildUsing<CreateGameServerCommandBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var result = await mediator.Send(queryResult.Value);
		if (result.HasFailed) return Problem(result);

		return CreatedAtAction(nameof(GetGameServer), new { name = result.Value.Name }, result.Value);
	}

	/// <summary>
	/// Mark that the game server crashed
	/// </summary>
	/// <param name="name" example="mineserv">Name of the game server</param>
	/// <response code="204">Server has been deleted</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only admins can call this request</response>
	/// <response code="404">Game server not found</response>
	[Authorize(Roles = "Admin")]
	[HttpDelete("{name}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<CreateGameServerResult>> DeleteGameServer(string name)
	{
		var queryResult = Builder<DeleteGameServerCommand>
			.BindParameters(new DeleteGameServerParameters())
			.MapParameter(p => p.Name, name)
			.BuildUsing<DeleteGameServerCommandBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var result = await mediator.Send(queryResult.Value);
		if (result.HasFailed) return Problem(result);

		return NoContent();
	}

	/// <summary>
	/// Mark that the game has been shut down
	/// </summary>
	/// <param name="name" example="mineserv">Name of the game server</param>
	/// <response code="204">Server shutted down</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only the game server and admins are allowed to do this</response>
	/// <response code="404">Game server not found</response>
	[Authorize(Roles = "GameServer, Admin")]
	[HttpPatch("{name}/shutted-down")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult> ShuttedDown(string name)
	{
		if (User.IsInRole("GameServer") && User.Identity!.Name != name) return Forbid();

		var queryResult = Builder<GameServerShuttedDownCommand>
			.BindParameters(new GameServerShuttedDownParameters())
			.MapParameter(p => p.Name, name)
			.BuildUsing<GameServerShuttedDownCommandBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var result = await mediator.Send(queryResult.Value);
		if (result.HasFailed) return Problem(result);

		return NoContent();
	}

	/// <summary>
	/// Start the game server
	/// </summary>
	/// <param name="name" example="mineserv">Name of the game server</param>
	/// <response code="204">Server starts</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only admins can call this request</response>
	/// <response code="404">Game server not found</response>
	[Authorize(Roles = "Admin")]
	[HttpPatch("{name}/start")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult> Start(string name)
	{
		var queryResult = Builder<StartGameServerCommand>
			.BindParameters(new StartGameServerParameters())
			.MapParameter(p => p.Name, name)
			.BuildUsing<StartGameServerCommandBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var result = await mediator.Send(queryResult.Value);
		if (result.HasFailed) return Problem(result);

		return NoContent();
	}

	/// <summary>
	/// Mark that the game has been started
	/// </summary>
	/// <param name="name" example="mineserv">Name of the game server</param>
	/// <response code="204">Server has been started</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only servers and admins can call this request</response>
	/// <response code="404">Game server not found</response>
	[Authorize(Roles = "GameServer")]
	[HttpPatch("{name}/started")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult> Started(string name)
	{
		if (User.Identity!.Name != name) return Forbid();

		var queryResult = Builder<GameServerStartedCommand>
			.BindParameters(new GameServerStartedParameters())
			.MapParameter(p => p.Name, name)
			.BuildUsing<GameServerStartedCommandBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var result = await mediator.Send(queryResult.Value);
		if (result.HasFailed) return Problem(result);

		return NoContent();
	}

	/// <summary>
	/// Select the server which hosts the game server
	/// </summary>
	/// <param name="name" example="mineserv">Name of the game server</param>
	/// <response code="204">New Server selected</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only admins can call this request</response>
	/// <response code="404">Game server not found</response>
	[Authorize(Roles = "Admin")]
	[HttpPatch("{name}/Server")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult> UseServer(string name, UseServerParameters parameters)
	{
		var queryResult = Builder<UseServerCommand>
			.BindParameters(parameters)
			.MapParameter(p => p.Name, name)
			.BuildUsing<UseServerCommandBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var result = await mediator.Send(queryResult.Value);
		if (result.HasFailed) return Problem(result);

		return NoContent();
	}
}
