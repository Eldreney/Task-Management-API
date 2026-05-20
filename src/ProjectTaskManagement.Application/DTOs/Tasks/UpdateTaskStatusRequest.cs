using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.Application.DTOs.Tasks;

public record UpdateTaskStatusRequest(ProjectTaskStatus Status);
