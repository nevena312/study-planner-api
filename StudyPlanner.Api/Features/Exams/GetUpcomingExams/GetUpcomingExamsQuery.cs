using MediatR;
using StudyPlanner.Api.DTOs.Exams;

namespace StudyPlanner.Api.Features.Exams.GetUpcomingExams;

public record GetUpcomingExamsQuery(int UserId) : IRequest<List<ExamReadDto>>;