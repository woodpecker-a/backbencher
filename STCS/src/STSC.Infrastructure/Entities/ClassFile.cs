using System.ComponentModel.DataAnnotations;

namespace STCS.Infrastructure.Entities;

public class ClassFile : IEntity<Guid>
{
    public Guid Id { get; set; }
    [Required]
    public string FileName { get; set; } = String.Empty;
    public string? FileType { get; set; }
    public string? FilePath { get; set; }
    public Guid? SubjectId { get; set; }
    public Subject? Subject { get; set; }
}