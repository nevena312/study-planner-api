using MediatR;
using StudyPlanner.Api.DTOs.StudyTasks;

namespace StudyPlanner.Api.Features.StudyTasks.GetStudyTaskById;

public record GetStudyTaskByIdQuery(
    int Id,
    int UserId)
    : IRequest<StudyTaskReadDto?>;