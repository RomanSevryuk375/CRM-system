namespace CRMSystem.Core.Models;

public class WorkProposal
{
    public WorkProposal(int id, int orderId, int workId, int byWorker, int statusId, int decisionStatusId, DateTime date)
    {
        Id = id;
        OrderId = orderId;
        WorkId = workId;
        ByWorker = byWorker;
        StatusId = statusId;
        DecisionStatusId = decisionStatusId;
        Date = date;
    }
    public int Id { get; }
    
    public int OrderId { get; }

    public int WorkId { get; }

    public int ByWorker { get; }

    public int StatusId { get; }

    public int DecisionStatusId {  get; }

    public DateTime Date { get; }

    public static (WorkProposal workPropossal, string error) Create (int id, int orderId, int workId, int byWorker, int statusId, int decisionStatusId, DateTime date)
    {
        var error = string.Empty;

        if (orderId < 0)
            error = "Order Id must be positive";

        if (workId < 0)
            error = "Work Id must be positive";

        if (byWorker < 0)
            error = "By worker Id must be positive";

        if (statusId < 0)
            error = "Status Id must be positive";

        if (decisionStatusId >= 6 && decisionStatusId <= 8)
            error = "Dicision Id must be positive";

        if (date > DateTime.Now)
            error = "Propossed at can't be in future";

        var workPropossal = new WorkProposal(id, orderId, workId, byWorker, statusId, decisionStatusId, date);

        return (workPropossal, error);
    }
}
