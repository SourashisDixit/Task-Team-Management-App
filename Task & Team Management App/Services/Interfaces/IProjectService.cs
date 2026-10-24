using TaskTeamManagementApp.DTOs;

namespace TaskTeamManagementApp.Services.Interfaces;

public interface IProjectService
{
    Task<IReadOnlyList<ProjectResponse>> GetAllAsync(CancellationToken cancellationToken);
    Task<ProjectResponse?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<ProjectResponse> CreateAsync(CreateProjectRequest request, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(int id, UpdateProjectRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}
