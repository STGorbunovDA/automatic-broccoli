namespace AutomaticBroccoli.DataAccess.Models;

public record OpenLoop
{
    public OpenLoop(Guid id, Note note, CreatedDate createdDate)
    {
        Id = id;
       
        Note = note;
        if (createdDate == default)
            throw new ArgumentException($"Invalid CreatedDate '{createdDate}'", paramName: nameof(createdDate));
        CreatedDate = createdDate;
    }

    public Guid Id { get; init; }
    public Note Note { get; init; }
    public CreatedDate CreatedDate { get; init; }
}

