using MediatR;
using StudyPlanner.Api.DTOs.StudyTasks;
using StudyPlanner.Api.Enums;

namespace StudyPlanner.Api.Features.StudyTasks.CreateStudyTask;

public record CreateStudyTaskCommand(
    string Title,
    string? Description,
    StudyTaskStatus Status,
    TaskPriority Priority,
    DateTime? Deadline,
    int EstimatedDurationMinutes,
    int SubjectId,
    int? StudyPlanId,
    int UserId)
    : IRequest<StudyTaskReadDto?>;