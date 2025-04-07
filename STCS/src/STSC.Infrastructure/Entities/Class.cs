namespace STCS.Infrastructure.Entities;

public class Class : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string ClassName { get; set; }
    public int SubjectId { get; set; }
    public Subject Subject { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
}
