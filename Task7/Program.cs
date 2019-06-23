using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task7
{
    class Program
    {
        static void Main(string[] args) 
        {
            Console.WriteLine("Список всех 16 булевых функций от 3 аргументов, которые являются линейными:");

            byte[,] XYZ = BoolFunction3nd(); // Матрица значение трех аргументов
            byte[,] C1_C2_C3_C4 = CreatePolynom(); // Матрица значений аргументов
            string[] LinearFunctions = new string[16]; // Сами функции 

            for (int i = 0; i < 16; i++) // Формирование и вывод линейных функций 
            {
                for (int j = 0; j < 8; j++)
                {
                    if (CheckResultIsOne(XYZ, C1_C2_C3_C4, i, j))
                    {
                        LinearFunctions[i] += "1";
                    }
                    else
                    {
                        LinearFunctions[i] += "0";
                    }
                }
                Console.WriteLine(LinearFunctions[i]);
            }
            Console.ReadLine();
        }

        static byte[,] BoolFunction3nd() // Создание стандартных значений переменных 
        {
            byte[,] XYZ = new byte[8, 3];
            XYZ[0, 0] = 0;
            XYZ[0, 1] = 0;
            XYZ[0, 2] = 0;

            XYZ[1, 0] = 0;
            XYZ[1, 1] = 0;
            XYZ[1, 2] = 1;

            XYZ[2, 0] = 0;
            XYZ[2, 1] = 1;
            XYZ[2, 2] = 0;

            XYZ[3, 0] = 0;
            XYZ[3, 1] = 1;
            XYZ[3, 2] = 1;

            XYZ[4, 0] = 1;
            XYZ[4, 1] = 0;
            XYZ[4, 2] = 0;

            XYZ[5, 0] = 1;
            XYZ[5, 1] = 0;
            XYZ[5, 2] = 1;

            XYZ[6, 0] = 1;
            XYZ[6, 1] = 1;
            XYZ[6, 2] = 0;

            XYZ[7, 0] = 1;
            XYZ[7, 1] = 1;
            XYZ[7, 2] = 1;
            return XYZ;
        }

        static byte[,] CreatePolynom() // Создание стандартных значений аргументов полинома Жегалкина 
        {
            byte[,] C1_C2_C3_C4 = new byte[16, 4];
            bool A = true;
            byte B = 0;
            byte C = 0;
            for (int i = 0; i < 16; i++)
            {
                if (A)
                {
                    C1_C2_C3_C4[i, 3] = 0;
                    A = false;
                }
                else
                {
                    C1_C2_C3_C4[i, 3] = 1;
                    A = true;
                }
                if (i < 8)
                {
                    C1_C2_C3_C4[i, 0] = 0;
                }
                else
                {
                    C1_C2_C3_C4[i, 0] = 1;
                }
                if (B < 2)
                {
                    C1_C2_C3_C4[i, 2] = 0;
                    B++;
                }
                else
                {
                    C1_C2_C3_C4[i, 2] = 1;
                    B++;
                    if (B == 4) { B = 0; }
                }
                if (C < 4)
                {
                    C1_C2_C3_C4[i, 1] = 0;
                    C++;
                }
                else
                {
                    C1_C2_C3_C4[i, 1] = 1;
                    C++;
                    if (C == 8) { C = 0; }
                }
            }
            return C1_C2_C3_C4;
        }

        static bool CheckResultIsOne(byte[,] XYZ, byte[,] C1_C2_C3_C4, int NumberI, int NumberJ) // Проверка результата полинома 
        {
            int Result1 = XYZ[NumberJ, 0] * C1_C2_C3_C4[NumberI, 1];
            int Result2 = XYZ[NumberJ, 1] * C1_C2_C3_C4[NumberI, 2];
            int Result3 = XYZ[NumberJ, 2] * C1_C2_C3_C4[NumberI, 3];
            int Units = Result1 + Result2 + Result3 + C1_C2_C3_C4[NumberI, 0];
            if (Units % 2 == 0 || Units == 0)
            {
                return false;
            }
            return true;
        }
    }
}
