using MediatR;
using StudyPlanner.Api.Enums;

namespace StudyPlanner.Api.Features.Exams.UpdateExam;

public record UpdateExamCommand(
    int Id,
    string Title,
    DateTime ExamDate,
    string? Description,
    ExamType Type,
    int SubjectId,
    int UserId
) : IRequest<bool>;