using System;
using System.IO;
using System.Text;

namespace Method_Kvaina
{
    class Program
    {
        static string TF(int value)
        {
            if (value == 1)
                return "";
            else
                return "!";
        }
        static string CreateSDNF (string s)
        {
            string sdnf = "";
            Console.WriteLine("Таблица истинности: ");
            int count = 0;
            int size_sdnf = 0;
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
                                Console.Write(x + " " + y + " " + z + " " + d + " " + s[count]);
                            if (s[count] == '1')
                                sdnf = string.Concat(sdnf, TF(x) + "x" + TF(y) + "y" + TF(z) + "z" + TF(d) + "d U ");  
                            Console.Write("\n");
                            count++;
                        }
                    }
                }
            }
            
            return sdnf;
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
                string sdnf = CreateSDNF(s);
                //узнаем кол-во элементов в СДНФ
                int size_sdnf = 0;
                for (int i = 0; i < sdnf.Length; i++)
                {
                    if (sdnf[i] == 'U')
                        size_sdnf++;
                }
                //проверка на пустую функцию
                if (size_sdnf == 0)
                {
                    Console.WriteLine("Функция пустая!");
                    return;
                }
                //создание массива элементов СДНФ
                string[] arr_sdnf = new string[size_sdnf];
                int a = 0;
                int count_arr = 0;
                for (int i = 0; i < sdnf.Length; i++)
                {
                    if (sdnf[i] == 'U')
                    {
                        arr_sdnf[a] = sdnf.Substring(count_arr, i - count_arr);
                        Console.WriteLine(arr_sdnf[a]);
                        count_arr = i + 2;
                        a++;
                    }
                }
                sdnf = sdnf.Remove(sdnf.Length - 2, 1);
                Console.WriteLine("\nСДНФ: ");
                Console.WriteLine(sdnf);
                
            }

        }
    }
}
