using Microsoft.EntityFrameworkCore;
using TaskTeamManagementApp.Data;
using TaskTeamManagementApp.DTOs;
using TaskTeamManagementApp.Models;
using TaskTeamManagementApp.Services.Interfaces;

namespace TaskTeamManagementApp.Services;

public sealed class ProjectService(AppDbContext dbContext) : IProjectService
{
    public async Task<IReadOnlyList<ProjectResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Projects
            .AsNoTracking()
            .OrderBy(project => project.Id)
            .Select(project => new ProjectResponse(
                project.Id,
                project.Name,
                project.Description,
                project.CreatedAtUtc))
            .ToListAsync(cancellationToken);
    }

    public async Task<ProjectResponse?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var project = await dbContext.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(project => project.Id == id, cancellationToken);

        return project is null ? null : ToResponse(project);
    }

    public async Task<ProjectResponse> CreateAsync(
        CreateProjectRequest request,
        CancellationToken cancellationToken)
    {
        var project = new Project
        {
            Name = request.Name.Trim(),
            Description = request.Description?.Trim(),
            CreatedAtUtc = DateTime.UtcNow
        };

        dbContext.Projects.Add(project);
        await dbContext.SaveChangesAsync(cancellationToken);

        return ToResponse(project);
    }

    public async Task<bool> UpdateAsync(
        int id,
        UpdateProjectRequest request,
        CancellationToken cancellationToken)
    {
        var project = await dbContext.Projects.FindAsync([id], cancellationToken);
        if (project is null)
        {
            return false;
        }

        project.Name = request.Name.Trim();
        project.Description = request.Description?.Trim();
        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var project = await dbContext.Projects.FindAsync([id], cancellationToken);
        if (project is null)
        {
            return false;
        }

        dbContext.Projects.Remove(project);
        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    private static ProjectResponse ToResponse(Project project) =>
        new(project.Id, project.Name, project.Description, project.CreatedAtUtc);
}
