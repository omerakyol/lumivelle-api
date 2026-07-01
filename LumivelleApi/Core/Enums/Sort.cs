using System.ComponentModel.DataAnnotations;

namespace Core.Enums;

public enum Sort
{
    [Display(Name = "OrderBy")] ASC = 1,

    [Display(Name = "OrderByDescending")] DESC
}