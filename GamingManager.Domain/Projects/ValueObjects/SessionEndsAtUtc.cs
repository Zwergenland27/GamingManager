namespace GamingManager.Domain.Projects.ValueObjects;

public sealed record SessionEndsAtUtc(DateTime EndTime, bool Irregular = false);
