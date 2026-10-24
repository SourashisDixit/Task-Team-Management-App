using System.ComponentModel.DataAnnotations;

namespace TaskTeamManagementApp.DTOs;

public sealed class CreateProjectRequest
{
    [Required]
    [StringLength(120, MinimumLength = 2)]
    public string Name { get; init; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; init; }
}
