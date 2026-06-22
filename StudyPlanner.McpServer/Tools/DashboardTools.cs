using System.ComponentModel;
using ModelContextProtocol.Server;
using StudyPlanner.McpServer.Services;

namespace StudyPlanner.McpServer.Tools;

[McpServerToolType]
public class DashboardTools
{
    private readonly StudyPlannerApiService _apiService;

    public DashboardTools(StudyPlannerApiService apiService)
    {
        _apiService = apiService;
    }

    [McpServerTool, Description("Get dashboard summary for the logged-in Study Planner user.")]
    public async Task<string> GetDashboardSummary()
    {
        try
        {
            var dashboard = await _apiService.GetDashboardAsync();

            if (dashboard == null)
                return "Dashboard data could not be loaded.";

            var nextExam = dashboard.NextExamTitle == null
                ? "No upcoming exam."
                : $"{dashboard.NextExamTitle} ({dashboard.NextExamSubjectName}) on {dashboard.NextExamDate}";

            return
                $"Dashboard summary:\n" +
                $"- Subjects: {dashboard.SubjectCount}\n" +
                $"- Exams: {dashboard.ExamCount}\n" +
                $"- Pending tasks: {dashboard.PendingTaskCount}\n" +
                $"- Completed tasks: {dashboard.CompletedTaskCount}\n" +
                $"- Next exam: {nextExam}";
        }
        catch (InvalidOperationException ex)
        {
            return ex.Message;
        }
    }
}