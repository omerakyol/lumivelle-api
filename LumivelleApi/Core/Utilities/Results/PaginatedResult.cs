using System;
using System.Collections.Generic;
using Core.Utilities.Messages;

namespace Core.Utilities.Results;

public class PaginatedResult<T> : IDataResult<T>
{
    public PaginatedResult(T data, int totalRecords, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber <= 0 ? 1 : pageNumber;
        PageSize = pageSize <= 0 ? 1 : pageSize;
        TotalRecords = totalRecords < 0 ? 0 : totalRecords;
        Data = data;
        Messages = [new ResultMessage { Code = PaginationMessages.ListPaged }];
        Success = true;
        CalculatePaginationInfo();
    }

    public PaginatedResult(T data, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber <= 0 ? 1 : pageNumber;
        PageSize = pageSize <= 0 ? 1 : pageSize;
        TotalRecords = 0;
        Data = data;
        Messages = [new ResultMessage { Code = PaginationMessages.ListPaged }];
        Success = true;
        CalculatePaginationInfo();
    }

    public bool Success { get; set; }
    public List<ResultMessage> Messages { get; set; }
    public T Data { get; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;

    private void CalculatePaginationInfo()
    {
        if (TotalRecords <= 0 || PageSize <= 0)
        {
            TotalPages = 0;
            return;
        }

        TotalPages = (int)Math.Ceiling((double)TotalRecords / PageSize);
        if (PageNumber > TotalPages)
            PageNumber = TotalPages;
    }
}