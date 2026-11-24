using System;
using Laba5;
namespace Laba5
{

    /// <summary>
    /// Главный класс программы. Тут находится точка входа и меню.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Точка входа в программу. Показывает меню и вызывает нужный метод.
        /// </summary>
        /// <param name="args">Аргументы командной строки (не используются).</param>
        internal static void Main(string[] args)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\nВыберите пункт:");
                Console.WriteLine("1) Об авторе");
                Console.WriteLine("2) Отгадай ответ");
                Console.WriteLine("3) Выход");
                Console.WriteLine("4) Сортировка");
                Console.WriteLine("5) Крестики-нолики 10х10");
                Console.Write("Ваш выбор: ");

                bool isParsed = int.TryParse(Console.ReadLine(), out int ch);

                if (!isParsed)
                {
                    Console.WriteLine("Неверный ввод");
                }
                else
                {
                    switch (ch)
                    {
                        case 1:
                            Author.Run();
                            break;

                        case 2:
                            PlayGuessGame.Run(ref isRunning);
                            break;

                        case 3:
                            ExitProgram.Run(ref isRunning);
                            break;

                        case 4:
                            SortingCheck.Run();
                            break;

                        case 5:
                            PlayTicTakToe.Run();
                            break;

                        default:
                            Console.WriteLine("Неверный пункт меню.");
                            break;
                    }
                }
            }
        }
    }
}
