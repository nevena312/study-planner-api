namespace StudyPlanner.Api.DTOs.StudyPlans;

public class StudyPlanReadDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }

    public int TaskCount { get; set; }
}