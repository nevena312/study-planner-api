using StudyPlanner.Api.Enums;

namespace StudyPlanner.Api.DTOs.StudyTasks;

public class StudyTaskReadDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    public StudyTaskStatus Status { get; set; }
    public TaskPriority Priority { get; set; }

    public DateTime? Deadline { get; set; }
    public int EstimatedDurationMinutes { get; set; }

    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;

    public int? StudyPlanId { get; set; }
    public string? StudyPlanTitle { get; set; }
}