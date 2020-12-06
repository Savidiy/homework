using System;
using System.Text;

//Васильев Леонид
//3.С клавиатуры вводятся числа, пока не будет введен 0. Подсчитать сумму всех нечетных положительных чисел.

namespace L2Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("= Задание 3: Сумма введенных нечетных положительных чисел =");

           
            Console.WriteLine("\n Вводите целые числа (0 для остановки):");

            int sum = 0;
            int k = 2;
            do
            {
                Console.Write(">");
                k = Convert.ToInt32(Console.ReadLine());
                if (k > 0)
                {
                    if (k % 2 == 1)
                    {
                        sum += k;
                    }
                }
                Console.WriteLine($" Сумма введенных нечетных положительны чисел равна {sum}.");
            }
            while (k != 0);

            Console.Write("\nНажмите любую клавишу");
            Console.ReadKey();
        }
    }
}
