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
            // Создание списка
            MyCyclicalList<int> ML = new MyCyclicalList<int>() { 1, 1, 3, 4, 5 };

            // Вывод элементов
            for (int i = -5; i < 5; i++)
                Console.Write(ML[i] + "; ");
            Console.WriteLine("\n");

            // Изменение элемента вне границ списка
            ML[-10] = 555;

            // Вывод элементов
            for (int i = -5; i<5; i++)
                Console.Write(ML[i]+"; ");
            Console.WriteLine("\n");

            // Поиск элемента
            List<int> ss = ML.Find(1);
            foreach (int c in ss)
                Console.Write(c+";");
            Console.WriteLine("\n");

            // Удаление элемента по индексу
            ML.Remove(5);

            // Вывод элементов
            for (int i = -5; i < 5; i++)
                Console.Write(ML[i] + "; ");
            Console.WriteLine("\n");

            Console.Read();
        }
    }
   
}
