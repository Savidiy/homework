using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// Леонид Васильев
/// 3. *Для двух строк написать метод, определяющий, является ли одна строка перестановкой (символов) другой. Регистр можно не учитывать:
/// а) с использованием методов C#;
/// б) *разработав собственный алгоритм.
/// Например:
/// badc являются перестановкой abcd.
/// 
/// Замечание:
/// Не понял какие методы С# подразумеваются в пункте А. 
/// Не нашел стандартной функции, которая делает такую проверку, а все остальные решения подподают под пункт Б.
/// first.Intersect(second) не учитывает повторение одних и тех же символов
/// 
/// Про использование неизменяемых строк прочитал только в условии четвертого задания переделывать 2 и 3 задачу не стал.


namespace L5Task3
{
    class Program
    {
        /// <summary>
        /// Строки являются пермешанными, если имеют одинаковую длину
        /// и состоят из одного и того же набора символов, то есть каждый символ встерчается в обоих строках одинаковое количество раз.
        /// </summary>
        /// <param name="strA"></param>
        /// <param name="strB"></param>
        /// <returns>TRUE если строки являются пермешанным друг для друга</returns>
        static bool IsReshuffleStrings(string strA, string strB)
        {
            if (strA.Length != strB.Length) return false;

            strA = strA.ToLower(); // регистронезависимая проверка
            strB = strB.ToLower();
            var remainCharsFromB = new StringBuilder(strB);

            foreach (var c in strA)
            {
                bool isFinded = false;
                for (int i = 0; i < remainCharsFromB.Length; i++)
                {
                    if (remainCharsFromB[i] == c)
                    {
                        remainCharsFromB.Remove(i, 1); // со string было бы strB.Remove(i, 1)                        
                        isFinded = true;
                        break;
                    }
                } 
                if (isFinded == false)
                {
                    return false;
                }
            }
            return true;
        }

        static void Main(string[] args)
        {
            while (true)
            {
                PrintLn("Проверим является ли одна строка перестановкой символов другой строки.");
                PrintLn("Регистр букв не имеет значения.");
                while (true)
                {
                    Print("Введите первую строку: ");
                    string first = Console.ReadLine();
                    Print("Введите вторую строку: ");
                    string second = Console.ReadLine();
                    bool isShuffleStr = IsReshuffleStrings(first, second);
                    PrintWithColor(first, ConsoleColor.White);
                    if (isShuffleStr)
                    {
                        PrintWithColor(" является ", ConsoleColor.DarkGreen);
                    }
                    else
                    {
                        PrintWithColor(" не является ", ConsoleColor.Red);
                    }
                    Print("перестановкой символов строки ");
                    PrintWithColor(second, ConsoleColor.White);
                    PrintLn(".");
                    PrintLn();
                }
            }
        }
        static public void PrintLnWithColor(string text, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            PrintLn(text);
            Console.ForegroundColor = oldColor;
        }
        static public void PrintWithColor(string text, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Print(text);
            Console.ForegroundColor = oldColor;
        }
        static public void Print(string t)
        {
            Console.Write(t);
        }
        static public void PrintLn()
        {
            Console.WriteLine();
        }
        static public void PrintLn(string t)
        {
            Console.WriteLine(t);
        }
    }
}
