using CleanDomainValidation.Application;
using GamingManager.Application.Features.Projects.Commands.AddMember;
using GamingManager.Application.Features.Projects.Commands.Allow;
using GamingManager.Application.Features.Projects.Commands.Ban;
using GamingManager.Application.Features.Projects.Commands.ChangeMemberRole;
using GamingManager.Application.Features.Projects.Commands.Create;
using GamingManager.Application.Features.Projects.Commands.CreateTicket;
using GamingManager.Application.Features.Projects.Commands.Finish;
using GamingManager.Application.Features.Projects.Commands.Join;
using GamingManager.Application.Features.Projects.Commands.Leave;
using GamingManager.Application.Features.Projects.Commands.Pardon;
using GamingManager.Application.Features.Projects.Commands.RemoveMember;
using GamingManager.Application.Features.Projects.Commands.RescheduleStart;
using GamingManager.Application.Features.Projects.Commands.StartServer;
using GamingManager.Application.Features.Projects.Queries.Get;
using GamingManager.Application.Features.Projects.Queries.GetAll;
using GamingManager.Contracts.Features.Projects;
using GamingManager.Contracts.Features.Projects.Commands.AddToTeam;
using GamingManager.Contracts.Features.Projects.Commands.AllowAccount;
using GamingManager.Contracts.Features.Projects.Commands.Ban;
using GamingManager.Contracts.Features.Projects.Commands.ChangeMemberRole;
using GamingManager.Contracts.Features.Projects.Commands.Create;
using GamingManager.Contracts.Features.Projects.Commands.CreateTicket;
using GamingManager.Contracts.Features.Projects.Commands.Finish;
using GamingManager.Contracts.Features.Projects.Commands.Join;
using GamingManager.Contracts.Features.Projects.Commands.Leave;
using GamingManager.Contracts.Features.Projects.Commands.Pardon;
using GamingManager.Contracts.Features.Projects.Commands.RemoveMember;
using GamingManager.Contracts.Features.Projects.Commands.RescheduleStart;
using GamingManager.Contracts.Features.Projects.Commands.StartServer;
using GamingManager.Contracts.Features.Projects.Queries.Get;
using GamingManager.Contracts.Features.Projects.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography.Xml;

namespace GamingManager.WebApi.Controllers;

[Route("api/Projects")]
[Produces("application/json")]
public class ProjectsController(IMediator mediator) : ApiController
{
	/// <summary>
	/// Get a project by its id
	/// </summary>
	/// <param name="id" example="00000000-0000-0000-0000-000000000000">Id of the project</param>
	/// <response code="200">Returns project information</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only members can access the project page</response>
	[Authorize]
	[HttpGet("{id}")]
	[ProducesResponseType(typeof(GetProjectResult), 200)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<ActionResult<GetProjectResult>> Get(string id)
	{
		var queryResult = Builder<GetProjectQuery>
			.BindParameters(new GetProjectParameters())
			.MapParameter(p => p.ProjectId, id)
			.MapParameter(p => p.AuditorId, User.FindFirstValue(ClaimTypes.NameIdentifier))
			.BuildUsing<GetProjectQueryBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var result = await mediator.Send(queryResult.Value);
		if (result.HasFailed) return Problem(result);

		return Ok(result.Value);
	}

	/// <summary>
	/// Get all projects
	/// </summary>
	/// <response code="200">Returns projects information</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	[Authorize]
	[HttpGet]
	[ProducesResponseType(typeof(IEnumerable<GetAllProjectsResult>), 200)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<IEnumerable<GetAllProjectsResult>>> GetAll()
	{
		var queryResult = Builder<GetAllProjectsQuery>
			.BindParameters(new GetAllProjectsParameters())
			.MapParameter(p => p.AuditorId, User.FindFirstValue(ClaimTypes.NameIdentifier))
			.BuildUsing<GetAllProjectsQueryBuilder>();
		if (queryResult.HasFailed) return Problem(queryResult);

		var result = await mediator.Send(queryResult.Value);
		if (result.HasFailed) return Problem(result);

		return Ok(result.Value);
	}

	/// <summary>
	/// Add a member to a project
	/// </summary>
	/// <param name="id" example="00000000-0000-0000-0000-000000000000">Id of the project</param>
	/// <response code="200">Member added</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only project admins or moderators can add members</response>
	[Authorize]
	[HttpPut("{id}/Members")]
	[ProducesResponseType(typeof(AddMemberResult), 200)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<ActionResult<AddMemberResult>> AddMember(string id)
	{
		var commandResult = Builder<AddMemberCommand>
			.BindParameters(new AddMemberParameters())
			.MapParameter(p => p.ProjectId, id)
			.MapParameter(p => p.AuditorId, User.FindFirstValue(ClaimTypes.NameIdentifier))
			.BuildUsing<AddMemberCommandBuilder>();
		if (commandResult.HasFailed) return Problem(commandResult);

		var result = await mediator.Send(commandResult.Value);
		if (result.HasFailed) return Problem(result);

		return Ok(result.Value);
	}

	/// <summary>
	/// Allow an account to join a project server
	/// </summary>
	/// <param name="id" example="00000000-0000-0000-0000-000000000000">Id of the project</param>
	/// <response code="200">Account added</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only project admins or moderators can add a participant</response>
	[Authorize]
	[HttpPut("{id}/Participants")]
	[ProducesResponseType(typeof(AllowAccountResult), 200)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<ActionResult<AllowAccountResult>> AllowAccount(string id, AllowAccountParameters parameters)
	{
		var commandResult = Builder<AllowAccountOnProjectCommand>
			.BindParameters(parameters)
			.MapParameter(p => p.ProjectId, id)
			.MapParameter(p => p.AuditorId, User.FindFirstValue(ClaimTypes.NameIdentifier))
			.BuildUsing<AllowAccountOnProjectCommandBuilder>();
		if (commandResult.HasFailed) return Problem(commandResult);

		var result = await mediator.Send(commandResult.Value);
		if (result.HasFailed) return Problem(result);

		return Ok(result.Value);
	}

	/// <summary>
	/// Ban a participant from a project
	/// </summary>
	/// <param name="id" example="00000000-0000-0000-0000-000000000000">Id of the project</param>
	/// <param name="participantId" example="00000000-0000-0000-0000-000000000000">Id of the participant</param>
	/// <response code="200">Ban created</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only project admins or moderators can ban participants</response>
	/// <response code="404">Participant not found</response>
	[Authorize]
	[HttpPut("{id}/Participants/{participantId}/ban")]
	[ProducesResponseType(typeof(BanResult), 200)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<BanResult>> BanAccount(string id, string participantId, BanParticipantParameters parameters)
	{
		var commandResult = Builder<BanParticipantCommand>
			.BindParameters(parameters)
			.MapParameter(p => p.ProjectId, id)
			.MapParameter(p => p.ParticipantId, participantId)
			.MapParameter(p => p.AuditorId, User.FindFirstValue(ClaimTypes.NameIdentifier))
			.BuildUsing<BanParticipantCommandBuilder>();
		if (commandResult.HasFailed) return Problem(commandResult);

		var result = await mediator.Send(commandResult.Value);
		if (result.HasFailed) return Problem(result);

		return Ok(result.Value);
	}

	/// <summary>
	/// Change the role of a member
	/// </summary>
	/// <param name="id" example="00000000-0000-0000-0000-000000000000">Id of the project</param>
	/// <param name="memberId" example="00000000-0000-0000-0000-000000000000">Id of the participant</param>
	/// <response code="204">Role changed</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only project admins or moderators can change the member role</response>
	/// <response code="404">Member not found</response>
	[Authorize]
	[HttpPatch("{id}/Members/{memberId}/Role")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult> ChangeMemberRole(string id, string memberId, ChangeMemberRoleParameters parameters)
	{
		var commandResult = Builder<ChangeMemberRoleCommand>
			.BindParameters(parameters)
			.MapParameter(p => p.ProjectId, id)
			.MapParameter(p => p.MemberId, memberId)
			.MapParameter(p => p.AuditorId, User.FindFirstValue(ClaimTypes.NameIdentifier))
			.BuildUsing<ChangeMemberRoleCommandBuilder>();
		if (commandResult.HasFailed) return Problem(commandResult);

		var result = await mediator.Send(commandResult.Value);
		if (result.HasFailed) return Problem(result);

		return NoContent();
	}

	/// <summary>
	/// Create a projct
	/// </summary>
	/// <response code="201">Project created</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	[Authorize]
	[HttpPost]
	[ProducesResponseType(typeof(CreateProjectResult), 201)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<CreateProjectResult>> CreateProject(CreateProjectParameters parameters)
	{
		var commandResult = Builder<CreateProjectCommand>
			.BindParameters(parameters)
			.MapParameter(p => p.AuditorId, User.FindFirstValue(ClaimTypes.NameIdentifier))
			.BuildUsing<CreateProjectCommandBuilder>();
		if (commandResult.HasFailed) return Problem(commandResult);

		var result = await mediator.Send(commandResult.Value);
		if (result.HasFailed) return Problem(result);

		return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value);
	}

	/// <summary>
	/// Create a ticket to get a server
	/// </summary>
	/// <param name="id" example="00000000-0000-0000-0000-000000000000">Id of the project</param>
	/// <param name="parameters"></param>
	/// <response code="204">Ticket created</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only project admins or moderators can create a ticket</response>
	[Authorize]
	[HttpPut("{id}/Tickets")]
	[ProducesResponseType(typeof(CreateTicketResult), 200)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<ActionResult<CreateTicketResult>> CreateTicket(string id, CreateTicketParameters parameters)
	{
		var commandResult = Builder<CreateTicketCommand>
			.BindParameters(parameters)
			.MapParameter(p => p.ProjectId, id)
			.MapParameter(p => p.AuditorId, User.FindFirstValue(ClaimTypes.NameIdentifier))
			.BuildUsing<CreateTicketCommandBuilder>();
		if (commandResult.HasFailed) return Problem(commandResult);

		var result = await mediator.Send(commandResult.Value);
		if (result.HasFailed) return Problem(result);

		return Ok(result.Value);
	}

	/// <summary>
	/// Finish a project
	/// </summary>
	/// <param name="id" example="00000000-0000-0000-0000-000000000000">Id of the project</param>
	/// <response code="204">Project finished</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only project admins or moderators can create a ticket</response>
	[Authorize]
	[HttpPatch("{id}/finish")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<ActionResult> FinishProject(string id)
	{
		var commandResult = Builder<FinishProjectCommand>
			.BindParameters(new FinishProjectParameters())
			.MapParameter(p => p.ProjectId, id)
			.MapParameter(p => p.AuditorId, User.FindFirstValue(ClaimTypes.NameIdentifier))
			.BuildUsing<FinishProjectCommandBuilder>();
		if (commandResult.HasFailed) return Problem(commandResult);

		var result = await mediator.Send(commandResult.Value);
		if (result.HasFailed) return Problem(result);

		return NoContent();
	}

	/// <summary>
	/// Player joined a project
	/// </summary>
	/// <param name="id" example="00000000-0000-0000-0000-000000000000">Id of the project</param>
	/// <response code="204">Player joined</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only the gameServer can do that</response>
	[Authorize(Roles = "GameServer")]
	[HttpPut("{id}/join")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<ActionResult> JoinProject(string id, JoinParameters parameters)
	{
		var commandResult = Builder<JoinCommand>
			.BindParameters(parameters)
			.MapParameter(p => p.GameServerId, User.FindFirstValue(ClaimTypes.NameIdentifier))
			.MapParameter(p => p.ProjectId, id)
			.BuildUsing<JoinCommandBuilder>();
		if (commandResult.HasFailed) return Problem(commandResult);

		var result = await mediator.Send(commandResult.Value);
		if (result.HasFailed) return Problem(result);

		return NoContent();
	}

	/// <summary>
	/// Player left a project
	/// </summary>
	/// <param name="id" example="00000000-0000-0000-0000-000000000000">Id of the project</param>
	/// <response code="204">Player joined</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only the gameServer can do that</response>
	[Authorize(Roles = "GameServer")]
	[HttpPatch("{id}/leave")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<ActionResult> LeaveProject(string id, LeaveParameters parameters)
	{
		var commandResult = Builder<LeaveCommand>
			.BindParameters(parameters)
			.MapParameter(p => p.GameServerId, User.FindFirstValue(ClaimTypes.NameIdentifier))
			.MapParameter(p => p.ProjectId, id)
			.BuildUsing<LeaveCommandBuilder>();
		if (commandResult.HasFailed) return Problem(commandResult);

		var result = await mediator.Send(commandResult.Value);
		if (result.HasFailed) return Problem(result);

		return NoContent();
	}

	/// <summary>
	/// Pardon an participant from a project
	/// </summary>
	/// <param name="id" example="00000000-0000-0000-0000-000000000000">Id of the project</param>
	/// <param name="participantId" example="00000000-0000-0000-0000-000000000000">Id of the participant</param>
	/// <response code="204">Success</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only project admins or moderators can ban participants</response>
	/// <response code="404">Participant not found</response>
	[Authorize]
	[HttpPatch("{id}/Participants/{participantId}/pardon")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult> PardonParticipant(string id, string participantId, PardonParticipantParameters parameters)
	{
		var commandResult = Builder<PardonParticipantCommand>
			.BindParameters(parameters)
			.MapParameter(p => p.ProjectId, id)
			.MapParameter(p => p.ParticipantId, participantId)
			.MapParameter(p => p.AuditorId, User.FindFirstValue(ClaimTypes.NameIdentifier))
			.BuildUsing<PardonParticipantCommandBuilder>();
		if (commandResult.HasFailed) return Problem(commandResult);

		var result = await mediator.Send(commandResult.Value);
		if (result.HasFailed) return Problem(result);

		return NoContent();
	}

	/// <summary>
	/// Remove a member from the project
	/// </summary>
	/// <param name="id" example="00000000-0000-0000-0000-000000000000">Id of the project</param>
	/// <param name="memberId" example="00000000-0000-0000-0000-000000000000">Id of the member</param>
	/// <response code="204">Success</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only project admins or moderators can ban participants</response>
	/// <response code="404">Member not found</response>
	[Authorize]
	[HttpDelete("{id}/Members/{memberId}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult> RemoveMember(string id, string memberId, RemoveMemberParameters parameters)
	{
		var commandResult = Builder<RemoveMemberCommand>
			.BindParameters(parameters)
			.MapParameter(p => p.ProjectId, id)
			.MapParameter(p => p.MemberId, memberId)
			.MapParameter(p => p.AuditorId, User.FindFirstValue(ClaimTypes.NameIdentifier))
			.BuildUsing<RemoveMemberCommandBuilder>();
		if (commandResult.HasFailed) return Problem(commandResult);

		var result = await mediator.Send(commandResult.Value);
		if (result.HasFailed) return Problem(result);

		return NoContent();
	}

	/// <summary>
	/// Reschedule the start of the project
	/// </summary>
	/// <param name="id" example="00000000-0000-0000-0000-000000000000">Id of the project</param>
	/// <response code="204">Project rescheduled</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only project admins or moderators can ban participants</response>
	[Authorize]
	[HttpPatch("{id}/reschedule")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<ActionResult> RescheduleStartProject(string id, RescheduleProjectStartParameters parameters)
	{
		var commandResult = Builder<RescheduleProjectStartCommand>
			.BindParameters(parameters)
			.MapParameter(p => p.ProjectId, id)
			.MapParameter(p => p.AuditorId, User.FindFirstValue(ClaimTypes.NameIdentifier))
			.BuildUsing<RescheduleProjectStartCommandBuilder>();
		if (commandResult.HasFailed) return Problem(commandResult);

		var result = await mediator.Send(commandResult.Value);
		if (result.HasFailed) return Problem(result);

		return NoContent();
	}

	/// <summary>
	/// Start the project server
	/// </summary>
	/// <param name="id" example="00000000-0000-0000-0000-000000000000">Id of the project</param>
	/// <response code="204">Project rescheduled</response>
	/// <response code="400">The request is invalid</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="403">Only project admins or moderators can ban participants</response>
	[Authorize]
	[HttpPatch("{id}/start")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<ActionResult> StartProject(string id)
	{
		var commandResult = Builder<StartProjectServerCommand>
			.BindParameters(new StartProjectServerParameters())
			.MapParameter(p => p.ProjectId, id)
			.MapParameter(p => p.AuditorId, User.FindFirstValue(ClaimTypes.NameIdentifier))
			.BuildUsing<StartProjectServerCommandBuilder>();
		if (commandResult.HasFailed) return Problem(commandResult);

		var result = await mediator.Send(commandResult.Value);
		if (result.HasFailed) return Problem(result);

		return NoContent();
	}
}
