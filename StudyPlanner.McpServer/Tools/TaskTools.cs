using ModelContextProtocol.Server;
using StudyPlanner.McpServer.Models;
using StudyPlanner.McpServer.Services;
using System.ComponentModel;

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

    [McpServerTool, Description("Mark a study task as completed by task ID.")]
    public async Task<string> CompleteTask(int taskId)
    {
        try
        {
            await _apiService.CompleteTaskAsync(taskId);
            return $"Task with ID {taskId} has been marked as completed.";
        }
        catch (InvalidOperationException ex)
        {
            return ex.Message;
        }
    }

    [McpServerTool, Description("Create a new study task.")]
    public async Task<string> CreateTask(
    string title,
    int subjectId,
    string? description = null,
    int priority = 2,
    DateTime? deadline = null,
    int estimatedDurationMinutes = 60,
    int? studyPlanId = null)
    {
        try
        {
            var request = new CreateStudyTaskRequest
            {
                Title = title,
                Description = description,
                Status = 1,
                Priority = priority,
                Deadline = deadline,
                EstimatedDurationMinutes = estimatedDurationMinutes,
                SubjectId = subjectId,
                StudyPlanId = studyPlanId
            };

            var task = await _apiService.CreateTaskAsync(request);

            if (task == null)
                return "Task was created, but response data could not be loaded.";

            return $"Task created: {task.Id}: {task.Title} ({task.SubjectName})";
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