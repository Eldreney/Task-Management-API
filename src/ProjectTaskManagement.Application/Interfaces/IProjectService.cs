using ProjectTaskManagement.Application.DTOs.Projects;

namespace ProjectTaskManagement.Application.Interfaces;

public interface IProjectService
{
    Task<ProjectResponse> CreateAsync(CreateProjectRequest request, string userId);
    Task<IReadOnlyList<ProjectResponse>> GetAllAsync(string userId);
    Task<ProjectResponse> GetByIdAsync(Guid id, string userId);
    Task<ProjectResponse> UpdateAsync(Guid id, UpdateProjectRequest request, string userId);
    Task DeleteAsync(Guid id, string userId);
}
