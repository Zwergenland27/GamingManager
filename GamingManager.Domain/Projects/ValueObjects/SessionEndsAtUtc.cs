namespace GamingManager.Domain.Projects.ValueObjects;

public readonly record struct SessionEndsAtUtc(DateTime Value, bool Irregular = false);
