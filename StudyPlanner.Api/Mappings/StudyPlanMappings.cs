using StudyPlanner.Api.DTOs.StudyPlans;
using StudyPlanner.Api.Models;

namespace StudyPlanner.Api.Mappings;

public static class StudyPlanMappings
{
    public static StudyPlanReadDto ToDto(this StudyPlan studyPlan)
    {
        return new StudyPlanReadDto
        {
            Id = studyPlan.Id,
            Title = studyPlan.Title,
            StartDate = studyPlan.StartDate,
            EndDate = studyPlan.EndDate,
            Description = studyPlan.Description,
            CreatedAt = studyPlan.CreatedAt,
            TaskCount = studyPlan.StudyTasks.Count
        };
    }

    public static List<StudyPlanReadDto> ToDtoList(this IEnumerable<StudyPlan> studyPlans)
    {
        return studyPlans.Select(sp => sp.ToDto()).ToList();
    }
}