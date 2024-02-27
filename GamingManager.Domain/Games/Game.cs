﻿using CleanDomainValidation.Domain;
using GamingManager.Domain.Abstractions;
using GamingManager.Domain.Games.ValueObjects;
using GamingManager.Domain.Projects;
using GamingManager.Domain.Projects.ValueObjects;
using GamingManager.Domain.Users;

namespace GamingManager.Domain.Games;

/// <summary>
/// A game of which a server can be created
/// </summary>
public class Game : AggregateRoot<GameId>
{
	private Game(
		GameName name) : base(GameId.CreateNew())
	{
		Name = name;
	}

#pragma warning disable CS8618
	private Game() : base(default!) { }
#pragma warning restore CS8618

	public GameName Name { get; set; }

	/// <summary>
	/// Creates new <see cref="Game"/> instance
	/// </summary>
	public static CanFail<Game> Create(GameName name)
	{
		return new Game(name);
	}

	public CanFail<Project> CreateProject(ProjectName name, ProjectStartsAtUtc start, User owner)
	{
		return Project.Create(name, this, start, owner);
	}

}
