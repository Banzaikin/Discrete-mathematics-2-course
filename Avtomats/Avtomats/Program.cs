using System;
public class Program
{
    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("Введите слово: ");

            if (RecognizingAutomaton.IsWord(Console.ReadLine()))
                Console.WriteLine("+");
            else
                Console.WriteLine("-");

        }
    }
}

public class RecognizingAutomaton
{
    private enum States
    {
        Start,
        A,
        B,
        C,
        D,
        AA,
        BB,
        CC,
        DD,
        Final
    }

    private static readonly char[] alphabet = { 'a', 'b', 'c', 'd' };

    private static readonly int[,] matrix = new int[,]
    {
        {1, 5, 2, 3, 4, 5, 2, 3, 4, 9},
        {2, 1, 6, 3, 4, 1, 6, 3, 4, 9},
        {3, 1, 2, 7, 4, 1, 2, 7, 4, 9},
        {4, 1, 2, 3, 8, 1, 2, 3, 8, 9}
    };

    private static States Transition(States currentState, char input)
    {
        int inputIndex = Array.IndexOf(alphabet, input);
        if (inputIndex == -1)
        {
            Console.WriteLine("такой буквы не может быть в алфавите!");
            return States.Final;
        }

        return (States)matrix[inputIndex, (int)currentState];
    }

    public static bool IsWord(string word)
    {
        States state = States.Start;

        foreach (char letter in word)
        {
            state = Transition(state, letter);
            Console.WriteLine($"Letter: {letter}; State: {state}");
            
            if (state == States.Final)
                break;
        }

        return state == States.AA || state == States.BB || state == States.CC || state == States.DD;
    }
}