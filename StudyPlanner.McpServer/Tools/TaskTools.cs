using System.ComponentModel;
using ModelContextProtocol.Server;
using StudyPlanner.McpServer.Services;

namespace StudyPlanner.McpServer.Tools;

[McpServerToolType]
public class TaskTools
{
    private readonly StudyPlannerApiService _apiService;

    public TaskTools(StudyPlannerApiService apiService)
    {
        _apiService = apiService;
    }

    [McpServerTool, Description("Get pending study tasks for the logged-in Study Planner user.")]
    public async Task<string> GetPendingTasks()
    {
        try
        {
            var tasks = await _apiService.GetPendingTasksAsync();

            if (tasks.Count == 0)
                return "There are no pending study tasks.";

            return "Pending study tasks:\n" + string.Join("\n", tasks.Select(t =>
                $"- {t.Title} ({t.SubjectName}) | Priority: {FormatPriority(t.Priority)} | Deadline: {(t.Deadline.HasValue ? t.Deadline.Value.ToString("g") : "No deadline")}"
            ));
        }
        catch (InvalidOperationException ex)
        {
            return ex.Message;
        }
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