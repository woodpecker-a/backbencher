namespace STCS.Infrastructure.Entities;

public class Subject : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string SubjectName { get; set; }
    public ICollection<ClassFile>? Files { get; set; }

}
