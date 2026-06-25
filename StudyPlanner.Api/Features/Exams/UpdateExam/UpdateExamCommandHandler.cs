using MediatR;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.Exams.UpdateExam;

public class UpdateExamCommandHandler : IRequestHandler<UpdateExamCommand, bool>
{
    private readonly IExamRepository _examRepository;
    private readonly ISubjectRepository _subjectRepository;

    public UpdateExamCommandHandler(
        IExamRepository examRepository,
        ISubjectRepository subjectRepository)
    {
        _examRepository = examRepository;
        _subjectRepository = subjectRepository;
    }

    public async Task<bool> Handle(UpdateExamCommand request, CancellationToken cancellationToken)
    {
        var exam = await _examRepository.GetByIdAsync(request.Id, request.UserId);

        if (exam == null)
            return false;

        var subject = await _subjectRepository.GetByIdAsync(request.SubjectId, request.UserId);

        if (subject == null)
            return false;

        exam.Title = request.Title;
        exam.ExamDate = request.ExamDate;
        exam.Description = request.Description;
        exam.Type = request.Type;
        exam.SubjectId = request.SubjectId;

        await _examRepository.SaveChangesAsync();

        return true;
    }
}