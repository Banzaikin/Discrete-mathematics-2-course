using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator
{
    class Program
    {
        public static List<T> removeDuplicates<T>(List<T> list)
        {
            return new HashSet<T>(list).ToList();
        }
        static void Main(string[] args)
        {
            string[] menu = {
            "\nСписок команд: \n",
            "1. Создать универсум (диапазон)\n",
            "2. Создать множество\n",
            "3. Пересечение\n",
            "4. Объединение\n",
            "5. Дополнение\n",
            "6. Разность\n",
            "7. Симметрическая разность\n",
            "8. Выход из программы\n",
            "Введите команду: "
            };
            bool stop = false; //переменная отвечающая за остановку программы
            int left = 0, right = 0, numMn = 1; // границы диапазона и количество множеств
            int[][] mnog = new int[numMn][]; //множества
            for (; ; )
            {
                //вывод меню
                for (int i = 0; i < menu.Length; i++)
                {
                    Console.WriteLine(menu[i]);
                }
                int command = Convert.ToInt32(Console.ReadLine());
                switch (command)
                {
                    case 1: //Создание универсума (диапазона)
                        Console.WriteLine(" Введите границы диапозона ");
                        Console.WriteLine("Введите левую границу: ");
                        left = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Введите правую границу: ");
                        right = Convert.ToInt32(Console.ReadLine());
                        break;
                    case 2: //Создание множеств
                        Console.WriteLine("Введите количество множеств: ");
                        numMn = Convert.ToInt32(Console.ReadLine());
                        mnog = new int[numMn][];
                        for (int i = 0; i < numMn; i++)
                        {
                            Console.WriteLine("\nВведите размер " + (i + 1) + " множества: ");
                            int sizeMn = Convert.ToInt32(Console.ReadLine());
                            mnog[i] = new int[sizeMn];
                            Console.WriteLine(" \nВведите элементы множества\n ");
                            for (int j = 0; j < sizeMn; j++)
                            {
                                Console.WriteLine("Введите " + (j + 1) + " элемент: ");
                                int number = Convert.ToInt32(Console.ReadLine());
                                //проверка на вхождение в диапазон
                                while (number < left || number > right)
                                {
                                    Console.WriteLine("Число не входит в диапазон - " + "[" + left + ";" + right + "]" + ". Введите ещё раз: ");
                                    number = Convert.ToInt32(Console.ReadLine());
                                }
                                mnog[i][j] = number;
                            }
                        }
                        break;
                    case 3: //Пересечение
                        List<int> list1 = new List<int>();
                        List<int> res3 = new List<int>();
                        //создание листа для сравнения
                        for (int k = 0; k < mnog[0].Length; k++)
                        {
                            list1.Add(mnog[0][k]); 
                        }
                        //сначала сравниваются первые 2 множества и находятся совпадения в них, результат заносится в лист для сравнения
                        //далее 3 множество сравнивается с листом для сравнения и результаты заносятся в лист для сравнения и так далее
                        for (int count = 1; count < numMn; count++)
                        {
                            res3.Clear();
                            for (int i = 0; i < list1.Count; i++)
                            {
                                for (int j = 0; j < mnog[count].Length; j++)
                                {
                                    
                                    if (list1[i] == mnog[count][j])
                                    {
                                        res3.Add(mnog[count][j]);
                                    }
                                }
                            }
                            res3 = removeDuplicates(res3);
                            list1 = res3.ToList();
                        }
                        //вывод множеств и результата пересечения
                        Console.WriteLine("Ваши множества: ");
                        for (int i = 0; i < numMn; i++)
                        {
                            Console.WriteLine("\n" + (i+1) + " множество: ");
                            for (int j = 0; j < mnog[i].Length; j++)
                            {
                                Console.Write(mnog[i][j] + " ");
                            }
                        }
                        Console.WriteLine("\nРезультат выполнения пересечения множеств: ");
                        for (int i = 0; i < res3.Count; i++)
                        {
                            Console.Write(res3[i] + " ");
                        }
                        break;
                    case 4: //Объединение
                        List<int> res4 = new List<int>();
                        //добавление всех элементов в лист
                        for(int i = 0; i < numMn; i++)
                        {
                            for (int j = 0; j < mnog[i].Length; j++)
                            {
                                res4.Add(mnog[i][j]);
                            }
                        }
                        res4 = removeDuplicates(res4);
                        //вывод множеств и результата объединения
                        Console.WriteLine("Ваши множества: ");
                        for (int i = 0; i < numMn; i++)
                        {
                            Console.WriteLine("\n" + (i + 1) + " множество: ");
                            for (int j = 0; j < mnog[i].Length; j++)
                            {
                                Console.Write(mnog[i][j] + " ");
                            }
                        }
                        Console.WriteLine("\nРезультат выполнения объединения множеств: ");
                        for (int i = 0; i < res4.Count; i++)
                        {
                            Console.Write(res4[i] + " ");
                        }
                        break;
                    case 5: //Дополнение
                        //вывод множеств
                        Console.WriteLine("Ваши множества: ");
                        for (int i = 0; i < numMn; i++)
                        {
                            Console.WriteLine("\n" + (i + 1) + " множество: ");
                            for (int j = 0; j < mnog[i].Length; j++)
                            {
                                Console.Write(mnog[i][j] + " ");
                            }
                        }
                        Console.WriteLine("\nВведите какое множество будем дополнять: ");
                        int numAddition = Convert.ToInt32(Console.ReadLine());
                        List<int> res5 = new List<int>();
                        //заполнение листа числами в диапозоне
                        for (int i = left; i <= right; i++)
                        {
                            res5.Add(i);
                        }
                        for(int i = 0; i < mnog[numAddition - 1].Length; i++)
                        {
                            res5.Remove(mnog[numAddition - 1][i]);
                        }
                        //вывод результата дополнения
                        Console.WriteLine("\nРезультат выполнения дополнения множества: ");
                        for (int i = 0; i < res5.Count; i++)
                        {
                            Console.Write(res5[i] + " ");
                        }
                        break;
                    case 6: //Разность
                        //вывод множеств
                        Console.WriteLine("Ваши множества: ");
                        for (int i = 0; i < numMn; i++)
                        {
                            Console.WriteLine("\n" + (i + 1) + " множество: ");
                            for (int j = 0; j < mnog[i].Length; j++)
                            {
                                Console.Write(mnog[i][j] + " ");
                            }
                        }
                        Console.WriteLine("\nВведите из какого множества будем вычитать: ");
                        int numDeductible = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("\nВведите множество-вычитатель: ");
                        int numSubtractor = Convert.ToInt32(Console.ReadLine());
                        List<int> res6 = new List<int>();
                        for (int i = 0; i < mnog[numDeductible - 1].Length; i++)
                        {
                            res6.Add(mnog[numDeductible - 1][i]);
                        }
                        for (int i = 0; i < mnog[numSubtractor - 1].Length; i++)
                        {
                            res6.Remove(mnog[numSubtractor - 1][i]);
                        }
                        //вывод результата разности
                        Console.WriteLine("\nРезультат выполнения разности множеств: ");
                        for (int i = 0; i < res6.Count; i++)
                        {
                            Console.Write(res6[i] + " ");
                        }
                        break;
                    case 7: //Симметрическая разность
                        //вывод множеств
                        Console.WriteLine("Ваши множества: ");
                        for (int i = 0; i < numMn; i++)
                        {
                            Console.WriteLine("\n" + (i + 1) + " множество: ");
                            for (int j = 0; j < mnog[i].Length; j++)
                            {
                                Console.Write(mnog[i][j] + " ");
                            }
                        }
                        Console.WriteLine("\nВведите 2 множества для симметрической разности\n");
                        Console.WriteLine("\nВведите номер 1 множества для симметрической разности: ");
                        int num1Mn = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("\nВведите номер 2 множества для симметрической разности: ");
                        int num2Mn = Convert.ToInt32(Console.ReadLine());
                        List<int> res7 = new List<int>();
                        //добавление всех элементов в лист и удаление повторяющихся элементов
                        for (int j = 0; j < mnog[num1Mn - 1].Length; j++)
                        {
                            int indexElem = res7.IndexOf(mnog[num1Mn - 1][j]);
                            //если такое число уже есть, то удаляем его по индексу 
                            if (indexElem != -1)
                                res7.RemoveAt(indexElem);
                            //если такого числа нет, то добавляем его в конечное множество
                            else
                                res7.Add(mnog[num1Mn - 1][j]);
                        }
                        for (int j = 0; j < mnog[num2Mn - 1].Length; j++)
                        {
                            int indexElem = res7.IndexOf(mnog[num2Mn - 1][j]);
                            //если такое число уже есть, то удаляем его по индексу 
                            if (indexElem != -1)
                                res7.RemoveAt(indexElem);
                            //если такого числа нет, то добавляем его в конечное множество
                            else
                                res7.Add(mnog[num2Mn - 1][j]);
                        }
                        //вывод результата симметрической разности
                        Console.WriteLine("\nРезультат выполнения симметрической разности множеств: ");
                        for (int i = 0; i < res7.Count; i++)
                        {
                            Console.Write(res7[i] + " ");
                        }
                        break;
                    case 8: //Выход из программы
                        stop = true;
                        break;
                }
                //Если была введена команда 8, то цикл прервется
                if (stop)
                {
                    break;
                }
            }
        }
    }
}
