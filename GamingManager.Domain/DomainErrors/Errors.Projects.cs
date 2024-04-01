using CleanDomainValidation.Domain;
using GamingManager.Domain.Accounts.ValueObjects;
using GamingManager.Domain.Projects;
using GamingManager.Domain.Projects.ValueObjects;
using System.Diagnostics;

namespace GamingManager.Domain.DomainErrors;

/// <summary>
/// List of all errors
/// </summary>
public static partial class Errors
{
	/// <summary>
	/// Errors that occur when working with <see cref="Project"/> aggregate
	/// </summary>
	public static class Projects
	{
		/// <summary>
		/// A project with this id does not exist
		/// </summary>
		public static Error IdNotFound => Error.NotFound(
			"Project.IdNotFound",
			"A project with this id does not exist");


		/// <summary>
		/// The provided start time is in the past
		/// </summary>
		public static Error StartInPast => Error.Conflict(
			"Project.StartInPast",
			"The provided start time is in the past");

		/// <summary>
		/// The provided start time is in the past
		/// </summary>
		public static Error EndInPast => Error.Conflict(
			"Project.EndInPast",
			"The provided end time is in the past");

		/// <summary>
		/// The provided end time is before the start time
		/// </summary>
		public static Error EndBeforeStart => Error.Conflict(
			"Project.EndBeforeStart",
			"The provided end time is before the start time");

		public static Error Ended => Error.Conflict(
			"Project.Ended",
			"The project has ended");

		/// <summary>
		/// Errors that occur when working with <see cref="ProjectId"/>
		/// </summary>
		public static class Id
		{
			/// <summary>
			/// The provided id is not a valid guid
			/// </summary>
			public static Error Invalid => Error.Validation(
				"Project.Id.Invalid",
				"The provided id is not a valid guid");
		}

		public static class Team
		{
			/// <summary>
			/// The user is already a member in the management team
			/// </summary>
			public static Error AlreadyMember => Error.Conflict(
				"Project.Team.AlreadyMember",
				"The user is already a member in the management team");

			/// <summary>
			/// The user is no member of the management team
			/// </summary>
			public static Error NoMember => Error.NotFound(
				"Project.Team.NoMember",
				"The user is no member of the management team");

			/// <summary>
			/// The user cannot be removed from the organisators because no other administrator is selected
			/// </summary>
			public static Error NoAdmin => Error.Conflict(
				"Project.Team.NoAdmin",
				"The user cannot be removed from the management team because no other administrator is selected");

			/// <summary>
			/// Errors that occur when working with <see cref="TeamMemberId"/>
			/// </summary>
			public static class Id
			{
				/// <summary>
				/// The provided id is not a valid guid
				/// </summary>
				public static Error Invalid => Error.Validation(
					"Project.TeamMember.Id.Invalid",
					"The provided id is not a valid guid");
			}
		}

		public static class Participants
		{

			/// <summary>
			/// The participant is already banned
			/// </summary>
			public static Error AlreadyBanned => Error.Conflict(
				"Project.Participant.AlreadyBanned",
				"The participant is already banned");

			/// <summary>
			/// The participant is not banned
			/// </summary>
			public static Error NotBanned => Error.Conflict(
				"Project.Participant.NotBanned",
				"The participant is not banned");

			/// <summary>
			/// The provided account is not participating on this project
			/// </summary>
			public static Error NotParticipating => Error.NotFound(
				"Project.Participant.NotParticipating",
				"The account is not participating on this project");

			/// <summary>
			/// The account is already participating on this project
			/// </summary>
			public static Error AlreadyParticipating => Error.Conflict(
				"Project.Participant.AlreadyParticipating",
				"The account is already participating on this project");

			/// <summary>
			/// The accounts game differs from the project game
			/// </summary>
			public static Error WrongGame => Error.Conflict(
				"Project.Participant.WrongGame",
				"The accounts game differs from the project game");

			/// <summary>
			/// The participant is banned
			/// </summary>
			public static Error Banned => Error.Conflict(
				"Project.Participant.Banned",
				"The participant is banned");

			/// <summary>
			/// The player cannot join because he has an open (not ended) session
			/// </summary>
			public static Error OpenSession => Error.Conflict(
				"Project.Participant.OpenSession",
				"The player cannot join because he has an open (not ended) session");

			/// <summary>
			/// The player cannot leave because he has no open (not ended) session
			/// </summary>
			public static Error NoOpenSession => Error.Conflict(
				"Project.Participant.NoOpenSession",
				"The player cannot leave because he has no open (not ended) session");

			/// <summary>
			/// The player cannot join before the previous session
			/// </summary>
			public static Error JoinBeforePreviousSession => Error.Conflict(
				"Project.Participant.JoinBeforePreviousSession",
				"The player cannot join before the previous session");

			public static class Bans
			{

				public static class Reason
				{
					/// <summary>
					/// The reason for the ban cannot be empty
					/// </summary>
					public static Error Empty => Error.Validation(
						"Project.Participant.Ban.Reason.Empty",
						"The reason for the ban cannot be empty");
				}

				public static class Id
				{
					/// <summary>
					/// The provided id is not a valid guid
					/// </summary>
					public static Error Invalid => Error.Validation(
						"Project.Participant.Ban.Id.Invalid",
						"The provided id is not a valid guid");
				}
			}

			public static class Sessions
			{
				/// <summary>
				/// The session cannot end before it started
				/// </summary>
				public static Error EndBeforeStart => Error.Conflict(
					"Project.Participant.Session.EndBeforeStart",
					"The session cannot end before it started");

				public static class Id
				{
					/// <summary>
					/// The provided id is not a valid guid
					/// </summary>
					public static Error Invalid => Error.Validation(
						"Project.Participant.Session.Id.Invalid",
						$"The provided id is not a valid guid");
				}
			}

			/// <summary>
			/// Errors that occur when working with <see cref="ParticipantId"/>
			/// </summary>
			public static class Id
			{
				/// <summary>
				/// The provided id is not a valid guid
				/// </summary>
				public static Error Invalid => Error.Validation(
					"Project.Participant.Id.Invalid",
					$"The provided id is not a valid guid");
			}
		}
	}
}
