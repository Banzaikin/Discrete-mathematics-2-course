using System;
using System.IO;
using System.Text;

namespace matrix_properties
{
    class Program
    {
        // Функция для проверки рефлексивности
        static void IsReflex(string matrix, int columns, int b)
        {
            int a = 0;
            for (int i = 0; i < matrix.Length; i += 2 * columns + 2)
            {
                if (matrix[i] == '1')
                    a++;
                else if (matrix[i] == '0')
                    b++;
            }
            if (a == columns)
                Console.WriteLine("Рефлексивность");
            if (b == columns)
                Console.WriteLine("Антирефлексивность");
        }
        // Функция для проверки симметрии
        static void IsSymmetric(int[,] matrix, int columns, int b)
        {
            int a = 0;
            bool isAntis = false;
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (matrix[i, j] != matrix[j, i])
                    {
                        a++;
                    }
                    if (matrix[i, j] == 1 && matrix[j, i] != 0)
                        isAntis = true;
                }
                if (a == 1)
                    break;
            }
            if (a == 0)
            {
                if (b != columns)
                    Console.WriteLine("Симметричность");
            }
            if (isAntis)
                Console.WriteLine("Антисимметричность");
            if (a >= columns * columns - columns - 1)
                Console.WriteLine("Асимметричность");
        }
        //функция для проверки транзитивности
        static bool IsTransitive(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        for (int k = 0; k < n; k++)
                        {
                            if (matrix[j, k] == 1 && matrix[i, k] != 1)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        // Функция для проверки полноты
        static bool IsComplete(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if ((i != j) && (matrix[i, j] == 0) && (matrix[j, i] == 0))
                        return false;
                }
            }
            return true;
        }
        static void Main(string[] args)
        {

            string path = @"C:\Users\trajt\source\repos\Discrete-mathematics-2-course\matrix_properties\m3.txt";
            if (!File.Exists(path))
            {
                // Создание файла
                using (StreamWriter sw = File.CreateText(path))
                {
                    Console.WriteLine("Введите количество столбцов: ");
                    int numRow = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Введите элементы матрицы: ");
                    for (int i = 0; i < numRow; i++)
                        sw.WriteLine(Console.ReadLine());
                }
            }

            // Открытие файла для чтения
            using (StreamReader sr = File.OpenText(path))
            {
                string s;
                string matrix = "";
                int k = 0;
                //вывод матрицы + создание строки матрицы
                Console.WriteLine("Ваша матрица: ");
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                    k++;
                    matrix += s + " ";
                }
                int rows = k;
                int columns = rows;
                int a = 0;
                int b = 0;
                //создание матриц
                int[,] arr = new int[rows, columns];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        arr[i, j] = (int)Char.GetNumericValue(matrix[a]);
                        a += 2;
                    }
                }
                //вывод свойств матрицы
                IsReflex(matrix, columns, b);
                IsSymmetric(arr, columns, b);
                if (IsTransitive(arr) == true)
                    Console.WriteLine("Транзитивность");
                if (IsComplete(arr) == true)
                    Console.WriteLine("Полнота");
            }
        }
    }
}
