﻿// <auto-generated />
using System;
using GamingManager.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GamingManager.Infrastructure.Migrations
{
    [DbContext(typeof(GamingManagerContext))]
    [Migration("20240408162807_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GamingManager.Domain.Accounts.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Game")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("User")
                        .HasColumnType("uuid");

                    b.Property<string>("Uuid")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Game", "Name");

                    b.HasIndex("Game", "Uuid");

                    b.ToTable("Accounts", (string)null);
                });

            modelBuilder.Entity("GamingManager.Domain.GameServers.GameServer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("HostedOn")
                        .HasColumnType("uuid");

                    b.Property<bool>("Maintenance")
                        .HasColumnType("boolean");

                    b.Property<Guid>("Project")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ShutdownAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<uint>("ShutdownDelay")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<bool>("Unstartable")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("GameServers", (string)null);
                });

            modelBuilder.Entity("GamingManager.Domain.Games.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Games", (string)null);
                });

            modelBuilder.Entity("GamingManager.Domain.Projects.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("End")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Ended")
                        .HasColumnType("boolean");

                    b.Property<Guid>("Game")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("Server")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Start")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Projects", (string)null);
                });

            modelBuilder.Entity("GamingManager.Domain.Servers.Server", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastHeartbeatAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Mac")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Maintenance")
                        .HasColumnType("boolean");

                    b.Property<bool>("PossiblyUnstartable")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ShutdownAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Servers", (string)null);
                });

            modelBuilder.Entity("GamingManager.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Firstname")
                        .HasColumnType("text");

                    b.Property<string>("Lastname")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("GamingManager.Domain.Projects.Project", b =>
                {
                    b.OwnsMany("GamingManager.Domain.Projects.Entities.Participant", "Participants", b1 =>
                        {
                            b1.Property<Guid>("Project")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Account")
                                .HasColumnType("uuid");

                            b1.Property<bool>("Online")
                                .HasColumnType("boolean");

                            b1.Property<TimeSpan>("PlayTime")
                                .HasColumnType("interval");

                            b1.Property<DateTime>("Since")
                                .HasColumnType("timestamp with time zone");

                            b1.HasKey("Project", "Id");

                            b1.ToTable("Participants", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("Project");

                            b1.OwnsMany("GamingManager.Domain.Projects.Entities.Ban", "Bans", b2 =>
                                {
                                    b2.Property<Guid>("Project")
                                        .HasColumnType("uuid");

                                    b2.Property<Guid>("Participant")
                                        .HasColumnType("uuid");

                                    b2.Property<Guid>("Id")
                                        .HasColumnType("uuid");

                                    b2.Property<DateTime>("BannedAtUtc")
                                        .HasColumnType("timestamp with time zone");

                                    b2.Property<TimeSpan?>("Duration")
                                        .HasColumnType("interval");

                                    b2.Property<string>("Reason")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.HasKey("Project", "Participant", "Id");

                                    b2.ToTable("Bans", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("Project", "Participant");
                                });

                            b1.OwnsMany("GamingManager.Domain.Projects.Entities.Session", "Sessions", b2 =>
                                {
                                    b2.Property<Guid>("Project")
                                        .HasColumnType("uuid");

                                    b2.Property<Guid>("Participant")
                                        .HasColumnType("uuid");

                                    b2.Property<Guid>("Id")
                                        .HasColumnType("uuid");

                                    b2.Property<DateTime>("Start")
                                        .HasColumnType("timestamp with time zone");

                                    b2.HasKey("Project", "Participant", "Id");

                                    b2.ToTable("Sessions", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("Project", "Participant");

                                    b2.OwnsOne("GamingManager.Domain.Projects.ValueObjects.SessionEndsAtUtc", "End", b3 =>
                                        {
                                            b3.Property<Guid>("SessionProject")
                                                .HasColumnType("uuid");

                                            b3.Property<Guid>("SessionParticipant")
                                                .HasColumnType("uuid");

                                            b3.Property<Guid>("SessionId")
                                                .HasColumnType("uuid");

                                            b3.Property<DateTime>("EndTime")
                                                .HasColumnType("timestamp with time zone");

                                            b3.Property<bool>("Irregular")
                                                .HasColumnType("boolean");

                                            b3.HasKey("SessionProject", "SessionParticipant", "SessionId");

                                            b3.ToTable("Sessions");

                                            b3.WithOwner()
                                                .HasForeignKey("SessionProject", "SessionParticipant", "SessionId");
                                        });

                                    b2.Navigation("End");
                                });

                            b1.Navigation("Bans");

                            b1.Navigation("Sessions");
                        });

                    b.OwnsMany("GamingManager.Domain.Projects.Entities.TeamMember", "Team", b1 =>
                        {
                            b1.Property<Guid>("Project")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<int>("Role")
                                .HasColumnType("integer");

                            b1.Property<DateTime>("Since")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<Guid>("User")
                                .HasColumnType("uuid");

                            b1.HasKey("Project", "Id");

                            b1.ToTable("TeamMembers", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("Project");
                        });

                    b.Navigation("Participants");

                    b.Navigation("Team");
                });
#pragma warning restore 612, 618
        }
    }
}