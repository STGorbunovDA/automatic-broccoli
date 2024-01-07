using System.Text;
using AutomaticBroccoli.CLI;


Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.Unicode;

var openLoopsRepository = new OpenLoopsRepository();

{
    Console.WriteLine("Что Вас беспокоит сейчас?");

    string? note = null;

    do
    {
        note = Console.ReadLine();

    } while (string.IsNullOrWhiteSpace(note));


    openLoopsRepository.Add(new OpenLoop
    {
        Note = note,
        CreatedDate = DateTimeOffset.UtcNow
    });
}


{
    var openLoops = openLoopsRepository.Get();
    var groop = openLoops.GroupBy(x => new DateTime(x.CreatedDate.Year, x.CreatedDate.Month, x.CreatedDate.Day));


    foreach (var groupOfOpenLoops in groop)
    {
        Console.WriteLine($"Ваши заботы за: {groupOfOpenLoops.Key:dd.MM.yyyy}");

        foreach (var openLoop in groupOfOpenLoops.ToArray())
        {
            Console.WriteLine(openLoop.Note);
        }

    }
}


