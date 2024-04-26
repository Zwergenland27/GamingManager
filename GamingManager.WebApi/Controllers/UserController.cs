using CleanDomainValidation.Application;
using GamingManager.Application.Features.Users.Commands.Create;
using GamingManager.Contracts.Features.Users.Commands;
using GamingManager.Contracts.Features.Users.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GamingManager.WebApi.Controllers;

[Route("api/Users")]
[Produces("application/json")]
public class UserController(IMediator mediator) : ApiController
{
	/// <summary>
	/// Create a new user
	/// </summary>
	/// <response code="201">Returns new created user</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="409">The username or the email is already used by another user</response>
	[HttpPost]
	[ProducesResponseType(typeof(DetailedUserDto), 201)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<ActionResult<DetailedUserDto>> Create(CreateUserParameters parameters)
	{
		var commandResult = Builder<CreateUserCommand>
			.BindParameters(parameters)
			.BuildUsing<CreateUserConfiguration>();
		if (commandResult.HasFailed) return Problem(commandResult);

		var result = await mediator.Send(commandResult.Value);
		if (result.HasFailed) return Problem(result);

		return Ok(result.Value);
	}
}
