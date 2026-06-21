namespace StudyPlanner.Api.Models;

public class StudyTask
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    public string Status { get; set; } = "Planned";
    public string Priority { get; set; } = "Medium";

    public DateTime? Deadline { get; set; }
    public int EstimatedDurationMinutes { get; set; }

    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;

    public int? StudyPlanId { get; set; }
    public StudyPlan? StudyPlan { get; set; }
}