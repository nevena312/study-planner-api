using MediatR;
using StudyPlanner.Api.DTOs.Auth;
using StudyPlanner.Api.Repositories;
using StudyPlanner.Api.Services;

namespace StudyPlanner.Api.Features.Auth.Login;

public class LoginCommandHandler
    : IRequestHandler<LoginCommand, AuthResponseDto?>
{
    private readonly IUserRepository _userRepository;
    private readonly JwtService _jwtService;

    public LoginCommandHandler(
        IUserRepository userRepository,
        JwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDto?> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null)
            return null;

        var passwordValid =
            BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!passwordValid)
            return null;

        return new AuthResponseDto
        {
            Token = _jwtService.GenerateToken(user),
            UserId = user.Id,
            Email = user.Email
        };
    }
}