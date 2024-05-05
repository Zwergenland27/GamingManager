using CleanDomainValidation.Application;
using GamingManager.Application.Features.Accounts.Commands.AssignUser;
using GamingManager.Application.Features.Accounts.Commands.CreateFromLogin;
using GamingManager.Application.Features.Accounts.Commands.ReAssignUser;
using GamingManager.Application.Features.Accounts.Queries.GetAll;
using GamingManager.Application.Features.Games.Commands.CreateGame;
using GamingManager.Application.Features.Games.Queries.Get;
using GamingManager.Application.Features.Games.Queries.GetAll;
using GamingManager.Contracts.Features.Accounts.Commands.AssignUuid;
using GamingManager.Contracts.Features.Accounts.Commands.CreateFromUser;
using GamingManager.Contracts.Features.Accounts.Commands.ReAssignUser;
using GamingManager.Contracts.Features.Accounts.Queries.GetAllOfGame;
using GamingManager.Contracts.Features.Games.Commands.Create;
using GamingManager.Contracts.Features.Games.Queries.Get;
using GamingManager.Contracts.Features.Games.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamingManager.WebApi.Controllers;

[Route("api/Games")]
[Produces("application/json")]
public class GamesController(IMediator mediator) : ApiController
{
	/// <summary>
	/// Get a game by its name
	/// </summary>
	/// <param name="name" example="Minecraft">Name of the game</param>
	/// <response code="200">Returns game </response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Invalid request</response>
	/// <response code="404">Game not found</response>
	[Authorize]
	[HttpGet("{name}")]
	[ProducesResponseType(typeof(GetGameResult), 200)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<GetGameResult>> Get(string name)
	{
		var queryResult = Builder<GetGameQuery>
			.BindParameters(new GetGameParameters())
			.MapParameter(p => p.Name, name)
			.BuildUsing<GetGameQueryBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var result = await mediator.Send(queryResult.Value);
		if (result.HasFailed) return Problem(result);

		return Ok(result.Value);
	}

	/// <summary>
	/// Get all accounts of a game
	/// </summary>
	/// <param name="name" example="Minecraft">Name of the game</param>
	/// <response code="200">Returns accounts</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Not authorized</response>
	/// <response code="404">Game not found</response>
	[Authorize]
	[HttpGet("{name}/Accounts")]
	[ProducesResponseType(typeof(IEnumerable<GetAllAccountsResult>), 200)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<IEnumerable<GetAllAccountsResult>>> GetAllAccounts(string name)
	{
		var queryResult = Builder<GetAllAccountsQuery>
			.BindParameters(new GetAllAccountsParameters())
			.MapParameter(p => p.GameName, name)
			.BuildUsing<GetAllAccountsQueryBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var result = await mediator.Send(queryResult.Value);
		if (result.HasFailed) return Problem(result);

		return Ok(result.Value);
	}

	/// <summary>
	/// Get all games
	/// </summary>
	/// <response code="200">Returns games</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Not authorized</response>
	/// <response code="404">Game not found</response>
	[Authorize]
	[HttpGet]
	[ProducesResponseType(typeof(IEnumerable<GetAllGamesResult>), 200)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<IEnumerable<GetAllGamesResult>>> GetAll()
	{
		var result = await mediator.Send(new GetAllGamesQuery());
		if (result.HasFailed) return Problem(result);

		return Ok(result.Value);
	}

	/// <summary>
	/// Assign a uuid to an account
	/// </summary>
	/// <param name="gameName" example="Minecraft">Name of the game</param>
	/// <param name="accountName" example="Zwergenland">Name of the account</param>
	/// <response code="204">User assigned to account successfully</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Forbidden</response>
	/// <response code="404">Game not found or account name not found</response>
	[Authorize(Roles = "Server")]
	[HttpPut("{gameName}/Accounts/{accountName}/Uuid")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult> AssignUuidToAccount(string gameName, string accountName, AssignUuidParameters parameters)
	{
		var queryResult = Builder<AssignUuidCommand>
			.BindParameters(parameters)
			.MapParameter(p => p.GameName, gameName)
			.MapParameter(p => p.AccountName, accountName)
			.BuildUsing<AssignUuidCommandBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var assignResult = await mediator.Send(queryResult.Value);
		if (assignResult.HasFailed) return Problem(assignResult);

		return NoContent();
	}

	/// <summary>
	/// Create a new account
	/// </summary>
	/// <param name="gameName" example="Minecraft">Name of the game</param>
	/// <response code="200">User assigned to account successfully</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Forbidden</response>
	/// <response code="404">Game not found or account name not found</response>
	[Authorize]
	[HttpPost("{gameName}/Accounts")]
	[ProducesResponseType(typeof(CreateFromUserResult), 200)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<CreateFromUserResult>> CreateAccount(string gameName, CreateFromUserParameters parameters)
	{
		var queryResult = Builder<CreateFromUserCommand>
			.BindParameters(parameters)
			.MapParameter(p => p.GameName, gameName)
			.MapParameter(p => p.Username, User.Identity!.Name)
			.BuildUsing<CreateFromUserCommandBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var assignResult = await mediator.Send(queryResult.Value);
		if (assignResult.HasFailed) return Problem(assignResult);

		return Ok(assignResult.Value);
	}

	/// <summary>
	/// Reassign a user to an account
	/// </summary>
	/// <param name="gameName" example="Minecraft">Name of the game</param>
	/// <param name="accountName" example="Zwergenland">Name of the account</param>
	/// <response code="204">User assigned to account successfully</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only admins can do that</response>
	/// <response code="404">Game not found or account name not found</response>
	[Authorize(Roles = "Admin")]
	[HttpPatch("{gameName}/Accounts/{accountName}/User")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult> ReAssignUserAccount(string gameName, string accountName, ReAssignUserParameters parameters)
	{
		var queryResult = Builder<ReAssignUserCommand>
			.BindParameters(parameters)
			.MapParameter(p => p.GameName, gameName)
			.MapParameter(p => p.AccountName, accountName)
			.BuildUsing<ReAssignUserCommandBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var assignResult = await mediator.Send(queryResult.Value);
		if (assignResult.HasFailed) return Problem(assignResult);

		return NoContent();
	}

	/// <summary>
	/// Create a new game
	/// </summary>
	/// <response code="201">Game created succesfully</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only admins can do that</response>
	/// <response code="409">Game with this name already exists</response>
	[Authorize(Roles = "Admin")]
	[HttpPost]
	[ProducesResponseType(typeof(CreateGameResult), 201)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<ActionResult<CreateGameResult>> Create(CreateGameParameters parameters)
	{
		var queryResult = Builder<CreateGameCommand>
			.BindParameters(parameters)
			.BuildUsing<CreateGameCommandBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var result = await mediator.Send(queryResult.Value);
		if (result.HasFailed) return Problem(result);

		return CreatedAtAction(nameof(Get), new { username = result.Value.Name }, result.Value);
	}
}
