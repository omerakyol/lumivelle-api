using System;
using System.Collections.Generic;

namespace Core.Utilities.Results;

[Serializable]
public class ApiResult<T>
{
    public bool Success { get; set; }
    public List<ResultMessage> Messages { get; set; }
    public string InternalMessage { get; set; }
    public T Data { get; set; }
}

public class ApiReturn : ApiResult<object>
{
}