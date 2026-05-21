using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskManagement.Application.Common;
using ProjectTaskManagement.Application.DTOs.Tasks;
using ProjectTaskManagement.Application.Interfaces;

namespace ProjectTaskManagement.Api.Controllers;

[ApiController]
[Authorize]
public class TasksController(ITaskService taskService, ICurrentUserService currentUser) : ControllerBase
{
    [HttpPost("api/v1/projects/{projectId:guid}/tasks")]
    public async Task<ActionResult<ApiResponse<TaskResponse>>> Create(Guid projectId, CreateTaskRequest request)
    {
        var result = await taskService.CreateAsync(projectId, request, currentUser.UserId);
        return Ok(ApiResponse<TaskResponse>.Ok(result, "Task created."));
    }

    [HttpGet("api/v1/projects/{projectId:guid}/tasks")]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<TaskResponse>>>> GetByProject(Guid projectId)
    {
        var result = await taskService.GetByProjectAsync(projectId, currentUser.UserId);
        return Ok(ApiResponse<IReadOnlyList<TaskResponse>>.Ok(result));
    }

    [HttpPatch("api/v1/tasks/{id:guid}/status")]
    public async Task<ActionResult<ApiResponse<TaskResponse>>> UpdateStatus(Guid id, UpdateTaskStatusRequest request)
    {
        var result = await taskService.UpdateStatusAsync(id, request.Status, currentUser.UserId);
        return Ok(ApiResponse<TaskResponse>.Ok(result, "Task status updated."));
    }

    [HttpDelete("api/v1/tasks/{id:guid}")]
    public async Task<ActionResult<ApiResponse<object>>> Delete(Guid id)
    {
        await taskService.DeleteAsync(id, currentUser.UserId);
        return Ok(ApiResponse<object>.Ok(null!, "Task deleted."));
    }
}
