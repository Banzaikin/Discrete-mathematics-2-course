using coloring_graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coloring_graphs
{
    public class Program
    {
        readonly static string path = @"C:\Users\bayan\source\repos\Discrete-mathematics-2-course\coloring_graphs\g33.txt";
        static int countVertex;
        static int[][]? matrix;
        static List<Vertex> masVertex = new();
        public static void Main()
        {
            //считывание матрицы
            string text = ReadFile();
            ParseText(text);
            Console.WriteLine("Ваша матрица:\n");
            PrintMatrix();

            //Парсинг вершин
            MatrixToList();

            //Раскраска
            Coloring();

            //Вывод результата
            Console.WriteLine();
            Console.WriteLine("Индекс | Цвет ");
            for (int i = 0; i < countVertex; i++)
            {
                Console.WriteLine($"Вершина {masVertex[i].Number} - Цвет {masVertex[i].Color}");
            }
        }
        //Проверка на соседей определённого цвета color
        public static bool NotColorNeighbors(int color, List<int> neighbors, int countVertex, List<Vertex> masVertex)
        {
            for (int j = 0; j < countVertex; j++)
                if (neighbors.Contains(masVertex[j].Number) && masVertex[j].Color == color)
                    return false;
            return true;
        }
        //Печать матрицы
        public static void PrintMatrix()
        {
            if (matrix != null)
            {
                for (int i = 0; i < matrix.Length; i++)
                {
                    for (int j = 0; j < matrix.Length; j++)
                        Console.Write(matrix[i][j] + " ");
                    Console.WriteLine();
                }
            }
        }
        //Выбор нераскрашенного элемента с максимальным количеством связей
        public static int MaxRowNumber()
        {
            int maxRow = 0;
            if (matrix != null)
            {
                for (int i = 0; i < matrix.Length; i++)
                    if (matrix[i].Sum() > maxRow)
                        maxRow = i;
            }
            return maxRow;
        }
        //Чтение файла
        public static string ReadFile()
        {
            if (File.Exists(path))
            {
                string text = File.ReadAllText(path);
                return text;
            }
            return "";
        }
        //Заполнение матрицы
        public static void ParseText(string text)
        {
            string[] lines = text.Split('\n');
            string[] rowF = lines[0].Split(' ');
            countVertex = rowF.Length;
            matrix = new int[countVertex][];
            for (int i = 0; i < countVertex; i++)
            {
                matrix[i] = lines[i].Split(' ').Select(x => int.Parse(x)).ToArray();
                for (int j = 0; j < matrix[i].Length; j++)
                    if (matrix[i][j] > 1) matrix[i][j] = 1;
            }
        }
        //выбор вершин из матрицы
        public static void MatrixToList()
        {
            if (matrix != null)
            {
                for (int i = 0; i < countVertex; i++)
                {
                    int numerRow = MaxRowNumber();
                    List<int> neighbors = new();
                    for (int j = 0; j < matrix[numerRow].Length; j++)
                        if (matrix[numerRow][j] == 1)
                        {
                            neighbors.Add(j);
                            matrix[numerRow][j] = 0;
                        }
                    masVertex.Add(new Vertex(numerRow, neighbors));
                }
            }
        }
        //Раскраска
        public static void Coloring()
        {
            int useColor = 1;
            for (int i = 0; i < countVertex; i++)
            {
                if (masVertex[i].Color == 0)
                {
                    masVertex[i].Color = useColor;
                    for (int j = 0; j < countVertex; j++)
                        if (masVertex[j].Color == 0 && NotColorNeighbors(useColor, masVertex[j].Neighbors, countVertex, masVertex))
                        {
                            masVertex[j].Color = useColor;
                        }
                    useColor++;
                }
            }
        }
    }
}