using System.Net.Http.Json;
using StudyPlanner.McpServer.Models;

namespace StudyPlanner.McpServer.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;

    public string? Token { get; private set; }
    public string? Email { get; private set; }

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var request = new LoginRequest
        {
            Email = email,
            Password = password
        };

        var response = await _httpClient.PostAsJsonAsync("auth/login", request);

        if (!response.IsSuccessStatusCode)
        {
            return "Login failed. Check email and password.";
        }

        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

        if (loginResponse == null || string.IsNullOrWhiteSpace(loginResponse.Token))
        {
            return "Login failed. Token was not returned.";
        }

        Token = loginResponse.Token;
        Email = loginResponse.Email;

        return $"Login successful for {Email}.";
    }
}