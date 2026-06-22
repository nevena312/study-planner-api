using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using StudyPlanner.McpServer.Services;
using System.ComponentModel;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.AddConsole(options =>
{
    options.LogToStandardErrorThreshold = LogLevel.Trace;
});

builder.Services.AddHttpClient<AuthService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7112/api/");
});

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

await builder.Build().RunAsync();

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