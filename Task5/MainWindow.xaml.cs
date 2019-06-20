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

namespace Task5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // Инициализируем статическую страничку
            InitializeComponent();

            // Рекурсивно добавляем колонки и ряды
            for(int g = 0; g<9; g++)
            {
                // Добавляем колонку
                FieldsContainer.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = new GridLength(1, GridUnitType.Star),
                    Name = $"Col_{g}"
                });

                // Добавляем ряд
                FieldsContainer.RowDefinitions.Add(new RowDefinition()
                {
                    Height = new GridLength(1, GridUnitType.Star),
                    Name = $"Row_{g}"
                });

                for(int i = 0; i<9; i++)
                {
                    // Генерируем имя
                    string Code = $"B_{g}_{i}";
                    // Добавляем в словарь текстовое поле
                    _TextFields.Add(Code, new MyCustomTextField
                    {
                        Text = $"{g + 1}{i + 1}",
                        Style = (Style)this.Resources["MenuBurger"],
                        Name = Code,
                        Foreground = new SolidColorBrush(Color.FromRgb(243, 244, 249)),
                        TextAlignment = TextAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        FontSize = 18,
                        Background = (g == i) ? new SolidColorBrush(Color.FromRgb(41, 53, 65)) : new SolidColorBrush(Color.FromRgb(243, 53, 65))
                    });
                    // Добавляем обработчик нажатия клавиш
                    _TextFields[Code].PreviewTextInput += new TextCompositionEventHandler(IfButtonPressed);
                    // Размещаем элемент
                    Grid.SetColumn(_TextFields[Code], i); // Стобец от итератора
                    Grid.SetRow(_TextFields[Code], g); // Ряд от сетки
                    FieldsContainer.Children.Add(_TextFields[Code]); // Добавляем кнопку на панель
                }

                
            }
                
        }

        #region Тестовый метод для создания и удаления кнопки
        private void TestOfGeneration(object sender, RoutedEventArgs e)
        {

            // Создаем словарь элементов управления
            var controls = new Dictionary<string, FrameworkElement>
            {
                // Добавляем в словарь такую то кнопшку
                ["_B1"] = new Button
                {
                    Content = "Sample text",
                    Name = "BID"
                }
            };

            // Удаляем элемент и устанавливаем заново
            FieldsContainer.Children.Remove(FieldsContainer.Children.OfType<Button>().FirstOrDefault(block => block.Name == "BID")); // .Children[INT]
            FieldsContainer.Children.Add(controls["_B1"]);

            // Устанавливаем кнопке колонку и не только
            Grid.SetColumn(controls["_B1"], new Random().Next(5,8));
            Grid.SetRow(controls["_B1"], new Random().Next(0, 4));
        }
        #endregion

        /** Коллекция элементов управления **/
        private Dictionary<string, FrameworkElement> _TextFields = new Dictionary<string, FrameworkElement>();

        /** Обработка нажатий клавиш **/
        private void IfButtonPressed(object sender, TextCompositionEventArgs e)
        {
            // Сложное условие:  
            // Игнорируем, если (char не цифра и не запятая) или (если вводится запятая но она уже есть) или (мы не на первой странице)
            if ((!Char.IsDigit(e.Text, 0) && e.Text[0] != ',') || ((sender as TextBox).Text.Contains(',') && e.Text[0] == ',') || !ShowGenerated.IsEnabled)
                e.Handled = true;           
            (sender as MyCustomTextField).Text = (sender as MyCustomTextField).Text.Replace(" ", "");       // Устанавливаем текст поля
            
        }

        /** Просмотр изначальной матрицы **/
        private void ShowBaseClick(object sender, RoutedEventArgs e)
        {
            if (!ShowGenerated.IsEnabled) // Если мы были НЕ на первой странице (для оптимизации)
            {
                foreach(MyCustomTextField MCTF in FieldsContainer.Children)
                {
                    MCTF.Text = $"{MCTF.RealValue}";
                    MCTF.ToolTip = null;
                }
            }
            ShowGenerated.IsEnabled = true; // Переключаемся режим изменения матрицы
            MainForm.Title = "Задание 5 | Ввод исходной матрицы";
        }

        /** Просмотр ответа **/
        private void ShowGeneratedClick(object sender, RoutedEventArgs e)
        {
            if (ShowGenerated.IsEnabled) // Если мы были на первой странице
            {

                /* Для начала запоминаем все текущие значения текстовых полей */
                for(int row = 0; row<9; row++) // Проходимся по рядам
                {
                    for (int col = 0; col < 9; col++) // Проходимся по колонкам
                    {
                        // Создаем переменную для названия
                        string ThisFieldName = $"B_{row}_{col}";
                        // Пытаемся запомнить последнее значение
                        try { (_TextFields[ThisFieldName] as MyCustomTextField).RealValue = Convert.ToDouble((_TextFields[ThisFieldName] as MyCustomTextField).Text.Replace(" ", "")); }
                        // Если возникает ошибка - обнуляем
                        catch (Exception) { (_TextFields[ThisFieldName] as MyCustomTextField).RealValue = 0; }                       
                    }
                }

                /** После этого выполняем проверку соответственно условиям **/
                for (int row = 0; row < 9; row++) /// Проходимся по рядам
                {
                    for (int col = 0; col < 9; col++) /// Проходимся по колонкам
                    {
                        /// Создаем переменную для названия
                        string ThisFieldName = $"B_{row}_{col}";
                        /// Если текущий элемент больше, чем элемент в данной строке на главной диагонали
                        if ((_TextFields[ThisFieldName] as MyCustomTextField).RealValue > (_TextFields[$"B_{row}_{row}"] as MyCustomTextField).RealValue)
                            (_TextFields[ThisFieldName] as MyCustomTextField).Text = "1"; /// Выставляем 1
                        else /// Иначе
                            (_TextFields[ThisFieldName] as MyCustomTextField).Text = "0"; /// Выставляем 0
                        /// Добавляем всплывающую подсказку
                        (_TextFields[ThisFieldName] as MyCustomTextField).ToolTip = new ToolTip()
                        {
                            Content = 
                            (col == row) ? $"Элемент на главной диагонали: {(_TextFields[ThisFieldName] as MyCustomTextField).RealValue}" : 
                            $"Текущий элемент: {(_TextFields[ThisFieldName] as MyCustomTextField).RealValue}" +
                            $"\nГлавная диагональ: {(_TextFields[$"B_{row}_{row}"] as MyCustomTextField).RealValue}"
                        };
                    }
                }

                /// Был найден баг, заключающийся в том, что текущее значение последующих элементов неизвестно и остается равным предыдущему, если проходить все в 1 цикле
            }        

            ShowGenerated.IsEnabled = false; // Переключаемся в режим просмотра результата
            MainForm.Title = "Задание 5 | Просмотр результата обработки матрицы";
        }

        /** Клавиша очистки полей **/
        private void ClearClick(object sender, RoutedEventArgs e)
        {
            foreach(MyCustomTextField tb in FieldsContainer.Children)
            {
                tb.Text = ""; // Обнуляем тексты
                tb.ToolTip = null; // Обнуляем тултипы
            }               
            ShowGenerated.IsEnabled = true; // Переключаемся в режим просмотра результата
            MainForm.Title = "Задание 5 | Ввод исходной матрицы";
        }

    }

    /** Класс с дополнительным полем, в которое мы запоминаем последнее значение **/
    public class MyCustomTextField : TextBox
    {
        public double RealValue { get; set; }
    }

}
