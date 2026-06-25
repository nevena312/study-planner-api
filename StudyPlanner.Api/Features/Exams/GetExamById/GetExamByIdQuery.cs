using MediatR;
using StudyPlanner.Api.DTOs.Exams;

namespace StudyPlanner.Api.Features.Exams.GetExamById;

public record GetExamByIdQuery(int Id, int UserId) : IRequest<ExamReadDto?>;