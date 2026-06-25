using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyPlanner.Api.DTOs.Auth;
using StudyPlanner.Api.Models;
using StudyPlanner.Api.Repositories;
using StudyPlanner.Api.Services;

namespace StudyPlanner.Api.Features.Auth.Register;

public class RegisterCommandHandler
    : IRequestHandler<RegisterCommand, AuthResponseDto?>
{
    private readonly IUserRepository _userRepository;
    private readonly JwtService _jwtService;

    public RegisterCommandHandler(
        IUserRepository userRepository,
        JwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDto?> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        var emailExists = await _userRepository.EmailExistsAsync(request.Email);

        if (emailExists)
            return null;

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        try
        {
            var createdUser = await _userRepository.AddAsync(user);

            return new AuthResponseDto
            {
                Token = _jwtService.GenerateToken(createdUser),
                UserId = createdUser.Id,
                Email = createdUser.Email
            };
        }
        catch (DbUpdateException)
        {
            return null;
        }
    }
}