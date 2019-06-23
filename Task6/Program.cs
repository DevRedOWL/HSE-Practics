using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите количество элементов последовательности: ");
            int Length = GetInt();
            List<double> Sequence = new List<double>();
            Console.Write("Введите первый член последовательности: "); Sequence.Add(GetDouble());
            Console.Write("Введите второй член последовательности: "); Sequence.Add(GetDouble());
            Console.Write("Введите третий член последовательности: "); Sequence.Add(GetDouble());

            // Заполняем последовательность 
            for (int k = 3; k < Length; k++)
                Sequence.Add((13 * Sequence[k - 1]) - (10 * Sequence[k - 2]) + Sequence[k - 3] );

            // Выводим все элементы
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("\nВсе элементы последовательности:\n"); Console.ForegroundColor = ConsoleColor.White;
            foreach (double d in Sequence)
                Console.Write(d + "; ");

            // Проверка четных элементов
            bool SignWasNotChanged = true; // Флаг, принимающий false, если последовательность не возрастающая
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("\n\nВсе элементы последовательности с четным номером:\n"); Console.ForegroundColor = ConsoleColor.White;
            // Проверяем, образуют ли четные элементы возрастающую последовательность
            for (int i = 1; i < Length; i += 2)
            {
                if (SignWasNotChanged && i != 1 && Sequence[i] > Sequence[i - 2]) Console.Write("< "); // Если знак не менялся и не первый элемент и соотв. условию
                else if (i != 1 && Sequence[i] <= 1.7976931348623158e+308) SignWasNotChanged = false; // Если не соответствует условию но не первый элемент
                if(Sequence[i] <= 1.7976931348623158e+308) Console.Write($"{Sequence[i]} ");   // Если число в пределах double
            }

            // Выводим ответ
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"\n\nПоследовательность является {(SignWasNotChanged ? "возрастающей" : "невозрастающей")}"); Console.ForegroundColor = ConsoleColor.White;

            Console.Read();
        }

        /** Метод для ввода Double **/
        static double GetDouble()
        {
            double This;
            while (!double.TryParse(Console.ReadLine().Replace(".", ","), out This))
                Console.Write("Число должно быть вещественным: "); 
            return This;
        }

        /** Метод для ввода длины **/
        static int GetInt()
        {
            int This = 0;
            while (!int.TryParse(Console.ReadLine(), out This) || This < 3)
                Console.Write("Длина последовательности не может быть меньше, чем 3: ");
            return This;
        }       

    }
}
