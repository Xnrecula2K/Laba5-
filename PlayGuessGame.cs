namespace Laba5;

internal class PlayGuessGame
{

    public static void Run(ref bool isRunning)
    {
        if (!GameInputs(out int a, out int b))
        {
            return;
        }

        GameLogic(a, b, ref isRunning);
    }
    public static bool GameInputs(out int a, out int b)
    {
        a = 0;
        b = 0;

        Console.Write("Введите значение a: ");
        bool isParsedA = int.TryParse(Console.ReadLine(), out a);

        Console.Write("Введите значение b: ");
        bool isParsedB = int.TryParse(Console.ReadLine(), out b);

        if (!isParsedA || !isParsedB)
        {
            Console.WriteLine("Неверный ввод чисел.");
            return false;
        }

        if (b <= 0)
        {
            Console.WriteLine("Ошибка значение b должно быть больше 0.");
            return false;
        }

        double cosVal = Math.Cos(2 * a);
        if (cosVal <= 0)
        {
            Console.WriteLine("Ошибка cos(2a) должен быть строго больше 0.");
            return false;
        }

        return true;
    }

    public static void GameLogic(int a, int b, ref bool isRunning)
    {
        double cosVal = Math.Cos(2 * a);
        double res1 = Math.Log(b, 5);
        double res2 = Math.Sqrt(cosVal);

        double res3 = res1 / res2;
        double result = Math.Pow(Math.Sin(res3), 2);
        double resultr = Math.Round(result, 2);

        bool isGuessed = false;

        for (int attempt = 1; attempt <= 3; attempt++)
        {
            Console.Write($"Попытка {attempt}/3. Введите ваш ответ: ");
            bool isParsedAns = double.TryParse(Console.ReadLine(), out double ans);

            if (!isParsedAns)
            {
                Console.WriteLine("Неверный формат числа.");
                continue;
            }

            if (Math.Abs(ans - resultr) < 0.001)
            {
                Console.WriteLine("Ответ верный");
                isGuessed = true;
                isRunning = false;
                break;
            }
            else
            {
                Console.WriteLine("Ответ неверный.");
            }
        }

        if (!isGuessed)
        {
            Console.WriteLine("Попытки закончились.");
        }

        Console.WriteLine($"Правильный ответ: {resultr}");
    }
}
