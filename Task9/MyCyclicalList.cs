using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9
{
    public class MyCyclicalList<T> : IEnumerable<T>
    {
        // Структура данных
        private T[] data;

        #region Конструкторы
        // Конструктор без параметров для стандартной емкости
        public MyCyclicalList()
        {
            data = new T[5];
        }
        // Конструктор с параметром для установки изначальной емкости
        public MyCyclicalList(int capacity)
        {
            if (capacity > 0) // Исключительная ситуация3
                data = new T[capacity];
            else
                capacity = 5;
        }
        // Конструктор с параметром для копирования из другого циклического списка
        public MyCyclicalList(MyCyclicalList<T> AnotherList)
        {
            Count = AnotherList.data.Length;
            data = new T[Count];
            AnotherList.data.CopyTo(data, 0);         
        }
        // Перечислитель для конструктора
        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)data).GetEnumerator();
        }
        // Перечислитель для конструктора
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)data).GetEnumerator();
        }
        #endregion

        // Количество элементов списка
        public int Count { get; private set; }

        // Индексатор циклического списка
        public T this[int i]
        {
            get { return data[GetCyclicalIndex(i)]; }
            set { data[GetCyclicalIndex(i)] = value; }
        }

        // Метод получения индекса в циклическом списке
        private int GetCyclicalIndex(int index)
        {
            if ((index % Count == 0) || index < 1)
                return Count - (-index % Count) - 1;
            // Если i превышает количество элементов
            else if (index > Count)
                return (index % Count) - 1;
            // Если i в пределе от 1 до Count
            else
                return index - 1;
        }

        // Добавление элемента в список
        public void Add(T value)
        {
            if (Count >= data.Length)
                Array.Resize<T>(ref data, data.Length * 2);
            data[Count] = value;
            Count++;
        }

        // Поиск элементов
        public List<int> Find(T element)
        {
            List<int> FoundItems = new List<int>();
            for(int i = 0; i < data.Length; i++)
            {
                if (data[i].GetHashCode() == element.GetHashCode()) // Если элементы равны
                    FoundItems.Add(i+1);                         
            }
            return FoundItems; // Возвращаем список, с индексами элементов, которые ищем
        }

        // Удаление элемента по индексу
        public void Remove(int index)
        {
            for (int i = index - 1; i < Count - 1;)
                data[i] = data[++i];
            Count--;
        }

        // Демонстрация списка
        public void Show()
        {
            Console.WriteLine("Выберите способ просмотра списка путем нажатия клавиши:" +
                            "\n[1] - Просмотр отдельных элементов" +
                            "\n[2] - Просмотр элементов в диапазоне индексов" +
                            "\n[0] - Выход без просмотра\n");
            switch (Console.ReadKey(true).KeyChar)
            {
                case '1':
                    {
                        Console.WriteLine("Для выхода необходимо ввести любое значение, не являющееся целым числом");
                        int Input = 0; bool Selected = false;
                        do
                        {
                            Console.Write("Введите индекс элемента для отображения: ");
                            Selected = int.TryParse(Console.ReadLine(), out Input);
                            if (Selected)
                                Console.WriteLine($"Элемент [{Input}]: {this[Input]}\n");
                        }
                        while (Selected);
                        Console.WriteLine("Выход из вывода элементов...\n");
                    }break;
                case '2':
                    {
                        Console.Write("Введите индекс, от которого будут выведены элементы: ");
                        int From = Program.GetInt();
                        Console.Write("Введите индекс, до которого будут выведены элементы: ");
                        int To = Program.GetInt();
                        // Учитываем, что To может быть меньше, чем From, Обрабатываем при помощи тернарного оператора
                        for (int i = From; (From < To) ? (i <= To) : (i >= To); i += (From <= To)?1:-1) 
                            Console.Write($"[{i}]: {this[i]}; ");
                        Console.WriteLine("\nВыход из вывода элементов...\n");
                    }
                    break;
                default:
                    {
                        Console.WriteLine("Выход из вывода элементов...\n");
                    }
                    break;
            }
        }


        #region Сравнение объектов
        public override bool Equals(object obj)
        {
            return obj is MyCyclicalList<T> list &&
                   EqualityComparer<T[]>.Default.Equals(data, list.data) &&
                   Count == list.Count;
        }
        public override int GetHashCode()
        {
            var hashCode = 1264045569;
            hashCode = hashCode * -1521134295 + EqualityComparer<T[]>.Default.GetHashCode(data);
            hashCode = hashCode * -1521134295 + Count.GetHashCode();
            return hashCode;
        }
        #endregion
    }
}
