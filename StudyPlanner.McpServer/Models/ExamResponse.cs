using System.Text.Json.Serialization;

namespace StudyPlanner.McpServer.Models;

public class ExamResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime ExamDate { get; set; }
    public string? Description { get; set; }
    public int Type { get; set; }
    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
}