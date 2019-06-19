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
        public MainWindow()
        {
            InitializeComponent();
        }

        private FrameworkElement[,] Matrix;

        private void SizeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
        }

        private void SelectSize_Click(object sender, RoutedEventArgs e)
        {
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
                    if (MatrixRow == MatrixCol && MatrixRow >= 3 && MatrixCol <= 26)    // Если 25 <= (Ряд == Колонка) >= 3
                    {
                        Console.WriteLine($"Введено верно: {MatrixRow}x{MatrixCol}");      // Печатаем их значение
                    }
                    else
                    {
                        if (MatrixRow >= 3 && MatrixRow <= 26) // Если нам подходит ряд
                        {
                            MatrixCol = MatrixRow;
                            Console.WriteLine($"Выбрали ряд: {MatrixRow}x{MatrixCol}");
                        }
                        else if (MatrixCol >= 3 && MatrixCol <= 26) // Если не подходит ряд, но подходит колонка
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
                GenerateMatrix(MatrixRow, MatrixCol);
            }
            // Иначе значение по умолчанию
            else
                GenerateMatrix(5, 5);                
        }

        private void GenerateMatrix(int Rows, int Columns)
        {
            // Очищаем поле
            GridPanel.Children.Clear();
            GridPanel.RowDefinitions.Clear();
            GridPanel.ColumnDefinitions.Clear();

            SizeList.Text = $"{Rows}x{Columns}";
            Matrix = new FrameworkElement[Rows+1, Columns+1];

            for(int i = 0; i<=Rows; i++)
            {
                if (i == 0) // Если первая строка то пишем шапку
                {
                    for (int j = 0; j <= Columns; j++)
                    {
                        // Добавляем колонку
                        GridPanel.ColumnDefinitions.Add(new ColumnDefinition()
                        {
                            Width = new GridLength(1, GridUnitType.Star),
                            Name = $"Col_{i}"
                        });
                        // Добавляем ряд
                        GridPanel.RowDefinitions.Add(new RowDefinition()
                        {
                            Height = new GridLength(1, GridUnitType.Star),
                            Name = $"Row_{i}"
                        });

                        // Добавляем лейбл с буквой
                        var TestLabel = new Label() { Content = $"{(char)('A' + j - 1)}" };
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
                        if(j == 0) // Если первый столбец то добавляем боковую шапку
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
                            // Выделение центрального
                            if (i == j) (Matrix[i, j] as ComboBox).Opacity = 0.5;
                            // Размещение
                            Grid.SetRow(Matrix[i, j], i); /// Устанавливаем ряд от i
                            Grid.SetColumn(Matrix[i, j], j); /// Устанавливаем колонку от j
                            // Добавление события
                            (Matrix[i, j] as ComboBox).SelectionChanged += 
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
    }

    public class MyDropdown : ComboBox
    {
        public int Row { get; set; }
        public int Column { get; set; }
    }

}
