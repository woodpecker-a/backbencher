using STCS.Infrastructure.Enum;

namespace STCS.Infrastructure.BusinessModel;

public class Student
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public IdInit IdInitial { get; set; }
    public int? IdNo { get; set; }
    public Rank Rank { get; set; }
    public Guid CourseId { get; set; }
    public Course? EnrolledCourse { get; set; }

}
