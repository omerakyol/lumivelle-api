using System;
using System.Linq.Expressions;
using Hangfire;
using Hangfire.Common;

namespace Core.Utilities.TaskScheduler.Hangfire;

public interface IRecurringJobService
{
    void AddOrUpdate(string jobId, Expression<Action> job, string cronExpression,
        TimeZoneInfo timeZone = null, string queue = "default");

    void AddOrUpdate<T>(string jobId, Expression<Action<T>> job, string cronExpression,
        TimeZoneInfo timeZone = null, string queue = "default");

    void RemoveIfExists(string jobId);

    void Trigger(string jobId);
}

public class HangfireRecurringJobService(IRecurringJobManager recurringJobManager) : IRecurringJobService
{
    public void AddOrUpdate(string jobId, Expression<Action> job, string cronExpression,
        TimeZoneInfo timeZone = null, string queue = "default")
    {
        if (string.IsNullOrWhiteSpace(jobId))
            throw new ArgumentException("Job ID cannot be empty", nameof(jobId));

        if (timeZone == null)
            timeZone = TimeZoneInfo.Local;

        recurringJobManager.AddOrUpdate(
            jobId,
            Job.FromExpression(job),
            cronExpression,
            new RecurringJobOptions
            {
                TimeZone = timeZone,
                QueueName = queue
            });
    }

    public void RemoveIfExists(string jobId)
    {
        if (string.IsNullOrWhiteSpace(jobId))
            throw new ArgumentException("Job ID cannot be empty", nameof(jobId));

        recurringJobManager.RemoveIfExists(jobId);
    }

    public void Trigger(string jobId)
    {
        if (string.IsNullOrWhiteSpace(jobId))
            throw new ArgumentException("Job ID cannot be empty", nameof(jobId));

        recurringJobManager.Trigger(jobId);
    }

    public void AddOrUpdate<T>(string jobId, Expression<Action<T>> job, string cronExpression,
        TimeZoneInfo timeZone = null, string queue = "default")
    {
        if (string.IsNullOrWhiteSpace(jobId))
            throw new ArgumentException("Job ID cannot be empty", nameof(jobId));

        if (timeZone == null)
            timeZone = TimeZoneInfo.Local;

        recurringJobManager.AddOrUpdate(
            jobId,
            Job.FromExpression(job),
            cronExpression,
            new RecurringJobOptions
            {
                TimeZone = timeZone,
                QueueName = queue
            });
    }
}