using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                // Переменные аргумента и функции 
                double a = 0, f = 0;
                // Вводим число с клавиатуры
                do Console.Write("Введите целое число: ");
                while (!double.TryParse(Console.ReadLine(), out a));
                // Основная логика
                if (a < 0) f = -a;                  // (-oo ; 0)
                else if (a >= 0 && a <= 1) f = a;   // [0 ; 1]
                else if (a > 1 && a <= 2) f = 1;    // (1 ; 2]
                else if (a > 2) f = -(2 * a) + 5;   // (2 ; +oo)
                Console.WriteLine($"f({a}) = {f}\nНажмите любую клавишу для повторного ввода, либо 0 для завершения...\n");
            }
            while (Console.ReadKey(true).KeyChar != '0');
            
        }
    }
}
