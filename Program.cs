using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace calculator
{
    class Program
    {
        //функция для удаления повторяющихся элементов
        public static List<T> RemoveDuplicates<T>(List<T> list)
        {
            return new HashSet<T>(list).ToList();
        }

        //проверка на ввод числа
        static int InputInt(string message)
        {
            int number;
            Console.Write(message);
            if (!int.TryParse(Console.ReadLine(), out number))
            {
                do
                {
                    Console.WriteLine("Ошибка!");
                    Console.WriteLine("Введите целое число!");
                    Console.Write(message);
                } while (!int.TryParse(Console.ReadLine(), out number));
            }
            return number;
        }

        //пересечение множеств
        public static List<int> IntersectSets(List<int> mnog1, List<int> mnog2)
        {
            List<int> res = new List<int>();
            for (int i = 0; i < mnog1.Count; i++)
            {
                for (int j = 0; j < mnog2.Count; j++)
                {
                    if (mnog1[i] == mnog2[j])
                        res.Add(mnog1[i]);
                }
            }
            res = RemoveDuplicates(res);
            return res;
        }

        //объединение множеств
        public static List<int> MergeSets(List<int> mnog1, List<int> mnog2)
        {
            List<int> res = new List<int>();
            for (int i = 0; i < mnog1.Count; i++)
                res.Add(mnog1[i]);
            for (int i = 0; i < mnog2.Count; i++)
                res.Add(mnog2[i]);
            res = RemoveDuplicates(res);
            return res;
        }

        //дополнить множества
        public static List<int> ComplementSets(List<int> mnog, int left, int right)
        {
            List<int> res = new List<int>();
            //заполнение листа числами в диапозоне
            for (int i = left; i <= right; i++)
            {
                res.Add(i);
            }
            for (int i = 0; i < mnog.Count; i++)
            {
                res.Remove(mnog[i]);
            }
            return res;
        }

        //разность множеств
        public static List<int> SubtractSets(List<int> mnog1, List<int> mnog2)
        {
            List<int> res = new List<int>();
            for (int i = 0; i < mnog1.Count; i++)
            {
                res.Add(mnog1[i]);
            }
            for (int i = 0; i < mnog2.Count; i++)
            {
                res.Remove(mnog2[i]);
            }
            return res;
        }

        //симметрическая разность множеств
        public static List<int> SubtractSymmetricallySets(List<int> mnog1, List<int> mnog2)
        {
            List<int> res = new List<int>();
            //добавление всех элементов в лист и удаление повторяющихся элементов
            for (int j = 0; j < mnog1.Count; j++)
            {
                int indexElem = res.IndexOf(mnog1[j]);
                //если такое число уже есть, то удаляем его по индексу 
                if (indexElem != -1)
                    res.RemoveAt(indexElem);
                //если такого числа нет, то добавляем его в конечное множество
                else
                    res.Add(mnog1[j]);
            }
            for (int j = 0; j < mnog2.Count; j++)
            {
                int indexElem = res.IndexOf(mnog2[j]);
                //если такое число уже есть, то удаляем его по индексу 
                if (indexElem != -1)
                    res.RemoveAt(indexElem);
                //если такого числа нет, то добавляем его в конечное множество
                else
                    res.Add(mnog2[j]);
            }
            return res;
        }

        //функция вывода множеств 
        public static void OutputMnog(int[][] mnog, int numMn)
        {
            char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            Console.WriteLine("Ваши множества: ");
            for (int i = 0; i < numMn; i++)
            {
                Console.WriteLine("\n" + alpha[i] + " множество: ");
                for (int j = 0; j < mnog[i].Length; j++)
                {
                    Console.Write(mnog[i][j] + " ");
                }
            }
            Console.Write("\n");
        }

        //функция вывода результата операции 
        public static void OutputResult(List<int> res)
        {
            Console.WriteLine("\nРезультат выполнения операции над множествами: ");
            for (int i = 0; i < res.Count; i++)
            {
                Console.Write(res[i] + " ");
            }
        }

        //функция обработки строки
        public static List<int> UnidentifiedExpression(int[][] mnog, string input, int left, int right)
        {
            List<int> res = new List<int>();
            List<int> possitions = new List<int>();
            List<int> A1 = new List<int>();
            List<int> A2 = new List<int>();
            List<int> res1 = new List<int>();
            List<int> B1 = new List<int>();

            input = input.Replace('A', '0');
            input = input.Replace('B', '1');
            input = input.Replace('C', '2');
            input = input.Replace('D', '3');
            input = input.Replace('E', '4');
            input = input.Replace('F', '5');
            input = input.Replace('G', '6');

            int k = 0;
            input = input + 'b' + 'b' + 'b';
            string expression = input;
            if (input[0] != '(' && input[0] != 'd')
            {
                for (int j = 0; j < mnog[Int32.Parse(input[0].ToString())].Length; j++)
                    A1.Add(mnog[Int32.Parse(input[0].ToString())][j]);
                res = A1;
            }
            if (input[0] == 'd')
            {
                for (int j = 0; j < mnog[Int32.Parse(input[1].ToString())].Length; j++)
                    A1.Add(mnog[Int32.Parse(input[1].ToString())][j]);
                res = ComplementSets(A1, left, right);
            }
            try
            {
                foreach (var i in input)
                {
                    if (i == 'b')
                        break;
                    if (i == '(')
                        possitions.Add(k);

                    if (i == ')')
                    {
                        k++;
                        expression = input.Substring(possitions[possitions.Count - 1], k - possitions[possitions.Count - 1]);
                        if (A2.Count == 0)
                        {
                            for (int x = 0; x < mnog[Int32.Parse(expression[1].ToString())].Length; x++)
                                A2.Add(mnog[Int32.Parse(expression[1].ToString())][x]);
                        }
                        int k1 = 0;
                        foreach (var j in expression)
                        {
                            if (j == '-' || j == '+' || j == '/' || j == 's' || j == 'd')
                            {
                                
                                if (expression[k1 + 2] != ')' && expression[k1 + 2] != 'd')
                                {
                                    for (int x = 0; x < mnog[Convert.ToInt32(Int32.Parse(expression[k1 + 2].ToString()))].Length; x++)
                                        B1.Add(mnog[Int32.Parse(expression[k1 + 2].ToString())][x]);
                                }
                                if (j == '-')
                                    A2 = IntersectSets(A2, B1);
                                if (j == '+')
                                    A2 = MergeSets(A2, B1);
                                if (j == '/')
                                    A2 = SubtractSets(A2, B1);
                                if (j == 's')
                                    A2 = SubtractSymmetricallySets(A2, B1);
                                if (i == 'd')
                                {
                                    B1.Clear();
                                    for (int x = 0; x < mnog[Int32.Parse(expression[k1 + 1].ToString())].Length; x++)
                                        B1.Add(mnog[Int32.Parse(expression[k1 + 1].ToString())][x]);
                                    A2 = ComplementSets(B1, left, right);
                                }
                                B1.Clear();
                            }
                            k1++;
                        }
                        res1 = A2;
                        if (input[possitions[possitions.Count - 1] - 2] == '-' || input[possitions[possitions.Count - 1] - 2] == '+' ||
                            input[possitions[possitions.Count - 1] - 2] == '/' || input[possitions[possitions.Count - 1] - 2] == 's' ||
                            input[possitions[possitions.Count - 1] - 2] == 'd')
                        {
                        if (input[possitions[possitions.Count - 1] - 2] == '-')
                                res = IntersectSets(res, res1);
                            if (input[possitions[possitions.Count - 1] - 2] == '+')
                                res = MergeSets(res, res1);
                            if (input[possitions[possitions.Count - 1] - 2] == '/')
                                res = SubtractSets(res, res1);
                            if (input[possitions[possitions.Count - 1] - 2] == 's')
                                res = SubtractSymmetricallySets(res, res1);
                            if (input[possitions[possitions.Count - 1] - 2] == 'd')
                                res = ComplementSets(res1, left, right);
                        }
                        possitions.RemoveAt(possitions.Count - 1);
                        if (input[k + 2] == 'b')
                        {
                            A1 = res;
                            break;
                        }   
                        if (input[k + 2] == '-' || input[k + 2] == '+' || input[k + 2] == '/' || input[k + 2] == 's' || input[k + 2] == 'd')
                            A1 = res;
                    }

                    if (possitions.Count == 0 && input[k + 2] != '(' && input[k + 2] != 'd')
                    {
                        if (i == '-' || i == '+' || i == '/' || i == 's' || i == 'd')
                        {
                            if (A1.Count == 0)
                            {
                                for (int j = 0; j < mnog[Int32.Parse(expression[0].ToString())].Length; j++)
                                    A1.Add(mnog[Int32.Parse(expression[0].ToString())][j]);
                            }
                            for (int j = 0; j < mnog[Int32.Parse(expression[k + 2].ToString())].Length; j++)
                                B1.Add(mnog[Int32.Parse(expression[k + 2].ToString())][j]);
                            if (i == '-')
                                A1 = IntersectSets(A1, B1);
                            if (i == '+')
                                A1 = MergeSets(A1, B1);
                            if (i == '/')
                                A1 = SubtractSets(A1, B1);
                            if (i == 's')
                                A1 = SubtractSymmetricallySets(A1, B1);
                            if (i == 'd')
                            {
                                B1.Clear();
                                for (int j = 0; j < mnog[Int32.Parse(expression[k + 1].ToString())].Length; j++)
                                    B1.Add(mnog[Int32.Parse(expression[k + 1].ToString())][j]);
                                A1 = ComplementSets(B1, left, right);
                            }
                            B1.Clear();
                        }
                    }
                    k++;
                    res = A1;
                }
            }
            catch
            {
                Console.WriteLine("Возникли ошибки!");
            }
            return res;
        }

        //------------------------------------------------------------------//
        static void Main(string[] args)
        {
            string[] menu = {
            "\n\nСписок команд: \n",
            "1. Создать универсум (диапазон)\n",
            "2. Создать множество\n",
            "3. Ввести строку\n",
            "4. Выход из программы\n",
            };
            bool stop = false; //переменная отвечающая за остановку программы
            int left = 0, right = 0;
            int numMn = 1; // границы диапазона и количество множеств
            int[][] mnog = new int[numMn][]; //множества
            for (; ; )
            {
                //вывод меню
                for (int i = 0; i < menu.Length; i++)
                {
                    Console.WriteLine(menu[i]);
                }
                int command = InputInt("Введите команду: ");
                Console.Write("\n");
                switch (command)
                {
                    case 1: //Создание универсума (диапазона)
                        Console.WriteLine(" Введите границы диапозона ");
                        left = InputInt("Введите левую границу: ");
                        right = InputInt("Введите правую границу: ");
                        if  (right <= left)
                        {
                            Console.WriteLine("Правая граница должны быть больше левой! Ваша левая граница: " + left);
                            right = InputInt("Введите правую границу, большую левой границы: ");
                        }
                        break;
                    case 2: //Создание множеств
                        if (left == 0 && right == 0)
                        {
                            Console.WriteLine("Вы не задали универсум, пожалуйста, задайте универсум (1 команда)");
                            break;
                        }
                        numMn = InputInt("Введите количество множеств: ");
                        mnog = new int[numMn][];

                        //цикл перебора всех множеств
                        for (int i = 0; i < numMn; i++)
                        {
                            //выбор способа задания множества
                            Console.WriteLine("\nВведите каким способом задаем множество (1.условием/2.перечислением/3.случайно): ");
                            string method = Console.ReadLine();
                            //задание множества перечислением
                            if (method == "перечислением" || method == "2")
                            {
                                int sizeMn = InputInt("\nВведите размер " + (i + 1) + " множества: ");
                                mnog[i] = new int[sizeMn];
                                Console.WriteLine(" \nВведите элементы множества\n ");
                                for (int j = 0; j < sizeMn; j++)
                                {
                                    int number = InputInt("Введите " + (j + 1) + " элемент: ");
                                    //проверка на вхождение в диапазон
                                    while (number < left || number > right)
                                        number = InputInt("Число не входит в диапазон - " + "[" + left + ";" + right + "]" + ". Введите ещё раз: ");
                                    mnog[i][j] = number;
                                }
                            }
                            //задание множества условием
                            if (method == "условием" || method == "1")
                            {
                                Console.WriteLine("\nВведите условие (1.четность/2.нечетность/3.кратность: ");
                                string conditions = Console.ReadLine();
                                //промежуточный список для занесения туда всех элементов из диапазона, удовлетворяющих условию
                                List<int> listInd = new List<int>();
                                //задание множества из четных элементов
                                if (conditions == "1" || conditions == "четность")
                                {
                                    //поиск 1 элемента, удовлетворяющего условию
                                    int elem = 0;
                                    if (left % 2 == 0)
                                        elem = left;
                                    if (left % 2 != 0)
                                        elem = left + 1;
                                    int k = 0;
                                    //добавление всех элементов в список
                                    while (elem <= right)
                                    {
                                        listInd.Add(elem);
                                        k++;
                                        elem += 2;
                                    }
                                    //задание размера массива и перенос элементов туда из списка
                                    mnog[i] = new int[listInd.Count];
                                    for (int z = 0; z < listInd.Count; z++)
                                        mnog[i][z] = listInd[z];
                                    listInd.Clear();
                                }
                                //задание множества из нечетных элементов
                                if (conditions == "2" || conditions == "нечетность")
                                {
                                    //поиск 1 элемента, удовлетворяющего условию
                                    int elem = 0;
                                    if (left % 2 == 0)
                                        elem = left + 1;
                                    if (left % 2 != 0)
                                        elem = left;
                                    int k = 0;
                                    //добавление всех элементов в список
                                    while (elem <= right)
                                    {
                                        listInd.Add(elem);
                                        k++;
                                        elem += 2;
                                    }
                                    //задание размера массива и перенос элементов туда из списка
                                    mnog[i] = new int[listInd.Count];
                                    for (int z = 0; z < listInd.Count; z++)
                                        mnog[i][z] = listInd[z];
                                    listInd.Clear();
                                }
                                //задание множества из элементов кратных определенному числу
                                if (conditions == "3" || conditions == "кратность")
                                {
                                    int numberMultiplicity = InputInt("\nВведите какому числу кратны числа в множестве");
                                    //поиск 1 элемента, удовлетворяющего условию
                                    int elem = left;
                                    while (elem % numberMultiplicity != 0 && elem <= right)
                                        elem++;
                                    int k = 0;
                                    //добавление всех элементов в список
                                    while (elem <= right)
                                    {
                                        listInd.Add(elem);
                                        k++;
                                        elem += numberMultiplicity;
                                    }
                                    //задание размера массива и перенос элементов туда из списка
                                    mnog[i] = new int[listInd.Count];
                                    for (int z = 0; z < listInd.Count; z++)
                                        mnog[i][z] = listInd[z];
                                    listInd.Clear();
                                }
                            }
                            //заполнение массива случайными числами
                            if (method == "случайно" || method == "3")
                            {
                                int sizeMn = InputInt("\nВведите размер " + (i + 1) + " множества: ");
                                mnog[i] = new int[sizeMn];
                                Random rnd = new Random();
                                byte[] bytes = new byte[100];
                                for (int j = 0; j < sizeMn; j++)
                                {
                                    rnd.NextBytes(bytes);
                                    mnog[i][j] = rnd.Next(left, right);
                                }
                            }
                        }
                        OutputMnog(mnog, numMn);
                        break;
                    case 3: //строка
                        OutputMnog(mnog, numMn);
                        List<int> res = new List<int>();
                        Regex regexExpression = new Regex(@"^(d*\(*[A-Z]\s*[-+/s]*\s*d*\(*[A-Z]*\)*\s*(\s*[-+/s]\s*d*\(*[A-Z]\)*\s*)*){1,24}$");
                        string input = Console.ReadLine();
                        if (!regexExpression.IsMatch(input))
                        {
                            System.Console.WriteLine("Выражение задано неверно!");
                            Console.ReadKey();
                            break;
                        }
                        else if (regexExpression.IsMatch(input))
                        {
                            res = UnidentifiedExpression(mnog, input, left, right);
                        }
                        OutputResult(res);
                        break;
                    case 4: //Выход из программы
                        stop = true;
                        break;
                    default:
                        Console.Write("Неправильно введена команда!");
                        break;
                }
                //Если была введена команда 4, то цикл прервется
                if (stop)
                    break;
            }
        }
    }
}
