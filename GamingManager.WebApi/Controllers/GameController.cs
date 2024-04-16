using CleanDomainValidation.Application;
using GamingManager.Application.Features.Games.Commands.CreateGame;
using GamingManager.Contracts.Features.Games.Commands;
using GamingManager.Contracts.Features.Games.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GamingManager.WebApi.Controllers;

[Route("api/Games")]
[Produces("application/json")]
public class GameController(IMediator mediator) : ApiController
{
	/// <summary>
	/// Create a new game
	/// </summary>
	/// <response code="201">Returns new created game</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="409">The game name is already used</response>
	[HttpPost]
	[ProducesResponseType(typeof(DetailedGame), 201)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<ActionResult<DetailedGame>> Create(CreateGameParameters parameters)
	{
		var commandResult = Builder<CreateGameCommand>
			.BindParameters(parameters)
			.BuildUsing<CreateGameConfiguration>();
		if (commandResult.HasFailed) return Problem(commandResult);

		var result = await mediator.Send(commandResult.Value);
		if (result.HasFailed) return Problem(result);

		return Ok(result.Value.Convert());
	}
}
