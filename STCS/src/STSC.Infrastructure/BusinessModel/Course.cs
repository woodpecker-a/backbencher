using System.ComponentModel.DataAnnotations;

namespace STCS.Infrastructure.BusinessModel;

public class Course
{
    public Guid Id { get; set; }
    [Required]
    public string CourseName { get; set; }
    public string CourseCode { get; set; }
    public DateTime CourseStartDate { get; set; }
    public int CourseDuration { get; set; }
    public Guid? OICId { get; set; }
    public Instructor? OIC { get; set; }
    public Guid? JICId { get; set; }
    public Instructor? JIC { get; set; }
    public Guid? NICId { get; set; }
    public Instructor? NIC { get; set; }
    public bool IsCompleted { get; set; } = false;
    public ICollection<Subject>? Subjects { get; set; }
    public ICollection<Class>? Classes { get; set; }
    public ICollection<Student>? Students { get; set; }
    public ICollection<Instructor>? Instructors { get; set; }
}
