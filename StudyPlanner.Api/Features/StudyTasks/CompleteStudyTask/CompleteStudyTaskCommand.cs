using MediatR;

namespace StudyPlanner.Api.Features.StudyTasks.CompleteStudyTask;

public record CompleteStudyTaskCommand(int Id, int UserId) : IRequest<bool>;