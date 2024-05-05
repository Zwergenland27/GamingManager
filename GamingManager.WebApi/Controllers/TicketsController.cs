using CleanDomainValidation.Application;
using GamingManager.Application.Features.GameServerTickets.Commands.Accept;
using GamingManager.Application.Features.GameServerTickets.Commands.Reject;
using GamingManager.Application.Features.GameServerTickets.Queries.Get;
using GamingManager.Application.Features.GameServerTickets.Queries.GetAllOpen;
using GamingManager.Contracts.Features.GameServerTickets.Commands.Accept;
using GamingManager.Contracts.Features.GameServerTickets.Commands.Reject;
using GamingManager.Contracts.Features.GameServerTickets.Queries.Get;
using GamingManager.Contracts.Features.GameServerTickets.Queries.GetAllOpen;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GamingManager.WebApi.Controllers;

[Route("api/Tickets")]
[Produces("application/json")]
public class TicketsController(IMediator mediator) : ApiController
{
	/// <summary>
	/// Get a ticket by its id
	/// </summary>
	/// <param name="id" example="00000000-0000-0000-0000-000000000000">Id of the ticket</param>
	/// <response code="200">Returns ticket</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only admins and the accountant can access the ticket</response>
	/// <response code="404">Ticket not found</response>
	[Authorize]
	[HttpGet("{id}")]
	[ProducesResponseType(typeof(GetTicketResult), 200)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<GetTicketResult>> Get(string id)
	{
		var queryResult = Builder<GetTicketQuery>
			.BindParameters(new GetTicketParameters())
			.MapParameter(p => p.Id, id)
			.MapParameter(p => p.AuditorId, User.IsInRole("Admin") ? null : User.FindFirstValue(ClaimTypes.NameIdentifier))
			.BuildUsing<GetTicketQueryBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);																					
		var result = await mediator.Send(queryResult.Value);
		if (result.HasFailed) return Problem(result);

		return Ok(result.Value);
	}

	/// <summary>
	/// Get all open tickets
	/// </summary>
	/// <response code="200">Returns List of open tickets</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Not authorized</response>
	/// <response code="403">Only admins and the accountant can access the ticket</response>
	[Authorize(Roles = "Admin")]
	[HttpGet]
	[ProducesResponseType(typeof(IEnumerable<GetAllOpenTicketsResult>), 200)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<ActionResult<IEnumerable<GetAllOpenTicketsResult>>> GetAllOpenTickets()
	{
		var result = await mediator.Send(new GetAllOpenTickersQuery());
		if (result.HasFailed) return Problem(result);

		return Ok(result.Value);
	}

	/// <summary>
	/// Reject a ticket
	/// </summary>
	/// <param name="id" example="00000000-0000-0000-0000-000000000000">Id of the ticket</param>
	/// <response code="200">Returns List of open tickets</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Not authorized</response>
	/// <response code="403">Only admins and the accountant can access the ticket</response>
	/// <response code="404">Ticket not found</response>
	/// <response code="409">Ticket is already closed</response>
	[Authorize(Roles = "Admin")]
	[HttpPost("{id}/reject")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<ActionResult> Reject(string id)
	{
		var queryResult = Builder<RejectGameServerTicketCommand>
			.BindParameters(new RejectTicketParameters())
			.MapParameter(p => p.Id, id)
			.MapParameter(p => p.AuditorId, User.FindFirstValue(ClaimTypes.NameIdentifier))
			.BuildUsing<RejectGameServerTicketCommandBuilder>();

		if (queryResult.HasFailed) return Problem(queryResult);
		var commandResult = await mediator.Send(queryResult.Value);
		if (commandResult.HasFailed) return Problem(commandResult);

		return NoContent();
	}

	/// <summary>
	/// Accept a ticket
	/// </summary>
	/// <param name="id" example="00000000-0000-0000-0000-000000000000">Id of the ticket</param>
	/// <response code="200">Returns List of open tickets</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Not authorized</response>
	/// <response code="403">Only admins and the accountant can access the ticket</response>
	/// <response code="404">Ticket not found</response>
	/// <response code="409">Ticket is already closed</response>
	[Authorize(Roles = "Admin")]
	[HttpPost("{id}/accept")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	public async Task<ActionResult> Accept(string id)
	{
		var queryResult = Builder<AcceptGameServerTicketCommand>
			.BindParameters(new AcceptTicketParameters())
			.MapParameter(p => p.Id, id)
			.MapParameter(p => p.AuditorId, User.FindFirstValue(ClaimTypes.NameIdentifier))
			.BuildUsing<AcceptGameServerTicketCommandBuilder>();

		if (queryResult.HasFailed) return Problem(queryResult);
		var commandResult = await mediator.Send(queryResult.Value);
		if (commandResult.HasFailed) return Problem(commandResult);

		return NoContent();
	}
}
