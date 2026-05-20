using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.Application.DTOs.Tasks;

public record TaskResponse(Guid Id, string Title, string? Description, ProjectTaskStatus Status, DateTime? DueDate, TaskPriority Priority, Guid ProjectId);
