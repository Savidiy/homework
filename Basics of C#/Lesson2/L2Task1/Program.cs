using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Васильев Леонид

//1.Написать метод, возвращающий минимальное из трех чисел.

namespace L2Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("= Задание 1: Найти минимальное из трех чисел =\n");
            Console.Write("Введите первое целое число\n>");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите второе целое число\n>");
            int b = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите третье целое число\n>");
            int c = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"\nСамое маленькое число это {Min(a, b, c)}.\n");
            Console.Write("Нажмите любую клавишу");
            Console.ReadKey();
        }

        static int Min(int a, int b)
        {
            return a < b ? a : b;
        }

        static int Min (int a, int b, int c)
        {
            return Min(a, Min(b, c));
        }
    }
}
