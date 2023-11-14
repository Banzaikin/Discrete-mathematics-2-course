using System;
using System.IO;
using System.Text;

namespace Method_Kvaina
{
    class Program
    {
        static void TableTrue (string s)
        {
            Console.WriteLine("Таблица истинности: ");
            int count = 0;
            Console.WriteLine("x y z d f");
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    for (int z = 0; z < 2; z++)
                    {
                        for (int d = 0; d < 2; d++)
                        {
                            for (int i = 0; i < 1; i++)
                            {
                                Console.Write(x + " " + y + " " + z + " " + d + " " + s[count]);
                            }
                            Console.Write("\n");
                            count++;
                        }
                        
                    }
                }
            }
        }
        static int[,] CreateCartCarno (string vec, int size)
        {
            int[,] arr = new int[size, size];
            int count = 0;
            for (int i = 0; i < size; i++)
            {
                if (i == 2)
                    count = 12;
                if (i == 3)
                    count = 8;
                for (int j = 0; j < size; j++)
                {
                    if (j == 2)
                    {
                        for (j = size - 1; j >= 2; j--)
                        {
                            arr[i, j] = vec[count] - '0';
                            count++;
                        }
                        break;
                    }
                    else
                    {
                        arr[i, j] = vec[count] - '0';
                        count++;
                    }
                }      
            }
            Console.WriteLine("Карта Карно: ");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    Console.Write(arr[i, j] + " ");
                Console.Write("\n");
            }
            return arr;
        }
        static void Main(string[] args)
        {
            string path = @"C:\Users\trajt\source\repos\Discrete-mathematics-2-course\Method_Kvaina\My_file.txt";
            if (!File.Exists(path))
            {
                // Создание файла
                using (StreamWriter sw = File.CreateText(path))
                {
                    Console.WriteLine("Файл пустой. Введите функцию: ");
                    sw.WriteLine(Console.ReadLine());
                }
            }

            // Открытие файла для чтения
            using (StreamReader sr = File.OpenText(path))
            {
                string s = sr.ReadLine();
                Console.WriteLine("Ваша функция: " + s);
                if (s.Length % 4 != 0)
                {
                    Console.WriteLine("Ваша функция не соответствует заявленным требованиям! ");
                    return;
                }
                int size = (int)(Math.Sqrt(s.Length));
                int[,] arr = new int[size, size];
                arr = CreateCartCarno(s, size);
                TableTrue(s);
            }

        }
    }
}
