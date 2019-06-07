using System;
using System.IO;

namespace Task2
{
    class Program
    {
        static int Ways = 0;

        static void Main(string[] args)
        {
            // Задаем количество устройств
            int Devices = 0;

            // Читаем из файла
            StreamReader sr = new StreamReader("..\\..\\input.txt");
            Devices = Convert.ToInt32(sr.ReadLine().Trim(' ')); 
            sr.Close();

            // Выполняем рекурсивную функцию
            if (Devices != 3)
                RecFunc(Devices);
            else Ways++;

            // Пишем в файл
            StreamWriter sw = new StreamWriter("..\\..\\output.txt");
            sw.Write(Ways);
            sw.Close();
        }

        // Рекурсивная функция
        static void RecFunc(int devs)
        {
            // Если устройств не 3
            if (devs > 3)
                switch (devs/2==0)
                {
                    // Если четное 
                    case true:
                        {
                            RecFunc(devs / 2);
                            RecFunc(devs / 2);
                        }
                        break;
                    // Если нечетное
                    case false:
                        {
                            RecFunc(devs / 2);
                            RecFunc((devs / 2) + 1);
                        }
                        break;
                }
            else if (devs == 3) Ways++;

        }

    }
}
