using MediatR;
using StudyPlanner.Api.DTOs.StudyTasks;

namespace StudyPlanner.Api.Features.StudyTasks.GetTasksBySubject;

public record GetTasksBySubjectQuery(
    int SubjectId,
    int UserId)
    : IRequest<List<StudyTaskReadDto>>;