using Microsoft.EntityFrameworkCore;
using ProjectTaskManagement.Application.DTOs.Projects;
using ProjectTaskManagement.Application.Interfaces;
using ProjectTaskManagement.Domain.Entities;

namespace ProjectTaskManagement.Application.Services;

public class ProjectService(IApplicationDbContext context) : IProjectService
{
    public async Task<ProjectResponse> CreateAsync(CreateProjectRequest request, string userId)
    {
        var project = new Project { Name = request.Name.Trim(), Description = request.Description, UserId = userId };
        context.Projects.Add(project);
        await context.SaveChangesAsync();
        return ToResponse(project);
    }

    public async Task<IReadOnlyList<ProjectResponse>> GetAllAsync(string userId)
    {
        return await context.Projects
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new ProjectResponse(p.Id, p.Name, p.Description, p.CreatedAt))
            .ToListAsync();
    }

    public async Task<ProjectResponse> GetByIdAsync(Guid id, string userId)
    {
        var project = await context.Projects.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId)
            ?? throw new KeyNotFoundException("Project not found.");
        return ToResponse(project);
    }

    public async Task<ProjectResponse> UpdateAsync(Guid id, UpdateProjectRequest request, string userId)
    {
        var project = await context.Projects.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId)
            ?? throw new KeyNotFoundException("Project not found.");

        project.Name = request.Name.Trim();
        project.Description = request.Description;
        await context.SaveChangesAsync();
        return ToResponse(project);
    }

    public async Task DeleteAsync(Guid id, string userId)
    {
        var project = await context.Projects.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId)
            ?? throw new KeyNotFoundException("Project not found.");
        context.Projects.Remove(project);
        await context.SaveChangesAsync();
    }

    private static ProjectResponse ToResponse(Project p) => new(p.Id, p.Name, p.Description, p.CreatedAt);
}
