using Microsoft.EntityFrameworkCore;
using ProjectTaskManagement.Application.DTOs.Tasks;
using ProjectTaskManagement.Application.Interfaces;
using ProjectTaskManagement.Domain.Entities;
using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.Application.Services;

public class TaskService(IApplicationDbContext context) : ITaskService
{
    public async Task<TaskResponse> CreateAsync(Guid projectId, CreateTaskRequest request, string userId)
    {
        var projectExists = await context.Projects.AnyAsync(p => p.Id == projectId && p.UserId == userId);
        if (!projectExists) throw new KeyNotFoundException("Project not found.");

        var task = new ProjectTask
        {
            Title = request.Title.Trim(),
            Description = request.Description,
            DueDate = request.DueDate,
            Priority = request.Priority,
            ProjectId = projectId
        };

        context.ProjectTasks.Add(task);
        await context.SaveChangesAsync();
        return ToResponse(task);
    }

    public async Task<IReadOnlyList<TaskResponse>> GetByProjectAsync(Guid projectId, string userId)
    {
        var projectExists = await context.Projects.AnyAsync(p => p.Id == projectId && p.UserId == userId);
        if (!projectExists) throw new KeyNotFoundException("Project not found.");

        return await context.ProjectTasks
            .Where(t => t.ProjectId == projectId)
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new TaskResponse(t.Id, t.Title, t.Description, t.Status, t.DueDate, t.Priority, t.ProjectId))
            .ToListAsync();
    }

    public async Task<TaskResponse> UpdateStatusAsync(Guid id, ProjectTaskStatus status, string userId)
    {
        var task = await context.ProjectTasks
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == id && t.Project.UserId == userId)
            ?? throw new KeyNotFoundException("Task not found.");

        task.Status = status;
        await context.SaveChangesAsync();
        return ToResponse(task);
    }

    public async Task DeleteAsync(Guid id, string userId)
    {
        var task = await context.ProjectTasks
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == id && t.Project.UserId == userId)
            ?? throw new KeyNotFoundException("Task not found.");

        context.ProjectTasks.Remove(task);
        await context.SaveChangesAsync();
    }

    private static TaskResponse ToResponse(ProjectTask t) => new(t.Id, t.Title, t.Description, t.Status, t.DueDate, t.Priority, t.ProjectId);
}
