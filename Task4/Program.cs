using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {           
            do
            {
                // Переменные аргумента и функции 
                double 
                    a = 0, b = 2,   // Границы отрезка [0;2] 
                    c = 0,          // Нужно
                    x = 0,          // Ответ
                    e = 0;          // Точность 
                // Вводим число с клавиатуры и проверяем, соответсвует ли точность интервалу (0;1)
                do Console.Write("Введите точность (0;1): ");
                while (!double.TryParse(Console.ReadLine().Replace('.',','), out e) || !(e > 0 && e < 1));
                // Выводим, что вообще делает прога
                Console.WriteLine("-------------------------------------" +
                                "\nНахождение приближенного значения" +
                                "\nКорня уравнения x + ln(x + 1/2) - 1/2" +
                               $"\nНа интервале [0;2] с точностью {e}");
                Console.WriteLine("-------------------------------------");
                // Считаем
                if(F(a) * F(b) < 0){
                    do
                    {
                        c = (a + b) / 2;
                        if (F(a) * F(c) <= 0)
                            b = c;
                        else
                            a = c;
                    }
                    while (b-a>e);
                    x = (a + b) / 2;
                }
                else{
                    x = a - 1;
                }
                // Проверяем, существует ли значение и выводим ответ
                switch(x < a)
                {
                    case (true): Console.WriteLine("На данном интервале корней нет!"); break;
                    case (false): Console.WriteLine($"Значение x: {x}"); break;
                }
                // Можем повторить   
                Console.WriteLine($"Нажмите любую клавишу для повторного ввода, либо 0 для завершения...\n");
            }
            while (Console.ReadKey(true).KeyChar != '0');
        }

        // Функция, которая считает F
        static double F(double x)
        {
            return x + Math.Log(x + 0.5) - 0.5; 
        }
    }
}
