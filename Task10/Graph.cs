using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task10
{
    public class Vertex
    {
        // Список стрелок от графа
        private List<int> Arrows = new List<int>();

        // Значение, которое содержит данная вершина
        private readonly int Value = 0;

        // Конструктор
        public Vertex(int value, List<int> arrows)
        {
            Value = value;
            Arrows = arrows;
        }

        // Список всех точек
        public static List<Vertex> VList = new List<Vertex> {  };

        // Метод отображения следований
        public static void ShowArrows()
        {
            foreach (Vertex vv in VList)
            {
                Console.Write($"{vv.Value} > ");
                foreach (int ii in vv.Arrows)
                    Console.Write($"{ii+1}; ");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        // Метод стягивания
        public static void Stretch()
        {
            // Определяем первые вхождения для каждого элемента
            Dictionary<int, int> FirstEntries = new Dictionary<int, int>(); // Значение элемента - Первое вхождение           
            for (int i = 0; i<VList.Count; i++)
            {
                if (!FirstEntries.ContainsKey(VList[i].Value))  // Если еще не входил
                    FirstEntries.Add(VList[i].Value, i);        // Добавляем в список
            }

            Console.WriteLine("\nСписок вершин, в которые будут стянуты схожие:");
            // Выводим первые вхождения
            foreach(KeyValuePair<int, int> kvp in FirstEntries)
                Console.Write($"{kvp.Key}:{kvp.Value+1}; ");

            Console.WriteLine("\n\nСписок изначальных связей вершин:");
            ShowArrows(); // Выводим стрелки

            /// Переназначаем исхящие стрелки в повторяющиеся точки
            for (int i = 0; i < VList.Count; i++)
            {               
                for (int j = 0; j < VList[i].Arrows.Count; j++) // Проходимся по всем стрелкам этой точки
                    // Если индекс точки, в которую идет стрелка не равен первому вхождению элемента с таким же значением
                    if (VList[i].Arrows[j] != FirstEntries[VList[VList[i].Arrows[j]].Value])
                        // ИндексПервогоВхождения[СписокТочек[Значение точки, в которую ведет текущая точка]]
                        VList[i].Arrows[j] = FirstEntries[VList[VList[i].Arrows[j]].Value];              
            }
            Console.WriteLine("Список связей после стягивания стрелок, ведущих в повторяющиеся вершины:");
            ShowArrows(); // Выводим стрелки

            /// Переназначаем повторяющиеся стрелки из одинаковых точек
            for (int i = 0; i < VList.Count; i++)
            {
                if (i != FirstEntries[VList[i].Value]) // Если это точка не первая из точек с таким же значением   
                    for (int j = 0; j < VList[i].Arrows.Count;) // Проходимся по всем стрелкам этой точки
                    {
                        VList[FirstEntries[VList[i].Value]].Arrows.Add(VList[i].Arrows[j]); // Добавляем в элемент первого вхождения
                        VList[i].Arrows.Remove(VList[i].Arrows[j]); // И удаляем из текущей точки данную стрелку
                    }
            }
            Console.WriteLine("Список связей после стягивания стрелок, ведущих из повторяющиеся вершины:");
            ShowArrows(); // Выводим стрелки

            /// Очищаем от мусора
            int buf_i = 0; // Копия итератора, на случай сдвига
            for (int i = 0; i < VList.Count; buf_i++) { 
                List<int> UsedArrows = new List<int>();
                for (int j = 0; j < VList[i].Arrows.Count;) // Проходимся по всем стрелкам этой точки
                {
                    if (VList[i].Arrows[j] == i) // Удаляем петли
                        VList[i].Arrows.Remove(VList[i].Arrows[j]);
                    else if (UsedArrows.Contains(VList[i].Arrows[j])) // Удаляем повторяющиеся пути из точки
                        VList[i].Arrows.Remove(VList[i].Arrows[j]);
                    else
                    {
                        UsedArrows.Add(VList[i].Arrows[j]);
                        j++; // Только здесь инкрементируем итератор[1], т.к. при удалении список сдвигается
                    }                  
                }

                /// Стягиваем повторяющиеся точки в одну
                if (VList[i].Arrows.Count == 0 && FirstEntries[VList[i].Value] != buf_i) // Если это стянутая точка
                    VList.Remove(VList[i]); // Удаляем ее
                else i++; // Только здесь инкрементируем итератор[0], т.к. при удалении список сдвигается
            }
            Console.WriteLine("Список связей после удаления петель и повторных вершин:");
            ShowArrows(); // Выводим стрелки

        }
    }
}
