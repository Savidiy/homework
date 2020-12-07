using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Васильев Леонид
// 7.a) Разработать рекурсивный метод, который выводит на экран числа от a до b (a<b);
// б) *Разработать рекурсивный метод, который считает сумму чисел от a до b.

namespace L2Task7
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write("  Введите первое целое число\n> ");
                int a = StringToIntSafe(Console.ReadLine());
                Console.Write("  Введите второе целое число\n> ");
                int b = StringToIntSafe(Console.ReadLine());

                if (a > b)
                {
                    a = a + b;
                    b = a - b;
                    a = a - b;
                }

                //Part A
                Console.Write("\n Последовательный вывод:\n ");
                RepeatOutput(a, b);

                //Part B
                Console.WriteLine($"\n Сумма чисел диапазона = { RepeatSum(a, b)}");               

                Console.WriteLine("\nНажмите любую клавишу для повторения\n");
                Console.ReadKey();
                //Console.Clear();
            }
        }

        static void RepeatOutput(int a, int b)
        {
            Console.Write($"{a} ");
            if (a < b)
                RepeatOutput(a + 1, b);
        }

        static int RepeatSum(int a, int b)
        {
            if (a == b)
                return b;
            else
                return a + RepeatSum(a + 1, b);
        }

        static int StringToIntSafe(string t, bool onlyPositive = false)
        {
            string res = "";

            foreach (var c in t)
            {
                if (char.IsDigit(c))
                {
                    res += c;
                }
            }

            if (res.Length > 0)
            {
                //there are digits, chech negative number
                if (!onlyPositive)
                {
                    if (t[0] == '-')
                        res = "-" + res;
                }
            }
            else
            {
                //trere are no digits
                return 0;
            }

            return Convert.ToInt32(res);
        }
    }
}
