namespace StudyPlanner.Api.Models;

public class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    public ICollection<StudyPlan> StudyPlans { get; set; } = new List<StudyPlan>();
}