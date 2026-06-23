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

    public async Task<StudyTaskResponse?> GetTaskByIdAsync(int taskId)
    {
        EnsureLoggedIn();

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _authState.Token);

        return await _httpClient.GetFromJsonAsync<StudyTaskResponse>($"studytasks/{taskId}");
    }

    public async Task CompleteTaskAsync(int taskId)
    {
        EnsureLoggedIn();

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _authState.Token);

        var task = await GetTaskByIdAsync(taskId);

        if (task == null)
            throw new InvalidOperationException($"Task with ID {taskId} was not found.");

        var updateRequest = new
        {
            title = task.Title,
            description = task.Description,
            status = 3,
            priority = task.Priority,
            deadline = task.Deadline,
            estimatedDurationMinutes = task.EstimatedDurationMinutes,
            subjectId = task.SubjectId,
            studyPlanId = task.StudyPlanId
        };

        var response = await _httpClient.PutAsJsonAsync($"studytasks/{taskId}", updateRequest);

        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException($"Failed to complete task with ID {taskId}.");
    }

    public async Task<StudyTaskResponse?> CreateTaskAsync(CreateStudyTaskRequest request)
    {
        EnsureLoggedIn();

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _authState.Token);

        if (request.Deadline.HasValue)
        {
            request.Deadline = EnsureUtc(request.Deadline.Value);
        }

        var response = await _httpClient.PostAsJsonAsync("studytasks", request);

        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException("Failed to create study task.");

        return await response.Content.ReadFromJsonAsync<StudyTaskResponse>();
    }

    public async Task<StudyPlanResponse?> CreateStudyPlanAsync(CreateStudyPlanRequest request)
    {
        EnsureLoggedIn();

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _authState.Token);

        request.StartDate = EnsureUtc(request.StartDate);
        request.EndDate = EnsureUtc(request.EndDate);

        var response = await _httpClient.PostAsJsonAsync("studyplans", request);

        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException("Failed to create study plan.");

        return await response.Content.ReadFromJsonAsync<StudyPlanResponse>();
    }

    public async Task<StudyPlanResponse?> GenerateStudyPlanAsync(
    GenerateStudyPlanRequest request)
    {
        EnsureLoggedIn();

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _authState.Token);

        request.StartDate = EnsureUtc(request.StartDate);
        request.EndDate = EnsureUtc(request.EndDate);

        var response =
            await _httpClient.PostAsJsonAsync(
                "studyplans/generate",
                request);

        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException(
                "Failed to generate study plan.");

        return await response.Content
            .ReadFromJsonAsync<StudyPlanResponse>();
    }

    private static DateTime EnsureUtc(DateTime dateTime)
    {
        return dateTime.Kind switch
        {
            DateTimeKind.Utc => dateTime,
            DateTimeKind.Local => dateTime.ToUniversalTime(),
            DateTimeKind.Unspecified => DateTime.SpecifyKind(dateTime, DateTimeKind.Utc),
            _ => dateTime
        };
    }
}