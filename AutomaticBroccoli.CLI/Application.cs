using AutomaticBroccoli.CLI.Operation;

namespace AutomaticBroccoli.CLI;

public class Application
{
    private readonly Dictionary<string, IOperation> _menu;

    public Application()
    {
        _menu = new Dictionary<string, IOperation>()
        {
            {"create", new CreateNewNoteOperation() },
            {"get", new GetNotesOperation() },
        };
    }

    public void Run(CancellationToken token)
    {
        Console.Clear();

        while (!token.IsCancellationRequested)
        {
            PrintMenu();

            var operationName = Console.ReadLine() ?? string.Empty;

            if(!_menu.TryGetValue(operationName, out var operation) || operation is null)
            {
                Console.WriteLine($"Команды '{operationName}' не существует");
                Console.WriteLine($"Нажмите любую клавишу что-бы продолжить");
                Console.ReadKey(true);
                Console.Clear();
                continue;
            }
            Console.Clear();
            operation.Invoke();
        }
    }
    private void PrintMenu()
    {
        Console.WriteLine("Список доступных операций над заметками: ");
        foreach (var item in _menu)
        {
            Console.WriteLine($"- {item.Key}: ");
        }
        Console.WriteLine("Введите Ctrl + c чтобы выйти из программы");
    }
}

