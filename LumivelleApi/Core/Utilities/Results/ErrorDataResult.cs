using System;

namespace Core.Utilities.Results;

[Serializable]
public class ErrorDataResult<T> : DataResult<T>
{
    public ErrorDataResult(T data, ResultMessage message)
        : base(data, false, message)
    {
    }

    public ErrorDataResult(T data)
        : base(data, false)
    {
    }

    public ErrorDataResult(ResultMessage message)
        : base(default, false, message)
    {
    }

    public ErrorDataResult()
        : base(default, false)
    {
    }
}