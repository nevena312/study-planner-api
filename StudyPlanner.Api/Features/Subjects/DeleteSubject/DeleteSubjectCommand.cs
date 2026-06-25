using MediatR;

namespace StudyPlanner.Api.Features.Subjects.DeleteSubject;

public record DeleteSubjectCommand(int Id, int UserId) : IRequest<bool>;