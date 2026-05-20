using ProjectTaskManagement.Application.DTOs.Tasks;
using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.Application.Interfaces;

public interface ITaskService
{
    Task<TaskResponse> CreateAsync(Guid projectId, CreateTaskRequest request, string userId);
    Task<IReadOnlyList<TaskResponse>> GetByProjectAsync(Guid projectId, string userId);
    Task<TaskResponse> UpdateStatusAsync(Guid id, ProjectTaskStatus status, string userId);
    Task DeleteAsync(Guid id, string userId);
}
