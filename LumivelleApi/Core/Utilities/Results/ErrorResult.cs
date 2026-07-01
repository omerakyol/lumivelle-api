using System;

namespace Core.Utilities.Results;

[Serializable]
public class ErrorResult : Result
{
    public ErrorResult(ResultMessage message)
        : base(false, message)
    {
    }

    public ErrorResult()
        : base(false)
    {
    }
}