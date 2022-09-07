namespace CRM.Models;

public enum DealStatus {
        Open,
        Schedule,
        Won,
        Lost
    }

public class Deal {
    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public int Amount { get; private set; }
    public DateTime ScheduledDate { get; private set; }

    public DealStatus Status { get; set; }

    public static Deal Create(string Name, int Amount)
    {
        Random rnd = new();
        return new Deal{
            Id = rnd.Next(10),
            Name = Name,
            Amount = Amount,
            Status = DealStatus.Open
        };
    }

    public void Schedule(DateTime date)
    {
        if (this.Status != DealStatus.Open) {
            throw new Exception("Can't set schedule");
        }
        this.Status = DealStatus.Schedule;
        this.ScheduledDate = date;
    }

    public void Close()
    {
        if (this.Status != DealStatus.Schedule) {
            throw new Exception("Can't close deal");
        }
        this.Status = DealStatus.Won;
    }

    public void Cancel()
    {
        if (this.Status != DealStatus.Schedule) {
            throw new Exception("Can't cancel deal");
        }
        this.Status = DealStatus.Lost;
    }
}