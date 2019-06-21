using System;

namespace Task11
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            /* Подкотовка к операциям кодирования */
            string Path = "../../MatrixFiles/";             // Путь к файлам
            var processor = new MatrixProcessor();          // Создаем объект обработчика матриц
            processor.UploadMatrix(Path+"keyMatrix.txt");   // Инициализируем матрицу - ключ
               
            /* Кодируем сообщение по матрице - ключу */
            /// Кодируем входное сообщение и записываем в файл encodedMessage
            var encodedMatrix = processor.EncodeMessage(FileSystemManager.ReadMessage(Path + "messageToEncode.txt"));
            FileSystemManager.WriteMessage(Path + "encodedMessage.txt", encodedMatrix);           
            /// Читаем закодированную матрицу из файла
            char[,] encodedMatrixFromFile = FileSystemManager.ReadCharMatrix(Path + "encodedMessage.txt");

            /* Декодируем сообщение по матрице - ключу */
            /// Закодированное сообщение из файла
            string encodedMessageFromFile = "";          
            /// Переводим закодированную матрицу в строку
            for (int i = 0; i < 10; i++) 
            {
                for (int j = 0; j < 10; j++)
                {
                    encodedMessageFromFile += encodedMatrixFromFile[i, j];
                }
            }
            /// Записываем раскодированную матрицу в файл
            FileSystemManager.WriteMessage(Path+"decodedMessage.txt", processor.DecodeMessage(encodedMessageFromFile)); // decoding message and writing it in file

        }
    }
}
