using ProjectTaskManagement.Domain.Common;

namespace ProjectTaskManagement.Domain.Entities;

public class Project : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public string UserId { get; set; } = string.Empty;

    public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
}