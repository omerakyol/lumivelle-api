using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Core.Entities.Concrete;

/// <summary>
/// Pagination filter
/// </summary>
public class PaginationFilter
{
    public PaginationFilter()
    {
        PageNumber = 1;
        PageSize = 10;
    }

    public PaginationFilter(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize > 10 ? 10 : pageSize;
    }

    [FromQuery(Name = "pageNumber")] public int PageNumber { get; set; }
    [FromQuery(Name = "pageSize")] public int PageSize { get; set; }
}