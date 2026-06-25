using MediatR;
using StudyPlanner.Api.DTOs.Exams;

namespace StudyPlanner.Api.Features.Exams.GetExams;

public record GetExamsQuery(int UserId) : IRequest<List<ExamReadDto>>;