using StudyPlanner.Api.DTOs.Exams;
using StudyPlanner.Api.Models;

namespace StudyPlanner.Api.Mappings;

public static class ExamMappings
{
    public static ExamReadDto ToDto(this Exam exam)
    {
        return new ExamReadDto
        {
            Id = exam.Id,
            Title = exam.Title,
            ExamDate = exam.ExamDate,
            Description = exam.Description,
            Type = exam.Type,
            SubjectId = exam.SubjectId,
            SubjectName = exam.Subject.Name
        };
    }

    public static List<ExamReadDto> ToDtoList(this IEnumerable<Exam> exams)
    {
        return exams.Select(e => e.ToDto()).ToList();
    }
}