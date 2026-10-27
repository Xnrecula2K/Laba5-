namespace Laba5;

internal class PlayTicTakToe
{
    public static void Run()
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
            int moves = 0;

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
                moves++;

                if (CheckWin(board, size, row, col, currentPlayer))
                {
                    PrintBoard(board, size);
                    Console.WriteLine($"Игрок {currentPlayer} победил!");
                    gameFinished = true;
                }
                else if (moves == size * size)
                {
                    PrintBoard(board, size);
                    Console.WriteLine("Ничья! Все клетки заполнены.");
                    gameFinished = true;
                }
                else
                {
                    currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                }
            }

            Console.Write("Сыграть еще раз (y/n)? ");
            string ans = Console.ReadLine();
            again = (ans == "y");
        }
    }

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
