using System.ComponentModel.DataAnnotations;

namespace STCS.Infrastructure.Entities;

public class Subject : IEntity<Guid>
{
    public Guid Id { get; set; }
    [Required]
    public string SubjectName { get; set; } = String.Empty;
    public Guid? ClassId { get; set; }
    public Class? Class { get; set; }
    public ICollection<ClassFile>? Files { get; set; }

}
