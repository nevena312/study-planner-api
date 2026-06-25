using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyPlanner.Api.DTOs.Auth;
using StudyPlanner.Api.Features.Auth.Login;
using StudyPlanner.Api.Features.Auth.Register;

namespace StudyPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto dto)
    {
        var result = await _mediator.Send(
            new RegisterCommand(
                dto.FirstName,
                dto.LastName,
                dto.Email,
                dto.Password));

        if (result == null)
            return BadRequest("User with this email already exists.");

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
    {
        var result = await _mediator.Send(
            new LoginCommand(
                dto.Email,
                dto.Password));

        if (result == null)
            return Unauthorized("Invalid email or password.");

        return Ok(result);
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        return Ok("Logged out successfully.");
    }
}