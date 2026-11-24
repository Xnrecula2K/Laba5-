using System.Diagnostics;

namespace Laba5;

/// <summary>
/// Класс для сравнения двух сортировок: гномьей и вставками.
/// </summary>
internal class SortingCheck
{
    /// <summary>
    /// Длина массива.
    /// </summary>
    private int _len;

    /// <summary>
    /// Конструктор по умолчанию. Ставит длину массива 10.
    /// </summary>
    public SortingCheck()
    {
        _len = 10;
    }

    /// <summary>
    /// Конструктор, где длину массива задаём сами.
    /// </summary>
    /// <param name="len">Длина массива от 1 до 10. Если не подходит, ставится 10.</param>
    public SortingCheck(int len)
    {
        if (len <= 0 || len > 10)
        {
            Console.WriteLine("Некорректная длина в конструкторе. Установлено значение по умолчанию (10).");
            _len = 10;
        }
        else
        {
            _len = len;
        }
    }

    /// <summary>
    /// Запускает сортировку с длиной, которую задали в конструкторе.
    /// </summary>
    public void RunDefault()
    {
        ExecuteSorting(_len);
    }

    /// <summary>
    /// Спрашивает длину массива и запускает сортировку.
    /// </summary>
    public void RunCustom()
    {
        Console.Write("Введите размер массива от 1 до 10: ");
        bool isLength = int.TryParse(Console.ReadLine(), out int length);

        if (!isLength || length <= 0 || length > 10)
        {
            Console.WriteLine("Неправильный ввод длины массива от 1 до 10.");
            return;
        }

        _len = length;
        ExecuteSorting(_len);
    }

    /// <summary>
    /// Заполняет массив случайными числами, сортирует его двумя способами и показывает, какой быстрее.
    /// </summary>
    /// <param name="length">Длина массива.</param>
    private void ExecuteSorting(int length)
    {
        int[] array = new int[length];
        Random rand = new Random();

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = rand.Next(-50, 50);
        }

        int[] preArray = (int[])array.Clone();
        int[] backup = (int[])array.Clone();

        Stopwatch sw = new Stopwatch();

        sw.Start();
        GnomeSort(array);
        sw.Stop();
        double gnomeTime = sw.Elapsed.TotalMicroseconds;

        Array.Copy(backup, array, array.Length);

        sw.Restart();
        InsertionSort(array);
        sw.Stop();
        double insertionTime = sw.Elapsed.TotalMicroseconds;

        Console.WriteLine("\nИсходный массив:      " + string.Join(", ", preArray));
        Console.WriteLine("После сортировки:     " + string.Join(", ", array));

        Console.WriteLine($"1) Гномья сортировка:    {gnomeTime:F2} мкс");
        Console.WriteLine($"2) Сортировка вставками: {insertionTime:F2} мкс");

        if (gnomeTime < insertionTime)
        {
            Console.WriteLine("Гномья сортировка быстрее");
        }
        else if (insertionTime < gnomeTime)
        {
            Console.WriteLine("Сортировка вставками быстрее");
        }
        else
        {
            Console.WriteLine("Оба алгоритма показали одинаковое время.");
        }
    }

    /// <summary>
    /// Гномья сортировка массива по возрастанию.
    /// </summary>
    /// <param name="array">Массив, который сортируем.</param>
    private void GnomeSort(int[] array)
    {
        int index = 0;
        while (index < array.Length)
        {
            if (index == 0 || array[index] >= array[index - 1])
            {
                index++;
            }
            else
            {
                int temp = array[index];
                array[index] = array[index - 1];
                array[index - 1] = temp;
                index--;
            }
        }
    }

    /// <summary>
    /// Сортировка вставками по возрастанию.
    /// </summary>
    /// <param name="array">Массив, который сортируем.</param>
    private void InsertionSort(int[] array)
    {
        for (int i = 1; i < array.Length; i++)
        {
            int key = array[i];
            int j = i - 1;

            while (j >= 0 && array[j] > key)
            {
                array[j + 1] = array[j];
                j--;
            }

            array[j + 1] = key;
        }
    }
}
