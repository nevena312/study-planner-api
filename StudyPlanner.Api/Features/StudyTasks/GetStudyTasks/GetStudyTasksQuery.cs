using MediatR;
using StudyPlanner.Api.DTOs.StudyTasks;

namespace StudyPlanner.Api.Features.StudyTasks.GetStudyTasks;

public record GetStudyTasksQuery(int UserId)
    : IRequest<List<StudyTaskReadDto>>;