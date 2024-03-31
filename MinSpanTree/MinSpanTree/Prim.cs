using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinSpanTree
{
    public class Prim
    {
        private static int size = 10;
        static int MinimumKey(int[] key, bool[] mstSet)
        {
            int min = int.MaxValue, minIndex = 0;

            for (int v = 0; v < size; v++)
            {
                if (mstSet[v] == false && key[v] < min)
                {
                    min = key[v];
                    minIndex = v;
                }
            }

            return minIndex;
        }

        static void PrintMST(int[] parent, int[,] graph)
        {
            Console.WriteLine("\nМетод Прима");
            Console.WriteLine("Ребро \tВес");
            int sum = 0;
            for (int i = 1; i < size; i++)
            {
                Console.WriteLine((parent[i] + 1) + " - " + (i + 1) + "\t " + graph[i, parent[i]]);
                sum += graph[i, parent[i]];
            }
            Console.WriteLine("Суммарный вес: " + sum);
        }

        public void PrimMST(int[,] graph)
        {
            int[] parent = new int[size];
            int[] key = new int[size];
            bool[] mstSet = new bool[size];

            for (int i = 0; i < size; i++)
            {
                key[i] = int.MaxValue;
                mstSet[i] = false;
            }

            key[0] = 0;
            parent[0] = -1;

            for (int count = 0; count < size - 1; count++)
            {
                int u = MinimumKey(key, mstSet);
                mstSet[u] = true;

                for (int v = 0; v < size; v++)
                {
                    if (graph[u, v] != 0 && mstSet[v] == false && graph[u, v] < key[v])
                    {
                        parent[v] = u;
                        key[v] = graph[u, v];
                    }
                }
            }
            PrintMST(parent, graph);
        }
    }
}
