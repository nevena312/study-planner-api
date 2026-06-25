using StudyPlanner.Api.DTOs.Subjects;
using StudyPlanner.Api.Models;

namespace StudyPlanner.Api.Mappings;

public static class SubjectMappings
{
    public static SubjectReadDto ToDto(this Subject subject)
    {
        return new SubjectReadDto
        {
            Id = subject.Id,
            Name = subject.Name,
            Description = subject.Description
        };
    }

    public static List<SubjectReadDto> ToDtoList(this IEnumerable<Subject> subjects)
    {
        return subjects.Select(s => s.ToDto()).ToList();
    }
}