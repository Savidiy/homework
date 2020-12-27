using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// Леонид Васильев
/// 4. Задача ЕГЭ.
/// *На вход программе подаются сведения о сдаче экзаменов учениками 9-х классов некоторой средней
/// школы. В первой строке сообщается количество учеников N, которое не меньше 10, но не
/// превосходит 100, каждая из следующих N строк имеет следующий формат:
/// < Фамилия > < Имя > < оценки >,
/// где < Фамилия > — строка, состоящая не более чем из 20 символов, <Имя> — строка, состоящая не
/// более чем из 15 символов, <оценки> — через пробел три целых числа, соответствующие оценкам по
/// пятибалльной системе. <Фамилия> и <Имя>, а также <Имя> и <оценки> разделены одним пробелом.
/// Пример входной строки:
/// Иванов Петр 4 5 3
/// Требуется написать как можно более эффективную программу, которая будет выводить на экран
/// фамилии и имена трёх худших по среднему баллу учеников. Если среди остальных есть ученики,
/// набравшие тот же средний балл, что и один из трёх худших, следует вывести и их фамилии и имена.

namespace L5Task4
{
    class Program
    {
        static void Main(string[] args)
        {

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
