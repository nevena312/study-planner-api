using MediatR;
using StudyPlanner.Api.DTOs.Exams;
using StudyPlanner.Api.Enums;

namespace StudyPlanner.Api.Features.Exams.CreateExam;

public record CreateExamCommand(
    string Title,
    DateTime ExamDate,
    string? Description,
    ExamType Type,
    int SubjectId,
    int UserId
) : IRequest<ExamReadDto?>;