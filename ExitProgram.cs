namespace Laba5;

internal static class ExitProgram
{
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
