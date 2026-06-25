using MediatR;
using StudyPlanner.Api.DTOs.Auth;

namespace StudyPlanner.Api.Features.Auth.Login;

public record LoginCommand(
    string Email,
    string Password)
    : IRequest<AuthResponseDto?>;