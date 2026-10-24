namespace TaskTeamManagementApp.DTOs;

public sealed record ProjectResponse(
    int Id,
    string Name,
    string? Description,
    DateTime CreatedAtUtc);
