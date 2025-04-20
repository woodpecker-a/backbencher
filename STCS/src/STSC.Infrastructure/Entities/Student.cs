using STCS.Infrastructure.Enum;
using System.ComponentModel.DataAnnotations;

namespace STCS.Infrastructure.Entities;

public class Student : IEntity<Guid>
{
    public Guid Id { get; set; }
    [Required]
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public IdInit IdInitial { get; set; }
    public int IdNo { get; set; }
    public Rank Rank { get; set; }
    public Guid? CourseId { get; set; }
    public Course? EnrolledCourse { get; set; }

}
