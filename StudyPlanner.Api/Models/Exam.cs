namespace StudyPlanner.Api.Models;

public class Exam
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public DateTime ExamDate { get; set; }
    public string? Description { get; set; }

    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;
}