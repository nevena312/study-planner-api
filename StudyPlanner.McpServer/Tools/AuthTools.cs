using System.ComponentModel;
using ModelContextProtocol.Server;
using StudyPlanner.McpServer.Services;

namespace StudyPlanner.McpServer.Tools;

[McpServerToolType]
public class AuthTools
{
    private readonly AuthService _authService;

    public AuthTools(AuthService authService)
    {
        _authService = authService;
    }

    [McpServerTool, Description("Login to Study Planner using email and password.")]
    public async Task<string> LoginUser(string email, string password)
    {
        return await _authService.LoginAsync(email, password);
    }
}