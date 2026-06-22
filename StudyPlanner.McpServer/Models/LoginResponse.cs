namespace StudyPlanner.McpServer.Models;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string Email { get; set; } = string.Empty;
}