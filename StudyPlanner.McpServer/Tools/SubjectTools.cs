using System.ComponentModel;
using ModelContextProtocol.Server;
using StudyPlanner.McpServer.Services;

namespace StudyPlanner.McpServer.Tools;

[McpServerToolType]
public class SubjectTools
{
    private readonly StudyPlannerApiService _apiService;

    public SubjectTools(StudyPlannerApiService apiService)
    {
        _apiService = apiService;
    }

    [McpServerTool, Description("Get all subjects for the logged-in Study Planner user.")]
    public async Task<string> GetSubjects()
    {
        try
        {
            var subjects = await _apiService.GetSubjectsAsync();

            if (subjects.Count == 0)
                return "No subjects found.";

            return "Subjects:\n" + string.Join("\n", subjects.Select(s =>
                $"- {s.Id}: {s.Name}" + (string.IsNullOrWhiteSpace(s.Description) ? "" : $" — {s.Description}")
            ));
        }
        catch (InvalidOperationException ex)
        {
            return ex.Message;
        }
    }

    [McpServerTool, Description("Get study tasks for a specific subject by subject ID.")]
    public async Task<string> GetTasksBySubject(int subjectId)
    {
        try
        {
            var tasks = await _apiService.GetTasksBySubjectAsync(subjectId);

            if (tasks.Count == 0)
                return $"No tasks found for subject ID {subjectId}.";

            return $"Tasks for subject ID {subjectId}:\n" + string.Join("\n", tasks.Select(t =>
                $"- {t.Id}: {t.Title} | Status: {FormatStatus(t.Status)} | Priority: {FormatPriority(t.Priority)} | Deadline: {(t.Deadline.HasValue ? t.Deadline.Value.ToString("g") : "No deadline")}"
            ));
        }
        catch (InvalidOperationException ex)
        {
            return ex.Message;
        }
    }

    private static string FormatStatus(int status)
    {
        return status switch
        {
            1 => "Planned",
            2 => "In Progress",
            3 => "Completed",
            _ => "Unknown"
        };
    }

    private static string FormatPriority(int priority)
    {
        return priority switch
        {
            1 => "Low",
            2 => "Medium",
            3 => "High",
            _ => "Unknown"
        };
    }
}