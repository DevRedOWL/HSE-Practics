using System;
using System.IO;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] dirtycords; int[] cords;
            // Читаем файл
            StreamReader sr = new StreamReader("..\\..\\input.txt");
            cords = new int[Convert.ToInt32(sr.ReadLine().Trim(' '))]; // Задаем строку координат
            dirtycords = sr.ReadLine().Split(' '); // Читаем координаты в string   
            sr.Close();
            #region Old version
            //int thisstreak = 0, laststreak = 10001, FSEL = 0, LSEL = 0;   
            #endregion
            // Проходимся по массиву строк
            for (int i = 0; i < cords.Length; i++)
            {
                // Заводим текущую позицию
                cords[i] = Convert.ToInt32(dirtycords[i]);
                #region Old version
                //// Заводим следующую позицию, если не уперлись в конец массива
                //if (i != cords.Length - 1)
                //    cords[i + 1] = Convert.ToInt32(dirtycords[i + 1]);
                //// Console.WriteLine($"[{i}] {cords[i]} | {thisstreak}");                
                //if (i != cords.Length - 1 && cords[i + 1] - cords[i] == 1) // Если расстояние от следующего до текущего - еденица
                //{
                //    thisstreak++; // Продолжаем стрик
                //}
                //else // Если разрыв
                //{
                //    if (thisstreak != 0 && thisstreak < laststreak) // Если текущий стрик не равен нулю и меньше максимального
                //    {
                //        laststreak = thisstreak;                    // Записываем в максимальный текущий
                //        LSEL = i; FSEL = i - thisstreak;            // Записываем позиции начальной и конечной точек
                //    }
                //    //Console.WriteLine($"..:: Длина текущего отрезка: {thisstreak} ::..");
                //    thisstreak = 0; // Обнуляем текущий стрик
                //}
                #endregion
            }
            // Переменные лучшей минимальной и максимальной точек
            int TopI = cords[0], TopJ = cords[cords.Length-1];
            // Переменная длиннейшего отрезка
            double Eq = 10000;
            // От каждой точки
            for(int i = 0; i < cords.Length; i++)
            {
                // До всех последующих, пока не удовлетворит условию
                for (int j = i+1; j < cords.Length; j++)
                {
                    // Если соотношение меньше Eq
                    if ((cords[j] - cords[i]) / (j-i) < Eq)
                    {
                        Eq = (cords[j] - cords[i]) / (j - i); // Задаем новое eq
                        TopI = cords[i]; TopJ = cords[j]; // Задаем координаты точек
                        j = cords.Length; // Закрываем итерацию i
                    }
                }
            }
            #region Old version
            // Console.WriteLine($"Максимум точек на минимальном отрезке: ({cords[FSEL]}:{cords[LSEL]}) [{laststreak}]");
            // Console.Read();
            #endregion
            StreamWriter sw = new StreamWriter("..\\..\\output.txt");
            #region Old version
            //sw.Write(cords[FSEL] + " " + cords[LSEL]);
            #endregion
            sw.Write(TopI + " " + TopJ);
            sw.Close();
        }
    }
}