namespace STCS.Infrastructure.Entities;

public class Subject : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string SubjectName { get; set; }
    public Guid ClassId { get; set; }
    public Class Class { get; set; }
    public ICollection<ClassFile>? Files { get; set; }

}
