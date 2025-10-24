namespace CRMSystem.Core.Models;

public class Work
{
    public Work(int id, int orderId, int jobId, int workerId, decimal timeSpent, int statusId)
    {
        Id = id;
        OrderId = orderId;
        JobId = jobId;
        WorkerId = workerId;
        TimeSpent = timeSpent;
        StatusId = statusId;
    }
    public int Id { get; }

    public int OrderId { get; }

    public int JobId { get; }

    public int WorkerId { get; }

    public decimal TimeSpent { get; }
    
    public int StatusId { get; }

    public static (Work work, string error) Create (int id, int orderId, int jobId, int workerId, decimal timeSpent, int statusId)
    {
        var error = string.Empty;

        var work = new Work(id, orderId, jobId, workerId, timeSpent, statusId);

        if (orderId <= 0)
            error = "Order Id must be positive";

        if (jobId <= 0)
            error = "Job Id must be positive";

        if (workerId <= 0)
            error = "Worker Id must be positive";

        if (statusId <= 0)
            error = "Status Id must be positive";

        return (work, error);
    }
}
