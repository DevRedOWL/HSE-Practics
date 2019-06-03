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
            using (StreamReader sr = new StreamReader("INPUT.TXT")) //..\\..\\
            {
                cords = new int[Convert.ToInt32(sr.ReadLine().Trim(' '))]; // Задаем строку координат
                dirtycords = sr.ReadLine().Split(' '); // Читаем координаты в string                                     
            }           
            int thisstreak = 0, laststreak = 30001, FSEL = 0, LSEL = 0;
            // Проходимся по массиву строк
            for (int i = 0; i < cords.Length; i++)
            {
                // Заводим текущую позицию
                cords[i] = Convert.ToInt32(dirtycords[i]);
                // Заводим следующую позицию, если не уперлись в конец массива
                if(i != cords.Length - 1)
                    cords[i+1] = Convert.ToInt32(dirtycords[i+1]);
                //Console.WriteLine($"[{i}] {cords[i]} | {thisstreak}");                
                if (i != cords.Length - 1 && cords[i + 1] - cords[i] == 1) // Если расстояние от следующего до текущего - еденица
                {                    
                    thisstreak++; // Продолжаем стрик
                }
                else // Если разрыв
                {
                    if (thisstreak != 0 && thisstreak < laststreak) // Если текущий стрик не равен нулю и меньше максимального
                    {
                        laststreak = thisstreak;                    // Записываем в максимальный текущий
                        LSEL = i; FSEL = i - thisstreak;            // Записываем позиции начальной и конечной точек
                    }
                    //Console.WriteLine($"..:: Длина текущего отрезка: {thisstreak} ::..");
                    thisstreak = 0; // Обнуляем текущий стрик
                }
                                   
            }
            // Console.WriteLine($"Максимум точек на минимальном отрезке: ({cords[FSEL]}:{cords[LSEL]}) [{laststreak}]");
            // Console.Read();
            using (StreamWriter sw = new StreamWriter("OUTPUT.TXT")) // ..\\..\\
            {
                sw.WriteLine($"{cords[FSEL]} {cords[LSEL]}");
            }
        }
    }
}
