namespace Laba5;


/// <summary>
/// Класс для выхода из программы.
/// </summary>
internal static class ExitProgram
{
    /// <summary>
    /// Спрашивает у пользователя, хочет ли он выйти, и если да, то останавливает программу.
    /// </summary>
    /// <param name="isRunning">Флаг, который показывает, работает программа или нет.</param>
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
