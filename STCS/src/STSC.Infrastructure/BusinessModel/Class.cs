namespace STCS.Infrastructure.BusinessModel;

public class Class
{
    public Guid Id { get; set; }
    public string ClassName { get; set; }
    public int SubjectId { get; set; }
    public Subject Subject { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
}
