using CSharpFunctionalExtensions;

namespace AutomaticBroccoli.DataAccess.Models;

public readonly record struct Note
{
    private const string NoteCannotBeNullOrWhiteSpaceError = "Note cannot be null or whitespace";
    public Note(string note)
    {
        if (string.IsNullOrWhiteSpace(note))
            throw new ArgumentException(NoteCannotBeNullOrWhiteSpaceError, paramName: nameof(note));
        Value = note;
    }

    public static Result<Note> Create(string note)
    {
        if (string.IsNullOrWhiteSpace(note))
        {
            return Result.Failure<Note>(NoteCannotBeNullOrWhiteSpaceError);
        }
        return new Note(note);
    }

    public string Value { get; }

    public static implicit operator string(Note note) => note.Value;
    public static implicit operator Note(string value) => new(value);
}

