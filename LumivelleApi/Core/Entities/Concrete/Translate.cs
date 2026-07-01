using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Concrete;

public class Translate : DocumentDbEntity
{
    [StringLength(2)] public string Language { get; set; }
    [StringLength(50)] public string Code { get; set; }
    [StringLength(250)] public string Value { get; set; }
}