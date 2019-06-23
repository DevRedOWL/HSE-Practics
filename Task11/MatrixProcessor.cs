using System;
using System.Collections.Generic;
using System.IO;

namespace Task11
{
    // Класс обработчик матриц
    public class MatrixProcessor 
    {
        #region Поля класса
        public int[,] KeyMatrix; // Массив ключевой матрицы
        #endregion

        #region Метод получения декодированной матрицы
        public char[,] EncodeMessage(string message)
        {
            char[,] outputMessage = new char[KeyMatrix.GetLength(0), KeyMatrix.GetLength(0)];
            int curIndex = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < KeyMatrix.GetLength(0); j++)
                {
                    for (int k = 0; k < KeyMatrix.GetLength(1); k++)
                    {
                        if (KeyMatrix[j, k] == 1)
                        {
                            outputMessage[j, k] = message[curIndex];
                            curIndex++;
                        }
                    }
                }
                RotateKeyMatrix();
            }

            return outputMessage;
        }
        #endregion

        #region Метод загрузки матрицы - ключа
        public void UploadMatrix(string fileName)
        {
            KeyMatrix = FileSystemManager.ReadIntMatrix(fileName);
        }
        #endregion

        #region Метод поворота матрицы - ключа
        public void RotateKeyMatrix()
        {
            var result = new int[KeyMatrix.GetLength(1), KeyMatrix.GetLength(0)];
 
            for (int i = 0; i < KeyMatrix.GetLength(1); i++)
            for (int j = 0; j < KeyMatrix.GetLength(0); j++)
                result[i, j] = KeyMatrix[KeyMatrix.GetLength(0) - j - 1, i];
            KeyMatrix = result;
        }
        #endregion

        #region Метод декодирования сообщения
        public char[,] DecodeMessage(string message)
        {
            char[,] inputMessage = new char[KeyMatrix.GetLength(0), KeyMatrix.GetLength(0)];
            var curIndex = 0;
            for (int i = 0; i < KeyMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < KeyMatrix.GetLength(0); j++)
                {
                    inputMessage[i, j] = message[curIndex];
                    curIndex++;
                }
            }
            

            string outputMessage = "";
            for (int k = 0; k < 4; k++)
            {
                for (int i = 0; i < KeyMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < KeyMatrix.GetLength(0); j++)
                    {
                        if (KeyMatrix[i, j] == 1)
                        {
                            outputMessage += inputMessage[i, j];
                        }
                    }
                }
                RotateKeyMatrix();
            }
            char[,] outputMatrix = new char[KeyMatrix.GetLength(0), KeyMatrix.GetLength(0)];
            curIndex = 0;
            for (int i = 0; i < KeyMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < KeyMatrix.GetLength(0); j++)
                {
                    outputMatrix[i, j] = outputMessage[curIndex];
                    curIndex++;
                }
            }

            return outputMatrix;
        }
        #endregion

        #region Метод генерации ключевой матрицы
        public int[,] GenerateKeyMatrix(int size)
        {
            Random rnd = new Random();
            int[,] newMatrix = new int[size, size];
            List<Tuple<int, int>> possiblePositions = new List<Tuple<int, int>>();
            List<Tuple<int, int>> actualPositions = new List<Tuple<int, int>>();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    possiblePositions.Add(new Tuple<int, int>(i, j));
                }
            }

            for (int i = 0; i < size * size / 4; i++)
            {
                int curPosition = rnd.Next(possiblePositions.Count - 1);
                actualPositions.Add(possiblePositions[curPosition]);
                int itoDelete = possiblePositions[curPosition].Item1;
                int jToDelete = possiblePositions[curPosition].Item2;
                for (int j = 0; j < possiblePositions.Count; j++)
                {
                    int curI = possiblePositions[j].Item1;
                    int curJ = possiblePositions[j].Item2;
                    if (curI == itoDelete & curJ == jToDelete 
                        || curI == itoDelete & curJ == size - jToDelete - 1 
                        || curI == size - itoDelete - 1 & curJ == jToDelete 
                        || curI == size - itoDelete - 1 & curJ == size - jToDelete - 1)
                    {
                        possiblePositions.RemoveAt(j);
                    }
                }
            }

            foreach (Tuple<int,int> actualPosition in actualPositions)
            {
                newMatrix[actualPosition.Item1, actualPosition.Item2] = 1;
            }

            return newMatrix;
        }
        #endregion

        #region Метод демонстрации матрицы из Int
        public void ShowMatrix(int[,] matrix)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (j != 9)
                        Console.Write(matrix[i, j] + " ");
                    else Console.Write(matrix[i, j]);
                }
                if(i != 9)
                    Console.WriteLine();
            }
        }
        #endregion

        #region Метод демонстрации матрицы из Char
        public void ShowMatrix(char[,] matrix)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (j != 9)
                        Console.Write(matrix[i, j] + " ");
                    else Console.Write(matrix[i, j]);
                }
                if(i != 9)
                    Console.WriteLine();
            }
        }
        #endregion
    }

    // Класс, работающий с файловой системой
    public static class FileSystemManager
    {
        #region Поля класса
        private readonly static int _matrixSize = 10; // Размер матрицы
        #endregion

        #region Метод, читающий матрицу Int из файла
        public static int[,] ReadIntMatrix(string filePath)
        {
            int[,] matrix = new int[_matrixSize, _matrixSize];
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                string currFile = streamReader.ReadToEnd();
                string[] strings = currFile.Split('\n');
                int i = 0;
                foreach (string s in strings)
                {
                    var stringWithNums = s.Split(' ');
                    for (int j = 0; j < _matrixSize; j++)
                    {
                        matrix[i, j] = int.Parse(stringWithNums[j]);
                    }
                    i++;
                }
            }
            return matrix;
        }
        #endregion

        #region Метод, читающий матрицу Char из файла
        public static char[,] ReadCharMatrix(string filePath)
        {
            char[,] matrix = new char[_matrixSize, _matrixSize];
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                string currFile = streamReader.ReadToEnd();
                string[] strings = currFile.Split('\n');
                int i = 0;
                foreach (string s in strings)
                {
                    var stringWithNums = s.Split(' ');
                    for (int j = 0; j < _matrixSize; j++)
                    {
                        matrix[i, j] = stringWithNums[j][0];
                    }
                    i++;
                }
            }
            return matrix;
        }
        #endregion

        #region Метод, читающий сообщение, которое необходимо закодировать
        public static string ReadMessage(string filePath)
        {
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                return streamReader.ReadToEnd();
            }
        }
        #endregion

        #region Метод, записывающий сообщение в файл
        public static void WriteMessage(string filePath, char[,] matrix)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (j != 9)
                            streamWriter.Write(matrix[i, j] + " ");
                        else streamWriter.Write(matrix[i, j]);
                    }

                    if (i != 9)
                        streamWriter.WriteLine();
                }
            }
        }
        #endregion
    }
}