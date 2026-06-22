using System.ComponentModel;
using ModelContextProtocol.Server;
using StudyPlanner.McpServer.Services;

namespace StudyPlanner.McpServer.Tools;

[McpServerToolType]
public class StudyPlanTools
{
    private readonly StudyPlannerApiService _apiService;

    public StudyPlanTools(StudyPlannerApiService apiService)
    {
        _apiService = apiService;
    }

    [McpServerTool, Description("Get all study plans for the logged-in Study Planner user.")]
    public async Task<string> GetStudyPlans()
    {
        try
        {
            var plans = await _apiService.GetStudyPlansAsync();

            if (plans.Count == 0)
                return "No study plans found.";

            return "Study plans:\n" + string.Join("\n", plans.Select(p =>
                $"- {p.Id}: {p.Title} | {p.StartDate:g} - {p.EndDate:g} | Tasks: {p.TaskCount}"
            ));
        }
        catch (InvalidOperationException ex)
        {
            return ex.Message;
        }
    }

    [McpServerTool, Description("Get study tasks for a specific study plan by study plan ID.")]
    public async Task<string> GetTasksByPlan(int planId)
    {
        try
        {
            var tasks = await _apiService.GetTasksByPlanAsync(planId);

            if (tasks.Count == 0)
                return $"No tasks found for study plan ID {planId}.";

            return $"Tasks in study plan ID {planId}:\n" + string.Join("\n", tasks.Select(t =>
                $"- {t.Id}: {t.Title} ({t.SubjectName}) | Status: {FormatStatus(t.Status)} | Priority: {FormatPriority(t.Priority)} | Deadline: {(t.Deadline.HasValue ? t.Deadline.Value.ToString("g") : "No deadline")}"
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