using System;
using Serilog;

namespace Core.CrossCuttingConcerns.Logging.Serilog;

public abstract class LoggerServiceBase
{
    protected ILogger Logger { get; set; }

    public void Verbose(string message, object? propertyValue = null, Exception? exception = null)
    {
        Logger.Verbose(exception, message, propertyValue);
    }

    public void Fatal(string message, object? propertyValue = null, Exception? exception = null)
    {
        Logger.Fatal(exception, message, propertyValue);
    }

    public void Info(string message, object? propertyValue = null, Exception? exception = null)
    {
        Logger.Information(exception, message, propertyValue);
    }

    public void Warn(string message, object? propertyValue = null, Exception? exception = null)
    {
        Logger.Warning(exception, message, propertyValue);
    }

    public void Debug(string message, object? propertyValue = null, Exception? exception = null)
    {
        Logger.Debug(exception, message, propertyValue);
    }

    public void Error(string message, object? propertyValue = null, Exception? exception = null)
    {
        Logger.Error(exception, message, propertyValue);
    }
}