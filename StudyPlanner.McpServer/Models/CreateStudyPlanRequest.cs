namespace StudyPlanner.McpServer.Models;

public class CreateStudyPlanRequest
{
    public string Title { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Description { get; set; }
}