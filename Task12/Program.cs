using System;
using System.Diagnostics;

namespace Task12
{
    class Program
    {
        static void Main(string[] args)
        {
            //  10k
            //  12952 
            //  1573312 
            Console.WriteLine("Тестирование алгоритмов сортировки простым выбором и блочной сортировки");

            Console.WriteLine("Заполнение массивов...\n");
            int[] BucketArr = null, SelectionArr = null;
            Sort.Fill(100, ref BucketArr, ref SelectionArr);

            // Создаем таймер 
            Stopwatch SW;

            #region Сортировка несортированного массива
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Сортировка несортированного массива"); Console.ForegroundColor = ConsoleColor.White;
            long TimeElapsed1; SW = Stopwatch.StartNew();   // Создаем замер времени выполнения
            Sort.Bucket(ref BucketArr);                     // Выполняем сортировку
            TimeElapsed1 = SW.ElapsedTicks;                 // Сохраняем значение замера времени выполнения
            Console.WriteLine($"На сортировку массива блочным методом было затрачено:\n{TimeElapsed1} такт(ов) таймера\n{Sort.CheckBlock} проверок\n{Sort.CheckBlock} пересылок"); // Выводим время, очищаем таймер         
            Sort.Show("Массив, отсортированный блочным методом", BucketArr);

            long TimeElapsed2; SW = Stopwatch.StartNew();   // Создаем замер времени выполнения
            Sort.Selection(ref SelectionArr);               // Выполняем сортировку
            TimeElapsed2 = SW.ElapsedTicks;                 // Сохраняем значение замера времени выполнения
            Console.WriteLine($"На сортировку массива методом простого выбора было затрачено:\n{TimeElapsed2} такт(ов) таймера\n{Sort.CheckSelection} проверок\n{Sort.RelSelection} пересылок"); // Выводим время, очищаем таймер         
            Sort.Show("Массив, отсортированный методом простого выбора", SelectionArr);
            #endregion

            #region Сортировка отсортированного по возрастанию массива
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("\n\nСортировка отсортированного по возрастанию массива"); Console.ForegroundColor = ConsoleColor.White;

            SW = Stopwatch.StartNew();   // Создаем замер времени выполнения
            Sort.Bucket(ref BucketArr);                     // Выполняем сортировку
            TimeElapsed1 = SW.ElapsedTicks;                 // Сохраняем значение замера времени выполнения
            Console.WriteLine($"На сортировку массива блочным методом было затрачено:\n{TimeElapsed1} такт(ов) таймера\n{Sort.CheckBlock} проверок\n{Sort.CheckBlock} пересылок"); // Выводим время, очищаем таймер         
            Sort.Show("Массив, отсортированный блочным методом", BucketArr);

            SW = Stopwatch.StartNew();   // Создаем замер времени выполнения
            Sort.Selection(ref SelectionArr);               // Выполняем сортировку
            TimeElapsed2 = SW.ElapsedTicks;                 // Сохраняем значение замера времени выполнения
            Console.WriteLine($"На сортировку массива методом простого выбора было затрачено:\n{TimeElapsed2} такт(ов) таймера\n{Sort.CheckSelection} проверок\n{Sort.RelSelection} пересылок"); // Выводим время, очищаем таймер         
            Sort.Show("Массив, отсортированный методом простого выбора", SelectionArr);
            #endregion

            #region Сортировка отсортированного по убыванию массива            
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("\n\nСортировка отсортированного по убыванию массива"); Console.ForegroundColor = ConsoleColor.White;

            Array.Reverse(BucketArr, 0, BucketArr.Length - 1); // Разворачиваем массив
            SW = Stopwatch.StartNew();                      // Создаем замер времени выполнения
            Sort.Bucket(ref BucketArr);                     // Выполняем сортировку
            TimeElapsed1 = SW.ElapsedTicks;                 // Сохраняем значение замера времени выполнения
            Console.WriteLine($"На сортировку массива блочным методом было затрачено:\n{TimeElapsed1} такт(ов) таймера\n{Sort.CheckBlock} проверок\n{Sort.CheckBlock} пересылок"); // Выводим время, очищаем таймер         
            Sort.Show("Массив, отсортированный блочным методом", BucketArr);

            Array.Reverse(SelectionArr, 0, SelectionArr.Length - 1); // Разворачиваем массив
            SW = Stopwatch.StartNew();                      // Создаем замер времени выполнения
            Sort.Selection(ref SelectionArr);               // Выполняем сортировку
            TimeElapsed2 = SW.ElapsedTicks;                 // Сохраняем значение замера времени выполнения
            Console.WriteLine($"На сортировку массива методом простого выбора было затрачено:\n{TimeElapsed2} такт(ов) таймера\n{Sort.CheckSelection} проверок\n{Sort.RelSelection} пересылок"); // Выводим время, очищаем таймер         
            Sort.Show("Массив, отсортированный методом простого выбора", SelectionArr);
            #endregion

            Console.Read();
        }
    }
}
