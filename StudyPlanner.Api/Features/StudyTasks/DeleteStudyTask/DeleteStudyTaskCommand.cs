using MediatR;

namespace StudyPlanner.Api.Features.StudyTasks.DeleteStudyTask;

public record DeleteStudyTaskCommand(int Id, int UserId) : IRequest<bool>;