using MediatR;
using StudyPlanner.Api.DTOs.Exams;
using StudyPlanner.Api.Mappings;
using StudyPlanner.Api.Models;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.Exams.CreateExam;

public class CreateExamCommandHandler : IRequestHandler<CreateExamCommand, ExamReadDto?>
{
    private readonly IExamRepository _examRepository;
    private readonly ISubjectRepository _subjectRepository;

    public CreateExamCommandHandler(
        IExamRepository examRepository,
        ISubjectRepository subjectRepository)
    {
        _examRepository = examRepository;
        _subjectRepository = subjectRepository;
    }

    public async Task<ExamReadDto?> Handle(CreateExamCommand request, CancellationToken cancellationToken)
    {
        var subject = await _subjectRepository.GetByIdAsync(request.SubjectId, request.UserId);

        if (subject == null)
            return null;

        var exam = new Exam
        {
            Title = request.Title,
            ExamDate = request.ExamDate,
            Description = request.Description,
            Type = request.Type,
            SubjectId = request.SubjectId,
            Subject = subject
        };

        var created = await _examRepository.AddAsync(exam);

        return created.ToDto();
    }
}