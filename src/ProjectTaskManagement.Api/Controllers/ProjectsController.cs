using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskManagement.Application.Common;
using ProjectTaskManagement.Application.DTOs.Projects;
using ProjectTaskManagement.Application.Interfaces;

namespace ProjectTaskManagement.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/projects")]
public class ProjectsController(IProjectService projectService, ICurrentUserService currentUser) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ApiResponse<ProjectResponse>>> Create(CreateProjectRequest request)
    {
        var result = await projectService.CreateAsync(request, currentUser.UserId);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, ApiResponse<ProjectResponse>.Ok(result, "Project created."));
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<ProjectResponse>>>> GetAll()
    {
        var result = await projectService.GetAllAsync(currentUser.UserId);
        return Ok(ApiResponse<IReadOnlyList<ProjectResponse>>.Ok(result));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApiResponse<ProjectResponse>>> GetById(Guid id)
    {
        var result = await projectService.GetByIdAsync(id, currentUser.UserId);
        return Ok(ApiResponse<ProjectResponse>.Ok(result));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ApiResponse<ProjectResponse>>> Update(Guid id, UpdateProjectRequest request)
    {
        var result = await projectService.UpdateAsync(id, request, currentUser.UserId);
        return Ok(ApiResponse<ProjectResponse>.Ok(result, "Project updated."));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ApiResponse<object>>> Delete(Guid id)
    {
        await projectService.DeleteAsync(id, currentUser.UserId);
        return Ok(ApiResponse<object>.Ok(null!, "Project deleted."));
    }
}
