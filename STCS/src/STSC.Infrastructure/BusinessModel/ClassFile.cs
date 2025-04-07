namespace STCS.Infrastructure.BusinessModel;

public class ClassFile
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
    public string FilePath { get; set; }
    public Guid SubjectId { get; set; }
    public Subject Subject { get; set; }
}