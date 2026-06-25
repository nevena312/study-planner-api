using MediatR;
using StudyPlanner.Api.DTOs.Auth;

namespace StudyPlanner.Api.Features.Auth.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password)
    : IRequest<AuthResponseDto?>;