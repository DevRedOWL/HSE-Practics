using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Конструктор
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Поля класса
        private FrameworkElement[,] Matrix; /// Матрица
        private int LastSelected = 0;       /// Последний выбранный тип матрицы
        #endregion

        #region Действие при изменении размера матрицы
        private void SizeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
        }
        #endregion

        #region Генерация матрицы
        private void GenerateMatrix(int Rows, int Columns, bool Incident)
        {
            // Очищаем поле
            GridPanel.Children.Clear();
            GridPanel.RowDefinitions.Clear();
            GridPanel.ColumnDefinitions.Clear();

            SizeList.Text = $"{Rows}x{Columns}";
            Matrix = new FrameworkElement[Rows + 1, Columns + 1];

            // Добавляем колонки
            for (int co = 0; co <= Columns; co++)
                GridPanel.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = new GridLength(1, GridUnitType.Star),
                    Name = $"Col_{co}"
                });
            // Добавляем ряды
            for (int ro = 0; ro <= Rows; ro++)
                GridPanel.RowDefinitions.Add(new RowDefinition()
                {
                    Height = new GridLength(1, GridUnitType.Star),
                    Name = $"Row_{ro}"
                });

            for (int i = 0; i <= Rows; i++)
            {
                if (i == 0) // Если первая строка то пишем шапку
                {
                    for (int j = 0; j <= Columns; j++)
                    {
                        // Добавляем лейбл с буквой
                        Label TestLabel = null;
                        if (Incident == false) TestLabel = new Label() { Content = $"{(char)('A' + j - 1)}" };
                        else TestLabel = new Label() { Content = $"{j}" };
                        TestLabel.VerticalContentAlignment = VerticalAlignment.Center;
                        TestLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(TestLabel, i); // Стобец от итератора
                        Grid.SetColumn(TestLabel, j); // Ряд от сетки
                        GridPanel.Children.Add(TestLabel);
                    }
                }
                else
                {
                    for (int j = 0; j <= Columns; j++)
                    {
                        if (j == 0) // Если первый столбец то добавляем боковую шапку
                        {
                            // Лейбл с буквой
                            var TestLabel = new Label() { Content = $"{(char)('A' + i - 1)}" };
                            TestLabel.VerticalContentAlignment = VerticalAlignment.Center;
                            TestLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
                            Grid.SetRow(TestLabel, i); // Стобец от итератора
                            Grid.SetColumn(TestLabel, j); // Ряд от сетки
                            GridPanel.Children.Add(TestLabel);
                        }
                        else // Иначе добавляем dropdown
                        {
                            // Добавляем объект собственного типа, так как в исходном не хватает полей
                            Matrix[i, j] = new MyDropdown() { Row = i, Column = j, Name = $"M{i}{j}" }; /// Добавляем кастомные поля
                            (Matrix[i, j] as ComboBox).Items.Add("0"); /// Добавляем поле выбора
                            (Matrix[i, j] as ComboBox).Items.Add("1"); /// Добавляем поле выбора
                            (Matrix[i, j] as ComboBox).SelectedIndex = 0; /// Устанавливаем изначальное значение
                            // Выравнивание
                            (Matrix[i, j] as ComboBox).VerticalContentAlignment = VerticalAlignment.Center;
                            (Matrix[i, j] as ComboBox).HorizontalContentAlignment = HorizontalAlignment.Center;
                            // Выделение центрального, если не матрица инцидентности
                            if (i == j && !Incident) (Matrix[i, j] as ComboBox).Opacity = 0.6;
                            // Размер шрифта
                            (Matrix[i, j] as ComboBox).FontSize = 12;
                            // Размещение
                            Grid.SetRow(Matrix[i, j], i); /// Устанавливаем ряд от i
                            Grid.SetColumn(Matrix[i, j], j); /// Устанавливаем колонку от j
                            // Добавление события, если матрица смежности
                            if (!Incident) (Matrix[i, j] as ComboBox).SelectionChanged +=
                                 new SelectionChangedEventHandler((object sender, SelectionChangedEventArgs args) =>
                                 {
                                     var Sender = (MyDropdown)sender; /// Приводим к типу MyDropdown 
                                                                      /// Устанавливаем соответствующую связь для элемента по другую сторону главной диагонали
                                    (Matrix[Sender.Column, Sender.Row] as ComboBox).SelectedIndex = Sender.SelectedIndex;
                                 });
                            // Добавление в сетку
                            GridPanel.Children.Add(Matrix[i, j]);
                        }
                    }
                }

            }
        }
        #endregion

        #region Выбор матрицы инцеденций
        private void SelectIncedent_Click(object sender, RoutedEventArgs e)
        {
            // Устанавливаем, что выбирали матрицу инциденций
            LastSelected = 1;
            // Делим состояние дропдауна по x 
            string[] SizeString = SizeList.Text.Contains('x') ? SizeList.Text.Split('x') : SizeList.Text.Contains('х') ? SizeList.Text.Split('х') : new [] { "4", "6" };
            // Если разделилось нормально
            if (SizeString.Length == 2)
            {
                int MatrixRow, MatrixCol; // Назначаем переменные
                // Пытаемся получить из строки размер матрицы
                try
                {
                    MatrixRow = Convert.ToInt32(SizeString[0].Replace(" ", ""));    // Делим на ряды
                    MatrixCol = Convert.ToInt32(SizeString[1].Replace(" ", ""));    // И столбцы
                    if (MatrixRow >= 2 && MatrixRow <= 26 && MatrixCol >= 1 && MatrixCol <= 26 && MatrixCol >= MatrixRow - 1 && MatrixCol <= MatrixRow * 0.5 * (MatrixRow - 1))    // Если 25 <= (Ряд == Колонка) >= 3
                    {
                        Console.WriteLine($"Матрица инциденции: {MatrixCol}x{MatrixRow}");      // Печатаем их значение
                        GenerateMatrix(MatrixRow, MatrixCol, true);
                    }
                    // 0.5*число вершин*(число вершин-1)
                    else
                        GenerateMatrix(4, 6, true);
                }
                catch (Exception)
                {
                    GenerateMatrix(4, 6, true);
                }
            }
        }
        #endregion

        #region Выбор матрицы смежности
        private void SelectAdjacency_Click(object sender, RoutedEventArgs e)
        {
            // Устанавливаем, что выбирали матрицу смежности
            LastSelected = 2;
            // Делим состояние дропдауна по x 
            string[] SizeString = SizeList.Text.Contains('x') ? SizeList.Text.Split('x') : SizeList.Text.Contains('х') ? SizeList.Text.Split('х') : new [] { "5", "5" };
            // Если разделилось нормально
            if (SizeString.Length == 2)
            {
                int MatrixRow, MatrixCol; // Назначаем переменные
                // Пытаемся получить из строки размер матрицы
                try
                {
                    MatrixRow = Convert.ToInt32(SizeString[0].Replace(" ", ""));    // Делим на ряды
                    MatrixCol = Convert.ToInt32(SizeString[1].Replace(" ", ""));    // И столбцы
                    if (MatrixRow == MatrixCol && MatrixRow >= 2 && MatrixCol <= 26)    // Если 25 <= (Ряд == Колонка) >= 3
                    {
                        Console.WriteLine($"Введено верно: {MatrixRow}x{MatrixCol}");      // Печатаем их значение
                    }
                    else
                    {
                        if (MatrixRow >= 2 && MatrixRow <= 26) // Если нам подходит ряд
                        {
                            MatrixCol = MatrixRow;
                            Console.WriteLine($"Выбрали ряд: {MatrixRow}x{MatrixCol}");
                        }
                        else if (MatrixCol >= 2 && MatrixCol <= 26) // Если не подходит ряд, но подходит колонка
                        {
                            MatrixRow = MatrixCol;
                            Console.WriteLine($"Выбрали колонку: {MatrixRow}x{MatrixCol}");
                        }
                        else // Если вообще ничего не подходит
                        {
                            MatrixCol = MatrixRow = 5;
                            Console.WriteLine($"Выбрали автоматически: {MatrixRow}x{MatrixCol}");
                        }
                    }
                }
                catch (Exception) // Если было введено левое значение
                {
                    MatrixCol = MatrixRow = 5;
                    Console.WriteLine($"Введены вообще не числа: {MatrixRow}x{MatrixCol}");
                }
                GenerateMatrix(MatrixRow, MatrixCol, false);
            }
            // Иначе значение по умолчанию
            else
                GenerateMatrix(5, 5, false);                
        }
        #endregion       

        #region Выбор проверки графа на дерево
        private void CheckIfTree_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Проверка, является ли граф деревом");
            if (LastSelected == 1) // Если матрица инциденций
            {          
                int[] Conected = new int[Matrix.GetLength(1)];  // Массив количества вершин, соединенных одним ребром (едениц в столбце)

                /// Считаем количество вершин для 1 ребра
                for (int i = 1; i < Matrix.GetLength(0); i++) // Проходимся по строкам
                {
                    for (int j = 1; j < Matrix.GetLength(1); j++) // Проходимся по столбца строк
                    {
                        Console.Write((Matrix[i, j] as MyDropdown).SelectedItem + " "); // Печатаем элемент
                        if ((Matrix[i, j] as MyDropdown).SelectedItem.ToString() == "1") // Если он равен еденице
                            Conected[j]++; // Увеличиваем для данного ребра кол-во вершин
                    }
                    Console.WriteLine(); // Переходим к новой строке
                }
                Console.WriteLine("\nВершин для ребер:");

                /// Проверяем, сколько вершин соединяет каждое ребро (количество едениц в столбце)
                for(int i = 1; i < Conected.Length; i++)
                {
                    Console.Write(Conected[i]); // Печатаем текущую
                    if (Conected[i] != 2) // 
                    {
                        RightConsole.Text = $"Для каждого ребра допустимо ровно 2 точки (Столбец {i})";
                        Console.WriteLine();
                        return;
                    }
                    else
                    {
                        // Поиск в глубину                       
                        RightConsole.Text = "Матрица соответствует требованиям";
                    }                        
                }
                Console.WriteLine();

                /// Заводим, какая вершина куда ведет
                Dictionary<int, List<int>> Connections = new Dictionary<int, List<int>>(); // Задаем словарь Вершина->Список вершин
                for(int i = 1; i<Matrix.GetLength(0); i++) // По строкам
                {
                    Connections[i] = new List<int>(); // Выделяем память под список текущей вершины (строки)
                    for (int j = 1; j < Matrix.GetLength(1); j++) // По столбцам строки
                    {                      
                        if ((Matrix[i, j] as MyDropdown).SelectedItem.ToString() == "1") // Если отсюда ведет ребро
                            for (int ob = 1; ob < Matrix.GetLength(0); ob++) // Проходимся по всем строчкам этого столбца
                                if((Matrix[ob, j] as MyDropdown).SelectedItem.ToString() == "1" && ob != i) // Если в строчке индекс = 1 и это не он
                                    Connections[i].Add(ob); // Добавляем связь                        
                    }

                    // Нет ли соединения между следующим, и тем, что идет в а, словарь списков A: точки, в которые можем попасть и только 1 путь
                    // типа как А-Б-Н => С может быть соеденина только с одной ведущей к А1
                    /// A  1  1  0
                    /// B  0  1  0
                    /// C  1  0  1
                }

                /// Выводим список связей для отладки
                Console.WriteLine("\nПроверяем связи");
                foreach (KeyValuePair<int,List<int>> kvp in Connections)
                {
                    Console.Write((char)('@' + kvp.Key) + ": ");
                    foreach (char val in kvp.Value)
                        Console.Write((char)('@' + val) + ", ");
                    Console.WriteLine();
                }

                /// Передаем методу, проверяющему на связность и наличие циклов список связей
                CheckCyclesAndCoherency(Connections);   

            }
            else if (LastSelected == 2)
            {
                RightConsole.Text = $"В разработке";
            }
        }
        #endregion

        #region Метод проверки на предмет циклов и связности графа, определение, является ли граф деревом **/
        private List<int> Visited = new List<int>();
        private void CheckCyclesAndCoherency(Dictionary<int, List<int>> Connections)
        {
            foreach (KeyValuePair<int, List<int>> kvp in Connections)
                for (int i = 0; i < Connections[kvp.Key].Count; i++)
                    GoingDeep(i, Connections[kvp.Key][i], ref Connections); // Текущий ключ, Ключ в который входим, Коллекция
            
            //Console.WriteLine("\nПроверка связей еще раз");
            //foreach (KeyValuePair<int, List<int>> kvp in Connections)
            //{
            //    Console.Write((char)('@' + kvp.Key) + ": ");
            //    foreach (char val in kvp.Value)
            //        Console.Write((char)('@' + val) + ", ");
            //    Console.WriteLine();
            //}
        }
        private void GoingDeep(int FromKey, int ThisKey, ref Dictionary<int, List<int>> Connections)
        {
            // Перекрестно удаляем ссылки
            Visited.Add(ThisKey); Console.WriteLine((char)('@'+ThisKey)+"-");
            // Идем в глубь
            for (int i = 0; i < Connections[ThisKey].Count; i++)
                if(i != FromKey)
                    GoingDeep(ThisKey, Connections[ThisKey][i], ref Connections);
        }
        #endregion
    }

    /// Собственный класс для матрицы смежности
    public class MyDropdown : ComboBox
    {
        public int Row { get; set; }
        public int Column { get; set; }
    }

}
