using AutomaticBroccoli.CLI.Models;
using AutomaticBroccoli.CLI.Repository;

namespace AutomaticBroccoli.CLI.Operation;

public class CreateNewNoteOperation : IOperation
{
    public void Invoke()
    {
        Console.WriteLine("Что Вас беспокоит сейчас?");

        string? note;

        do
        {
            note = Console.ReadLine();

        } while (string.IsNullOrWhiteSpace(note));


        OpenLoopsRepository.Add(new OpenLoop
        {
            Note = note,
            CreatedDate = DateTimeOffset.UtcNow
        });
    }
}

