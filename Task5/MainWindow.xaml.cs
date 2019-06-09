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
            for(int grid = 0; grid<8; grid++)
            {
                // Добавляем колонку
                FieldsContainer.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = new GridLength(1, GridUnitType.Star),
                    Name = $"Col_{grid}"
                });
                // Добавляем ряд
                FieldsContainer.RowDefinitions.Add(new RowDefinition()
                {
                    Height = new GridLength(1, GridUnitType.Star),
                    Name = $"Row_{grid}"
                });

                // Добавляем кнопку и размещаем где надо
                var d = new Button
                {
                    Content = "Test"
                };
                Grid.SetColumn(d, grid);
                Grid.SetRow(d, grid);
                FieldsContainer.Children.Add(d);
            }
                
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
    }
}
