using System.ComponentModel;
using ModelContextProtocol.Server;
using StudyPlanner.McpServer.Services;

namespace StudyPlanner.McpServer.Tools;

[McpServerToolType]
public class ExamTools
{
    private readonly StudyPlannerApiService _apiService;

    public ExamTools(StudyPlannerApiService apiService)
    {
        _apiService = apiService;
    }

    [McpServerTool, Description("Get upcoming exams for the logged-in Study Planner user.")]
    public async Task<string> GetUpcomingExams()
    {
        try
        {
            var exams = await _apiService.GetUpcomingExamsAsync();

            if (exams.Count == 0)
                return "There are no upcoming exams.";

            return "Upcoming exams:\n" + string.Join("\n", exams.Select(e =>
                $"- {e.Title} ({e.SubjectName}) on {e.ExamDate:g}"
            ));
        }
        catch (InvalidOperationException ex)
        {
            return ex.Message;
        }
    }
}