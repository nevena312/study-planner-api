using MediatR;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.Subjects.DeleteSubject;

public class DeleteSubjectCommandHandler : IRequestHandler<DeleteSubjectCommand, bool>
{
    private readonly ISubjectRepository _subjectRepository;

    public DeleteSubjectCommandHandler(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task<bool> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
    {
        var subject = await _subjectRepository.GetByIdAsync(request.Id, request.UserId);

        if (subject == null)
            return false;

        _subjectRepository.Delete(subject);
        await _subjectRepository.SaveChangesAsync();

        return true;
    }
}