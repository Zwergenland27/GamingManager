using GamingManager.Application.Features.GameServerTickets;
using GamingManager.Contracts.Features.GameServerTickets.Queries.Get;
using GamingManager.Contracts.Features.GameServerTickets.Queries.GetAllOpen;
using GamingManager.Domain.GameServerRequests.ValueObjects;
using GamingManager.Domain.GameServerTickets.ValueObjects;
using GamingManager.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GamingManager.Infrastructure.Repositories;

public class GameServerTicketDtoRepository(GamingManagerReadContext context) : IGameServerTicketDtoRepository
{
	public IAsyncEnumerable<GetAllOpenTicketsResult> GetAllOpenAsync()
	{
		return context.GameServerTickets
			.Include(ticket => ticket.Project)
				.ThenInclude(project => project.Game)
			.Include(ticket => ticket.Applicant)
			.Include(ticket => ticket.Issuer)
			.Where(ticket => ticket.Status == TicketStatus.Open)
			.Select(ticket => new GetAllOpenTicketsResult(
				ticket.Id.ToString(),
				ticket.Title,
				ticket.Details,
				new GetAllOpenTicketsGameResult(
					ticket.Project.Game.Id.ToString(),
					ticket.Project.Game.Name),
				new GetAllOpenTicketsApplicantResult(
					ticket.Applicant.Id.ToString(),
					ticket.Applicant.Username)
				))
			.AsAsyncEnumerable();
	}

	public async Task<GetTicketResult?> GetAsync(GameServerTicketId id, UserId? applicantId)
	{
		return await context.GameServerTickets
			.Include(ticket => ticket.Project)
				.ThenInclude(project => project.Game)
			.Include(ticket => ticket.Applicant)
			.Include(ticket => ticket.Issuer)
			.Where(ticket => ticket.Id == id.Value && (applicantId == null || ticket.Applicant.Id == applicantId.Value))
			.Select(ticket => new GetTicketResult(
				ticket.Id.ToString(),
				ticket.Title,
				ticket.Details,
				ticket.Annotation,
				ticket.Status.ToString(),
				new GetTicketGameResult(
					ticket.Project.Game.Id.ToString(),
					ticket.Project.Game.Name),
				new GetTicketApplicantResult(
					ticket.Applicant.Id.ToString(),
					ticket.Applicant.Username,
					ticket.Applicant.Firstname,
					ticket.Applicant.Lastname,
					ticket.Applicant.Email),
				ReferenceEquals(ticket.Issuer, null) ? null : new GetTicketIssuerResult(
					ticket.Issuer.Id.ToString(),
					ticket.Issuer.Username,
					ticket.Issuer.Firstname,
					ticket.Issuer.Lastname,
					ticket.Issuer.Email)))
			.FirstOrDefaultAsync();
	}
}
