using CleanDomainValidation.Application;
using GamingManager.Application.Features.Users.Commands.ChangePasword;
using GamingManager.Application.Features.Users.Commands.Delete;
using GamingManager.Application.Features.Users.Commands.EditPersonalData;
using GamingManager.Application.Features.Users.Commands.Login;
using GamingManager.Application.Features.Users.Commands.RefreshJwtToken;
using GamingManager.Application.Features.Users.Commands.Register;
using GamingManager.Application.Features.Users.Commands.RequestEmailVerification;
using GamingManager.Application.Features.Users.Commands.RequestPasswordReset;
using GamingManager.Application.Features.Users.Commands.VerifyEmail;
using GamingManager.Application.Features.Users.Queries.Get;
using GamingManager.Application.Features.Users.Queries.GetAll;
using GamingManager.Contracts.Features.Users.Commands.ChangePassword;
using GamingManager.Contracts.Features.Users.Commands.DeleteUser;
using GamingManager.Contracts.Features.Users.Commands.EditPersonalData;
using GamingManager.Contracts.Features.Users.Commands.Login;
using GamingManager.Contracts.Features.Users.Commands.RefreshJwtToken;
using GamingManager.Contracts.Features.Users.Commands.Register;
using GamingManager.Contracts.Features.Users.Commands.RequestEmailVerification;
using GamingManager.Contracts.Features.Users.Commands.RequestPasswordReset;
using GamingManager.Contracts.Features.Users.Commands.VerifyEmail;
using GamingManager.Contracts.Features.Users.Queries.Get;
using GamingManager.Contracts.Features.Users.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamingManager.WebApi.Controllers;

[Route("api/Users")]
[Produces("application/json")]
public class UserController(IMediator mediator) : ApiController
{

	[Authorize]
	[HttpGet("{username}")]
	[ProducesResponseType(typeof(GetUserResult), 200)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<GetUserResult>> Get(string username)
	{
		if (username != User.Identity!.Name) return Unauthorized();

		var queryResult = Builder<GetUserQuery>
			.BindParameters(new GetUserParameters())
			.MapParameter(p => p.Username, username)
			.BuildUsing<GetUserQueryBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var result = await mediator.Send(queryResult.Value);
		if (result.HasFailed) return Problem(result);

		return Ok(result.Value);
	}

	[Authorize(Roles = "Admin")]
	[HttpGet]
	[ProducesResponseType(typeof(IEnumerable<GetAllUsersResult>), 200)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<IEnumerable<GetAllUsersResult>>> GetAll()
	{
		
		var result = await mediator.Send(new GetAllUsersQuery());
		if (result.HasFailed) return Problem(result);
		
		return Ok(result.Value);
	}

	/// <summary>
	/// Register a new user
	/// </summary>
	/// <response code="201">Returns new created user</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="409">The username or the email is already used by another user</response>
	[HttpPost("register")]
	[ProducesResponseType(typeof(RegisterUserResult), 201)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<ActionResult<RegisterUserResult>> Register(RegisterUserParameters parameters)
	{
		var commandResult = Builder<RegisterUserCommand>
			.BindParameters(parameters)
			.BuildUsing<RegisterUserCommandBuilder>();
		if (commandResult.HasFailed) return Problem(commandResult);

		var result = await mediator.Send(commandResult.Value);
		if (result.HasFailed) return Problem(result);

		return CreatedAtAction(nameof(Get), new { username = result.Value.Username }, result.Value);
	}

	/// <summary>
	/// Login
	/// </summary>
	/// <response code="200">Returns jtw access token</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Invalid username / email or password</response>
	[HttpPost("login")]
	[ProducesResponseType(typeof(LoginResult), 201)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<LoginResult>> Login(LoginParameters parameters)
	{
		var commandResult = Builder<LoginCommand>
			.BindParameters(parameters)
			.BuildUsing<LoginCommandBuilder>();
		if (commandResult.HasFailed) return Problem(commandResult);

		var result = await mediator.Send(commandResult.Value);
		if (result.HasFailed) return Problem(result);

		var jwtToken = result.Value.Item1;
		var refreshToken = result.Value.Item2;

		var cookieOptions = new CookieOptions
		{
			HttpOnly = true,
			Secure = true,
			Expires = refreshToken.Expires
		};

		Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

		return Ok(new LoginResult(jwtToken));
	}

	/// <summary>
	/// Login
	/// </summary>
	/// <param name="username">Username of the user</param>
	/// <response code="200">Returns jtw access token</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Invalid username / email or password</response>
	[HttpPatch("{username}/Token/refresh")]
	[ProducesResponseType(typeof(RefreshJwtTokenResult), 201)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<RefreshJwtTokenResult>> Login(string username)
	{
		var refreshToken = Request.Cookies["refreshToken"];
		if (refreshToken is null) return Unauthorized();

		var commandResult = Builder<RefreshJwtTokenCommand>
			.BindParameters(new RefreshJwtTokenParameters())
			.MapParameter(p => p.Username, username)
			.MapParameter(p => p.RefreshToken, refreshToken)
			.BuildUsing<RefreshJwtTokenCommandBuilder>();
		if (commandResult.HasFailed) return Problem(commandResult);

		var result = await mediator.Send(commandResult.Value);
		if (result.HasFailed) return Problem(result);

		var jwtToken = result.Value.Item1;
		var newRefreshToken = result.Value.Item2;

		var cookieOptions = new CookieOptions
		{
			HttpOnly = true,
			Secure = true,
			Expires = newRefreshToken.Expires
		};

		Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

		return Ok(new RefreshJwtTokenResult(jwtToken));
	}

	/// <summary>
	/// Change password
	/// </summary>
	/// <param name="username">username of the user</param>
	/// <response code="200">New password has been set</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="404">User not found</response>
	[Authorize]
	[HttpPatch("{username}/Password")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult> ChangePassword(string username, ChangePasswordParameters parameters)
	{
		if (username != User.Identity!.Name) return Unauthorized();

		var commandResult = Builder<ChangePasswordCommand>
			.BindParameters(parameters)
			.MapParameter(p => p.Username, username)
			.BuildUsing<ChangePasswordCommandBuilder>();
		if (commandResult.HasFailed) return Problem(commandResult);

		var result = await mediator.Send(commandResult.Value);
		if (result.HasFailed) return Problem(result);

		return Ok();
	}

	/// <summary>
	/// Request password reset
	/// </summary>
	/// <param name="email">email of the user</param>
	/// <response code="200">Email verification has been sent</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="404">User not found</response>
	[HttpPut("{email}/Password/reset")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult> ResetPassword(string email)
	{
		var commandResult = Builder<RequestPasswordResetCommand>
			.BindParameters(new RequestPasswordResetParameters())
			.MapParameter(p => p.Email, email)
			.BuildUsing<RequestPasswordResetCommandBuilder>();
		if (commandResult.HasFailed) return Problem(commandResult);

		var result = await mediator.Send(commandResult.Value);
		if (result.HasFailed) return Problem(result);

		return Ok();
	}

	/// <summary>
	/// Delete username
	/// </summary>
	/// <param name="username">username of the user</param>
	/// <response code="200">User has been deleted</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="404">User not found</response>
	[Authorize]
	[HttpDelete("{username}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult> Delete(string username)
	{
		if (username != User.Identity!.Name) return Unauthorized();

		var commandResult = Builder<DeleteUserCommand>
			.BindParameters(new DeleteUserParameters())
			.MapParameter(p => p.Username, username)
			.BuildUsing<DeleteUserCommandBuilder>();
		if (commandResult.HasFailed) return Problem(commandResult);

		var result = await mediator.Send(commandResult.Value);
		if (result.HasFailed) return Problem(result);

		return Ok();
	}

	/// <summary>
	/// Change personal data
	/// </summary>
	/// <param name="username">username of the user</param>
	/// <response code="200">User has been edited</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="404">User not found</response>
	[Authorize]
	[HttpPatch("{username}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult> Edit(string username, EditPersonalDataParameters parameters)
	{
		if (username != User.Identity!.Name) return Unauthorized();

		var commandResult = Builder<EditPersonalDataCommand>
			.BindParameters(parameters)
			.MapParameter(p => p.CurrentUsername, username)
			.BuildUsing<EditPersonalDataCommandBuilder>();
		if (commandResult.HasFailed) return Problem(commandResult);

		var result = await mediator.Send(commandResult.Value);
		if (result.HasFailed) return Problem(result);

		return Ok();
	}

	/// <summary>
	/// Request email verification
	/// </summary>
	/// <param name="username">username of the user</param>
	/// <response code="200">Email verification has been sent</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="404">User not found</response>
	[Authorize]
	[HttpPut("{username}/Email/verify")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult> VerifyEmail(string username)
	{
		if (username != User.Identity!.Name) return Unauthorized();

		var commandResult = Builder<RequestEmailVerificationCommand>
			.BindParameters(new RequestEmailVerificationParameters())
			.MapParameter(p => p.Username, username)
			.BuildUsing<RequestEmailVerificationCommandBuilder>();
		if (commandResult.HasFailed) return Problem(commandResult);

		var result = await mediator.Send(commandResult.Value);
		if (result.HasFailed) return Problem(result);

		return Ok();
	}

	/// <summary>
	/// Verify email
	/// </summary>
	/// <param name="username">username of the user</param>
	/// <param name="token">token received by email</param>
	/// <response code="200">Email has been verified</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="404">User not found</response>
	[HttpGet("{username}/Email/verify/{token}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult> VerifyEmail(string username, string token)
	{
		var commandResult = Builder<VerifyEmailCommand>
			.BindParameters(new VerifyEmailParameters())
			.MapParameter(p => p.Username, username)
			.MapParameter(p => p.Token, token)
			.BuildUsing<VerifyEmailCommandBuilder>();
		if (commandResult.HasFailed) return Problem(commandResult);

		var result = await mediator.Send(commandResult.Value);
		if (result.HasFailed) return Problem(result);

		return Ok();
	}
}
