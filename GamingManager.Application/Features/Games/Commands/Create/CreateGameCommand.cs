using GamingManager.Application.Abstractions;
using GamingManager.Domain.Games;
using GamingManager.Domain.Games.ValueObjects;

namespace GamingManager.Application.Features.Games.Commands.CreateGame;

public record CreateGameCommand(GameName Name) : ICommand<Game>;
