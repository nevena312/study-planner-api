namespace StudyPlanner.Api.DTOs.Dashboard;

public class DashboardDto
{
    public int SubjectCount { get; set; }
    public int ExamCount { get; set; }
    public int PendingTaskCount { get; set; }
    public int CompletedTaskCount { get; set; }

    public string? NextExamTitle { get; set; }
    public DateTime? NextExamDate { get; set; }
    public string? NextExamSubjectName { get; set; }
}