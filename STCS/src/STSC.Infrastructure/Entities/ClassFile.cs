namespace STCS.Infrastructure.Entities;

public class ClassFile : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
    public string FilePath { get; set; }
    public int SubjectId { get; set; }
    public Subject Subject { get; set; }
}