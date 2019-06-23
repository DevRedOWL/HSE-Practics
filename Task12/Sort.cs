using System;
using System.Collections.Generic;
using System.Text;

namespace Task12
{   
    public class Sort
    {
        public static int RelSelection, RelBlock, CheckSelection, CheckBlock;

        #region Сортировка простым выбором
        public static void Selection(ref int[] arr)
        {
            int min, temp; // Минимальный элемент и пузырек
            int length = arr.Length; // Сокращаем текст

            for (int i = 0; i < length - 1; i++) // Проход по всем элементам массива
            {
                min = i; // Текущий назначаем минимальным

                for (int j = i + 1; j < length; j++) // Еще раз проход по всем элементам массива, начиная со следующего
                {
                    CheckSelection++;
                    if (arr[j] < arr[min]) // Если найденный элемент меньше установленного минимального
                    {
                        RelSelection++;
                        min = j; // Запоминаем найденный как минимальный
                    }
                }

                CheckSelection++;
                if (min != i) // Если мы таки нашли элемент меньше, чем i
                {
                    // Свап пузырьком
                    temp = arr[i]; 
                    arr[i] = arr[min];
                    arr[min] = temp;
                }
            }
        }
        #endregion

        #region Блочная сортировка
        public static void Bucket(ref int[] arr)
        {
            // Предварительная проверка элементов исходного массива
            if (arr == null || arr.Length < 2)
                return;

            // Поиск элементов с максимальным и минимальным значениями
            int maxValue = arr[0];
            int minValue = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] > maxValue)
                    maxValue = arr[i];

                if (arr[i] < minValue)
                    minValue = arr[i];
            }

            // Создание временного массива "карманов" в количестве,
            // достаточном для хранения всех возможных элементов,
            // чьи значения лежат в диапазоне между minValue и maxValue.
            // Т.е. для каждого элемента массива выделяется "карман" List<int>.
            // При заполнении данных "карманов" элементы исходного не отсортированного массива
            // будут размещаться в порядке возрастания собственных значений "слева направо".
            List<int>[] bucket = new List<int>[maxValue - minValue + 1];

            for (int i = 0; i < bucket.Length; i++)
            {
                bucket[i] = new List<int>();
            }

            // Занесение значений в пакеты
            for (int i = 0; i < arr.Length; i++)
            {
                RelBlock++;
                bucket[arr[i] - minValue].Add(arr[i]);
            }

            // Восстановление элементов в исходный массив
            // из карманов в порядке возрастания значений
            int position = 0;
            for (int i = 0; i < bucket.Length; i++)
            {
                CheckBlock++;
                if (bucket[i].Count > 0)
                {
                    for (int j = 0; j < bucket[i].Count; j++)
                    {
                        RelBlock++;
                        arr[position] = bucket[i][j];
                        position++;
                    }
                }
            }
        }
        #endregion

        #region Случайное заполнение двух массивов числами от 0 до 100
        public static void Fill(int Count, ref int[] FirstArray, ref int[] SecondArray)
        {
            Random rand = new Random();
            List<int> IntArray = new List<int>();
            for (int i = 0; i < Count; i++)
                IntArray.Add(i);
            FirstArray = new int[Count];
            SecondArray = new int[Count];
            for (int i = 0; i < Count; i++)
            {
                int TempInt = IntArray[rand.Next(0, IntArray.Count)];
                FirstArray[i] = TempInt; SecondArray[i] = TempInt;
                IntArray.Remove(TempInt);
            }
        }
        #endregion

        #region Демонстрация массива
        public static void Show(string Message, int[] Arr)
        {
            Console.Write($"{Message}: \n{{ ");
            foreach (int b in Arr)
                Console.Write($"{b}; ");
            Console.WriteLine("}\n");
        }
        #endregion
    }
}
