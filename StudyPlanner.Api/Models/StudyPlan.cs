namespace StudyPlanner.Api.Models;

public class StudyPlan
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public ICollection<StudyTask> StudyTasks { get; set; } = new List<StudyTask>();
}