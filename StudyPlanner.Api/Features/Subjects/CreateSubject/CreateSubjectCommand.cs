using MediatR;
using StudyPlanner.Api.DTOs.Subjects;

namespace StudyPlanner.Api.Features.Subjects.CreateSubject;

public record CreateSubjectCommand(
    string Name,
    string? Description,
    int UserId
) : IRequest<SubjectReadDto>;