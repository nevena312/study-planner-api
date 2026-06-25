using MediatR;

namespace StudyPlanner.Api.Features.Subjects.UpdateSubject;

public record UpdateSubjectCommand(
    int Id,
    string Name,
    string? Description,
    int UserId
) : IRequest<bool>;