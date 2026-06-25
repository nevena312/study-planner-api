using MediatR;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.Subjects.UpdateSubject;

public class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand, bool>
{
    private readonly ISubjectRepository _subjectRepository;

    public UpdateSubjectCommandHandler(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task<bool> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
    {
        var subject = await _subjectRepository.GetByIdAsync(request.Id, request.UserId);

        if (subject == null)
            return false;

        subject.Name = request.Name;
        subject.Description = request.Description;

        await _subjectRepository.SaveChangesAsync();

        return true;
    }
}