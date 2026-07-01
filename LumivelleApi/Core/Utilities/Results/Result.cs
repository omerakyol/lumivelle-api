using System;
using System.Collections.Generic;

namespace Core.Utilities.Results;

[Serializable]
public class Result : IResult
{
    public Result()
    {
    }

    public Result(bool success)
    {
        Success = success;
        Messages = [];
    }

    public Result(bool success, ResultMessage message)
        : this(success)
    {
        Messages = [message];
    }

    public Result(bool success, List<ResultMessage> messages)
        : this(success)
    {
        Messages = messages;
    }

    public bool Success { get; set; }
    public List<ResultMessage> Messages { get; set; } = [];
}

public class ResultMessage
{
    public string Code { get; set; }
    public string Description { get; set; }
}