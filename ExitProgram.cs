namespace Laba5;


internal static class ExitProgram
{
    /// Запрашивает у пользователя подтверждение завершения работы и обновляет флаг выполнения программы.
    public static void Run(ref bool isRunning)
    {
        Console.Write("Завершить программу (y/n)? ");
        string answer = Console.ReadLine();
        if (answer == "y")
        {
            isRunning = false;
        }
    }
}
