using MediatR;

namespace StudyPlanner.Api.Features.Exams.DeleteExam;

public record DeleteExamCommand(int Id, int UserId) : IRequest<bool>;