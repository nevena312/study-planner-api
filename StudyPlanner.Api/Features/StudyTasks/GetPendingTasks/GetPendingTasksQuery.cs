using MediatR;
using StudyPlanner.Api.DTOs.StudyTasks;

namespace StudyPlanner.Api.Features.StudyTasks.GetPendingTasks;

public record GetPendingTasksQuery(int UserId)
    : IRequest<List<StudyTaskReadDto>>;