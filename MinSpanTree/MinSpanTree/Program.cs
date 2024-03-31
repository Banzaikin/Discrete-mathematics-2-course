using System;
using System.Drawing;

namespace MinSpanTree
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\bayan\source\repos\Discrete-mathematics-2-course\MinSpanTree\g21.txt";
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
                Console.WriteLine("Ваша матрица:\n");
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                    k++;
                    matrix += s + " ";
                }
                int rows = k;
                int columns = rows;
                int count = 0;
                //создание матрицы
                int[,] arr = new int[rows, columns];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        arr[i, j] = (int)Char.GetNumericValue(matrix[count]);
                        count += 2;
                    }
                }
                Prim prim = new Prim();
                prim.PrimMST(arr);
                KruskalAlgorithm kruskal = new KruskalAlgorithm();
                kruskal.PrintMST(arr);
            }
        }
    }
}