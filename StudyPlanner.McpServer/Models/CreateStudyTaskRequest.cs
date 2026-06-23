namespace StudyPlanner.McpServer.Models;

public class CreateStudyTaskRequest
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    public int Status { get; set; } = 1;
    public int Priority { get; set; } = 2;

    public DateTime? Deadline { get; set; }
    public int EstimatedDurationMinutes { get; set; }

    public int SubjectId { get; set; }
    public int? StudyPlanId { get; set; }
}