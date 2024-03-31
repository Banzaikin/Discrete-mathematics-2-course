using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinSpanTree
{
    public class Edge
    {
        public int Source { get; set; }
        public int Destination { get; set; }
        public int Weight { get; set; }
    }

    public class KruskalAlgorithm
    {

        // Метод для нахождения минимального остовного дерева
        public List<Edge> FindMinimumSpanningTree(int[,] graph)
        {
            List<Edge> result = new List<Edge>();
            List<Edge> edges = new List<Edge>();
            // Заполняем список рёбер
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = i; j < graph.GetLength(1); j++)
                {
                    if (graph[i, j] > 0)
                    {
                        edges.Add(new Edge { Source = i, Destination = j, Weight = graph[i, j] });
                    }
                }
            }
            // Сортируем рёбра по весу
            edges.Sort((e1, e2) => e1.Weight - e2.Weight);
            int[] parent = new int[graph.GetLength(0)];
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                parent[i] = i;
            }
            foreach (Edge edge in edges)
            {
                int root1 = Find(parent, edge.Source);
                int root2 = Find(parent, edge.Destination);

                if (root1 != root2)
                {
                    result.Add(edge);
                    parent[root1] = root2;
                }
            }
            return result;
        }

        // Метод для поиска корня вершины
        private int Find(int[] parent, int vertex)
        {
            while (vertex != parent[vertex])
            {
                vertex = parent[vertex];
            }
            return vertex;
        }
        // Метод вывода ребер и веса
        public void PrintMST(int[,] arr)
        {
            List<Edge> result = FindMinimumSpanningTree(arr);
            Console.WriteLine("\nМетод Краскала");
            Console.WriteLine("Ребро \tВес");
            int sum = 0;
            foreach (Edge edge in result)
            {
                Console.WriteLine($"{edge.Source} - {edge.Destination} \t {edge.Weight}");
                sum += edge.Weight;
            }
            Console.WriteLine("Суммарный вес: " + sum);
        }
    }
}
