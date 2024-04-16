using GamingManager.Application.Abstractions;
using GamingManager.Domain.Servers.ValueObjects;

namespace GamingManager.Application.Features.Servers.Commands.ChangeHostname;

public record ChangeHostnameCommand(
	Hostname CurrentHostname,
	Hostname NewHostname) : ICommand;
