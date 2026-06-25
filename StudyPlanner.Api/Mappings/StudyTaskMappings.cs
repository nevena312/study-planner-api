using StudyPlanner.Api.DTOs.StudyTasks;
using StudyPlanner.Api.Models;

namespace StudyPlanner.Api.Mappings;

public static class StudyTaskMappings
{
    public static StudyTaskReadDto ToDto(this StudyTask task)
    {
        return new StudyTaskReadDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            Priority = task.Priority,
            Deadline = task.Deadline,
            EstimatedDurationMinutes = task.EstimatedDurationMinutes,
            SubjectId = task.SubjectId,
            SubjectName = task.Subject.Name,
            StudyPlanId = task.StudyPlanId,
            StudyPlanTitle = task.StudyPlan != null ? task.StudyPlan.Title : null
        };
    }

    public static List<StudyTaskReadDto> ToDtoList(this IEnumerable<StudyTask> tasks)
    {
        return tasks.Select(t => t.ToDto()).ToList();
    }
}