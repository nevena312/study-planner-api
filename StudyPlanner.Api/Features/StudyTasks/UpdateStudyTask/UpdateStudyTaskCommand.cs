using MediatR;
using StudyPlanner.Api.Enums;

namespace StudyPlanner.Api.Features.StudyTasks.UpdateStudyTask;

public record UpdateStudyTaskCommand(
    int Id,
    string Title,
    string? Description,
    StudyTaskStatus Status,
    TaskPriority Priority,
    DateTime? Deadline,
    int EstimatedDurationMinutes,
    int SubjectId,
    int? StudyPlanId,
    int UserId)
    : IRequest<bool>;