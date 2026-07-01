using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Concrete;

public class Language : DocumentDbEntity
{
    [StringLength(50)] public string Name { get; set; }
    [StringLength(2)] public string Code { get; set; }
}