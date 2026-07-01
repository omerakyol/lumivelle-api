using System.ComponentModel.DataAnnotations;

namespace Core.Enums;

public enum EntityStatus
{
    [Display(Name = "Active")] Active = 1,

    [Display(Name = "Passive")] Passive = 2,

    [Display(Name = "Deleted")] Deleted = 3
}