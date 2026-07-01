using System;

namespace Core.Utilities.Results;

[Serializable]
public class SuccessDataResult<T> : DataResult<T>
{
    public SuccessDataResult(T data, ResultMessage message)
        : base(data, true, message)
    {
    }

    public SuccessDataResult(T data)
        : base(data, true)
    {
    }

    public SuccessDataResult(ResultMessage message)
        : base(default, true, message)
    {
    }

    public SuccessDataResult()
        : base(default, true)
    {
    }
}