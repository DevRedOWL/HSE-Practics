using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] dirtycords; int[] cords;
            // Читаем файл
            using (StreamReader sr = new StreamReader("..\\..\\INPUT.TXT"))
            {
                cords = new int[Convert.ToInt32(sr.ReadLine().Trim(' '))]; // Задаем строку координат
                dirtycords = sr.ReadLine().Split(' '); // Читаем координаты в string                                     
            }           
            int thisstreak = 0, laststreak = 30000, FSEL = 0, LSEL = 0;
            // Проходимся по файлу и назначаем элементам int
            for (int i = 0; i < cords.Length; i++)
            {
                cords[i] = Convert.ToInt32(dirtycords[i]);
                Console.WriteLine($"[{i}] {cords[i]} | {thisstreak}");
                //
                if (i != cords.Length - 1 && cords[i + 1] - cords[i] == 1) // Если расстояние от следующего до текущего - еденица
                {                    
                    thisstreak++; // Продолжаем стрик
                    Console.WriteLine($"Я продолжаю, на основании того, что {cords[i]} минус {cords[i - 1]} это еденица");
                }
                else // Если разрыв
                {
                    if (thisstreak != 0 && thisstreak < laststreak) // Если текущий стрик не равен нулю и меньше максимального
                    {
                        laststreak = thisstreak;                    // Записываем в максимальный текущий
                        LSEL = i; FSEL = i - thisstreak;            // Записываем позиции всего
                    }
                    Console.WriteLine($"--------------{thisstreak}--------------");
                    thisstreak = 0;
                }
                                   
            }
            Console.WriteLine($"Максимум точек на минимальном отрезке: {FSEL}-{LSEL} [{laststreak}]");
            //    

            Console.Read();
        }
    }
}
