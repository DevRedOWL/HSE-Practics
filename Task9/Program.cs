using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9
{
    class Program // Поиск, Удаление, Ввод N элементов
    {
        static void Main(string[] args)
        {
            // Просмотр всех элементов и циклическое удаление
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("ВАЖНО: Нумерация элементов начинается с еденицы\n"); Console.ForegroundColor = ConsoleColor.White;

            /// Создание списка
            Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine("..:: Создание списка ::.."); Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Введите количество элементов: ");
            int Length = GetLength(); // Записываем длину
            MyCyclicalList<int> MCL = new MyCyclicalList<int>(Length);

            /// Заполнение списка
            Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine("..:: Заполнение списка ::.."); Console.ForegroundColor = ConsoleColor.White;
            Fill(ref MCL, Length);

            /// Вывод списка
            MCL.Show();

            /// Поиск элемента
            Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine("..:: Поиск элемента ::.."); Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Введите значение элемента, который необходимо найти: ");
            List<int> ss = MCL.Find(GetInt());
            Console.WriteLine("Список индексов, на которых находятся элементы с данным значением: ");
            foreach (int c in ss)
                Console.Write(c + ";");
            Console.WriteLine("\n");

            // Удаление элемента
            Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine("..:: Удаление элемента ::.."); Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Введите количество элементов, которые вы хотите удалить: ");
            int DeleteTo = GetLength();
            for (int i = 0; i < DeleteTo; i++)
            {
                Console.Write("Введите индекс элемента, который необходимо удалить: ");
                MCL.Remove(GetInt());
            }          

            // Вывод списка
            try { MCL.Show(); }
            catch (Exception) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Кажется массив пустой"); }

            Console.Read();
        }

        /** Метод для ввода количества элементов **/
        static int GetLength()
        {
            int This;
            while (!int.TryParse(Console.ReadLine(), out This) || This < 1)
                Console.Write("Число должно быть целым и больше нуля: ");
            return This;
        }

        /** Метод для ввода целого числа **/
        public static int GetInt()
        {
            int This;
            while (!int.TryParse(Console.ReadLine(), out This))
                Console.Write("Число должно быть целым: ");
            return This;
        }
 
        /** Метод заполнения списка типа int **/
        static void Fill(ref MyCyclicalList<int> MCL, int Length)
        {
            Console.WriteLine("Выберите способ заполнения списка:" +
                           "\n[1] - Заполнение вручную" +
                           "\n[2] - Заполнение датчиком случайных чисел" +
                           "\n[0] - Заполнение изначально подобранными значениями\n");
            switch (Console.ReadKey(true).KeyChar)
            {
                case '1':
                    {
                        for (int i = 1; i <= Length; i++)
                        {
                            Console.Write($"Введите элемент с индексом [{i}]: ");
                            MCL.Add(GetInt());
                        }
                        Console.WriteLine($"Список успешно заполнен, количество элементов: {Length}\n");
                    }
                    break;
                case '2':
                    {
                        Random random = new Random();
                        for (int i = 1; i <= Length; i++)
                        {
                            MCL.Add(random.Next(-50, 50));
                            Console.Write($"Элемент [{i}]: {MCL[i]}; \n");
                        }
                        Console.WriteLine($"Список успешно заполнен, количество элементов: {Length}\n");
                    }
                    break;
                default:
                    {
                        MCL = new MyCyclicalList<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
                        Console.WriteLine($"Список успешно заполнен, количество элементов: {Length}\n");
                    }
                    break;
            }
        }

    }
   
}
