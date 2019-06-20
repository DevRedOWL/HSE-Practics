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
            MyCyclicalList<int> ML = new MyCyclicalList<int>() { 1, 2, 1, 3, 1, 4, 22, 1, 1 };
            ML[10] = 1000; 

            // Вывод элементов
            for (int i = -9; i<10; i++)
                Console.Write(ML[i]+"; ");
            Console.WriteLine("\n");

            // Поиск элемента
            List<int> ss = ML.Find(1);
            foreach (int c in ss)
                Console.Write(c+";");
            Console.WriteLine("\n");

            Console.Read();
        }
    }
   
}
