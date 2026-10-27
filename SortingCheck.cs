using System.Diagnostics;
namespace Laba5;

internal static class SortingCheck
{
    public static void Run()
    {
        Console.Write("Введите размер массива от 1 до 10: ");
        bool isLength = int.TryParse(Console.ReadLine(), out int length);

        if (!isLength || length <= 0 || length >= 10)
        {
            Console.WriteLine("Неправильный ввод длины массива от 1 до 10.");
            return;
        }

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

    static void GnomeSort(int[] array)
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

    static void InsertionSort(int[] array)
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
