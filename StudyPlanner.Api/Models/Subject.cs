namespace StudyPlanner.Api.Models;

public class Subject
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public ICollection<Exam> Exams { get; set; } = new List<Exam>();
    public ICollection<StudyTask> StudyTasks { get; set; } = new List<StudyTask>();
}