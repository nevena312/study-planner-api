using MediatR;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.Exams.DeleteExam;

public class DeleteExamCommandHandler : IRequestHandler<DeleteExamCommand, bool>
{
    private readonly IExamRepository _examRepository;

    public DeleteExamCommandHandler(IExamRepository examRepository)
    {
        _examRepository = examRepository;
    }

    public async Task<bool> Handle(DeleteExamCommand request, CancellationToken cancellationToken)
    {
        var exam = await _examRepository.GetByIdAsync(request.Id, request.UserId);

        if (exam == null)
            return false;

        _examRepository.Delete(exam);
        await _examRepository.SaveChangesAsync();

        return true;
    }
}