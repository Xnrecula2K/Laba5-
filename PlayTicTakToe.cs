namespace Laba5;


/// <summary>
/// Игра "Крестики-нолики" на поле 10x10. Побеждает тот, кто первый соберёт 5 в ряд.
/// </summary>
internal class PlayTicTakToe
{
    /// <summary>
    /// Показывает меню игры: можно играть, проверить ничью или выйти.
    /// </summary>
    public static void Run()
    {
        while (true)
        {
            Console.WriteLine("\n=== ГЛАВНОЕ МЕНЮ ===");
            Console.WriteLine("1 - Играть нормально");
            Console.WriteLine("2 - Проверить ничью");
            Console.WriteLine("0 - Выход");
            Console.Write("Выберите пункт (1, 2 или 0): ");
            string choice = Console.ReadLine()?.Trim();

            if (choice == "1")
            {
                PlayGame();
            }
            else if (choice == "2")
            {
                TestDraw();
            }
            else if (choice == "0")
            {
                break;
            }
            else
            {
                Console.WriteLine("Неверный пункт меню.");
            }
        }
    }

    /// <summary>
    /// Основная игра для двух игроков. Ходы делаются по очереди, пока кто-то не победит или не будет ничьей.
    /// </summary>
    public static void PlayGame()
    {
        bool again = true;
        while (again)
        {
            const int size = 10;
            char[,] board = new char[size, size];

            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    board[r, c] = '.';
                }
            }

            char currentPlayer = 'X';
            bool gameFinished = false;

            while (!gameFinished)
            {
                PrintBoard(board, size);
                Console.WriteLine($"Ход игрока {currentPlayer}");

                int row, col;
                while (true)
                {
                    Console.Write("Введите координаты (например A3 или 1 3): ");
                    string input = Console.ReadLine();

                    if (TryParseCell(input, size, out row, out col))
                    {
                        if (board[row, col] == '.')
                        {
                            break;
                        }
                        Console.WriteLine("Клетка уже занята.");
                    }
                    else
                    {
                        Console.WriteLine("Неверные координаты.");
                    }
                }

                board[row, col] = currentPlayer;

                if (CheckWin(board, size, row, col, currentPlayer))
                {
                    PrintBoard(board, size);
                    Console.WriteLine($"Игрок {currentPlayer} победил!");
                    gameFinished = true;
                }
                else if (CheckDraw(board, size))
                {
                    PrintBoard(board, size);
                    Console.WriteLine("Ничья! Проверка на ничью работает успешно!");
                    gameFinished = true;
                }
                else
                {
                    currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                }
            }

            Console.Write("Сыграть еще раз (y/n)? ");
            string ans = Console.ReadLine();
            again = (ans == "y" || ans == "Y");
        }
    }

    /// <summary>
    /// Тестовый режим. Заполняет доску на 99 клеток и проверяет, что ничья определяется правильно.
    /// </summary>
    public static void TestDraw()
    {
        const int size = 10;
        char[,] board = new char[size, size];

        for (int r = 0; r < size; r++)
        {
            for (int c = 0; c < size; c++)
            {
                board[r, c] = ((r / 2 + c) % 2 == 0) ? 'X' : 'O';
            }
        }
        board[size - 1, size - 1] = '.';

        Console.WriteLine("\n--- ТЕСТОВЫЙ РЕЖИМ ПРОВЕРКИ НИЧЬЕЙ ---");
        Console.WriteLine("Доска заполнена на 99 клеток. Единственная свободная клетка: J10.");
        PrintBoard(board, size);

        int row, col;
        while (true)
        {
            Console.Write("Сделайте ход в свободную клетку J10 (или 10 10): ");
            string input = Console.ReadLine();

            if (TryParseCell(input, size, out row, out col))
            {
                if (board[row, col] == '.')
                {
                    break;
                }
                Console.WriteLine("Эта клетка уже занята. Введите J10.");
            }
            else
            {
                Console.WriteLine("Неверные координаты. Введите J10.");
            }
        }

        board[row, col] = 'X';

        PrintBoard(board, size);

        if (CheckDraw(board, size))
        {
            Console.WriteLine("Ничья! Проверка на ничью работает успешно!");
        }
        else
        {
            Console.WriteLine("Ошибка: ничья не определена.");
        }

        Console.WriteLine("Нажмите Enter для возврата в меню...");
        Console.ReadLine();
    }

    /// <summary>
    /// Проверяет, есть ли на доске ничья, то есть все клетки уже заняты.
    /// </summary>
    /// <param name="board">Игровая доска.</param>
    /// <param name="size">Размер доски.</param>
    /// <returns>Возвращает true, если свободных клеток нет, иначе false.</returns>
    public static bool CheckDraw(char[,] board, int size)
    {
        for (int r = 0; r < size; r++)
        {
            for (int c = 0; c < size; c++)
            {
                if (board[r, c] == '.')
                {
                    return false;
                }
            }
        }
        return true;
    }

    /// <summary>
    /// Печатает доску в консоль, чтобы игрок видел, куда ходить.
    /// </summary>
    /// <param name="board">Игровая доска.</param>
    /// <param name="size">Размер доски.</param>
    public static void PrintBoard(char[,] board, int size)
    {
        Console.WriteLine("\n   1 2 3 4 5 6 7 8 9 10");
        for (int r = 0; r < size; r++)
        {
            char letter = (char)('A' + r);
            Console.Write(letter + "  ");
            for (int c = 0; c < size; c++)
            {
                Console.Write(board[r, c] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Переводит введённые координаты (например "A3" или "1 3") в индексы массива.
    /// </summary>
    /// <param name="input">Что ввёл пользователь.</param>
    /// <param name="size">Размер доски.</param>
    /// <param name="r">Номер строки (считается с 0).</param>
    /// <param name="c">Номер столбца (считается с 0).</param>
    /// <returns>Возвращает true, если координаты верные, иначе false.</returns>
    public static bool TryParseCell(string input, int size, out int r, out int c)
    {
        r = -1;
        c = -1;

        if (string.IsNullOrWhiteSpace(input)) return false;
        input = input.Trim().ToUpper();

        if (input.Length >= 2 && input[0] >= 'A' && input[0] < 'A' + size)
        {
            r = input[0] - 'A';
            if (int.TryParse(input.Substring(1), out int colNum) && colNum >= 1 && colNum <= size)
            {
                c = colNum - 1;
                return true;
            }
        }

        string[] parts = input.Split(new char[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 2 && int.TryParse(parts[0], out int rNum) && int.TryParse(parts[1], out int cNum))
        {
            if (rNum >= 1 && rNum <= size && cNum >= 1 && cNum <= size)
            {
                r = rNum - 1;
                c = cNum - 1;
                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Проверяет, собрал ли игрок 5 одинаковых символов подряд после последнего хода.
    /// </summary>
    /// <param name="board">Игровая доска.</param>
    /// <param name="size">Размер доски.</param>
    /// <param name="r">Строка, куда сходил игрок.</param>
    /// <param name="c">Столбец, куда сходил игрок.</param>
    /// <param name="symbol">Символ игрока ('X' или 'O').</param>
    /// <returns>Возвращает true, если игрок победил, иначе false.</returns>
    public static bool CheckWin(char[,] board, int size, int r, int c, char symbol)
    {
        int[][] directions = new int[][]
        {
            new int[] { 0, 1 },
            new int[] { 1, 0 },
            new int[] { 1, 1 },
            new int[] { 1, -1 }
        };

        for (int d = 0; d < directions.Length; d++)
        {
            int dr = directions[d][0];
            int dc = directions[d][1];
            int count = 1;

            int step = 1;
            while (true)
            {
                int nr = r + dr * step;
                int nc = c + dc * step;
                if (nr >= 0 && nr < size && nc >= 0 && nc < size && board[nr, nc] == symbol)
                {
                    count++;
                    step++;
                }
                else
                {
                    break;
                }
            }

            step = 1;
            while (true)
            {
                int nr = r - dr * step;
                int nc = c - dc * step;
                if (nr >= 0 && nr < size && nc >= 0 && nc < size && board[nr, nc] == symbol)
                {
                    count++;
                    step++;
                }
                else
                {
                    break;
                }
            }

            if (count >= 5)
            {
                return true;
            }
        }

        return false;
    }
}
