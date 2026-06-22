using System.Net.Http.Headers;
using System.Net.Http.Json;
using StudyPlanner.McpServer.Models;

namespace StudyPlanner.McpServer.Services;

public class StudyPlannerApiService
{
    private readonly HttpClient _httpClient;
    private readonly AuthState _authState;

    public StudyPlannerApiService(HttpClient httpClient, AuthState authState)
    {
        _httpClient = httpClient;
        _authState = authState;
    }

    public async Task<DashboardResponse?> GetDashboardAsync()
    {
        EnsureLoggedIn();

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _authState.Token);

        return await _httpClient.GetFromJsonAsync<DashboardResponse>("dashboard");
    }

    public async Task<List<ExamResponse>> GetUpcomingExamsAsync()
    {
        EnsureLoggedIn();

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _authState.Token);

        return await _httpClient.GetFromJsonAsync<List<ExamResponse>>("exams/upcoming")
               ?? new List<ExamResponse>();
    }

    public async Task<List<StudyTaskResponse>> GetPendingTasksAsync()
    {
        EnsureLoggedIn();

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _authState.Token);

        return await _httpClient.GetFromJsonAsync<List<StudyTaskResponse>>("studytasks/pending")
               ?? new List<StudyTaskResponse>();
    }

    private void EnsureLoggedIn()
    {
        if (string.IsNullOrWhiteSpace(_authState.Token))
            throw new InvalidOperationException("User is not logged in. Call login_user first.");
    }

    public async Task<List<SubjectResponse>> GetSubjectsAsync()
    {
        EnsureLoggedIn();

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _authState.Token);

        return await _httpClient.GetFromJsonAsync<List<SubjectResponse>>("subjects")
               ?? new List<SubjectResponse>();
    }

    public async Task<List<StudyPlanResponse>> GetStudyPlansAsync()
    {
        EnsureLoggedIn();

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _authState.Token);

        return await _httpClient.GetFromJsonAsync<List<StudyPlanResponse>>("studyplans")
               ?? new List<StudyPlanResponse>();
    }

    public async Task<List<StudyTaskResponse>> GetTasksBySubjectAsync(int subjectId)
    {
        EnsureLoggedIn();

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _authState.Token);

        return await _httpClient.GetFromJsonAsync<List<StudyTaskResponse>>($"studytasks/by-subject/{subjectId}")
               ?? new List<StudyTaskResponse>();
    }

    public async Task<List<StudyTaskResponse>> GetTasksByPlanAsync(int planId)
    {
        EnsureLoggedIn();

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _authState.Token);

        return await _httpClient.GetFromJsonAsync<List<StudyTaskResponse>>($"studytasks/by-plan/{planId}")
               ?? new List<StudyTaskResponse>();
    }
}