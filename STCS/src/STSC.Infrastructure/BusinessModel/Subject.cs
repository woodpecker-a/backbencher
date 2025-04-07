namespace STCS.Infrastructure.BusinessModel;

public class Subject
{
    public Guid Id { get; set; }
    public string SubjectName { get; set; }
    public Guid ClassId { get; set; }
    public Class Class { get; set; }
    public ICollection<ClassFile>? Files { get; set; }

}
