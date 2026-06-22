using StudyPlanner.Api.Enums;

namespace StudyPlanner.Api.DTOs.Exams;

public class ExamCreateDto
{
    public string Title { get; set; } = string.Empty;
    public DateTime ExamDate { get; set; }
    public string? Description { get; set; }
    public ExamType Type { get; set; }
    public int SubjectId { get; set; }
}