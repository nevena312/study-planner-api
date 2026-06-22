namespace StudyPlanner.McpServer.Models;

public class StudyTaskResponse
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    public int Status { get; set; }
    public int Priority { get; set; }

    public DateTime? Deadline { get; set; }
    public int EstimatedDurationMinutes { get; set; }

    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;

    public int? StudyPlanId { get; set; }
    public string? StudyPlanTitle { get; set; }
}