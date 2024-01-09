namespace AutomaticBroccoli.DataAccess.Models;

public record OpenLoop
{
    public Guid Id { get; init; }
    public string Note { get; init; }
    public DateTimeOffset CreatedDate { get; init; }
}

