using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// Леонид Васильев
/// 2. а) С клавиатуры вводятся числа, пока не будет введен 0 (каждое число в новой строке). 
/// Требуется подсчитать сумму всех нечетных положительных чисел. Сами числа и сумму вывести на экран, используя tryParse;
/// б) Добавить обработку исключительных ситуаций на то, что могут быть введены некорректные данные. 
/// При возникновении ошибки вывести сообщение. Напишите соответствующую функцию;

namespace L3Task2
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Вводите числа для расчета суммы всех нечетных положительных чисел. 0 - стоп.");
            bool clearZero = false;
            double sum = 0;
            do
            {
                string input = Console.ReadLine();
                double num = 0;
                if (double.TryParse(input, out num))
                {
                    if (num == 0)
                    {
                        clearZero = true;
                    } else
                    {
                        if (num > 0 && (num % 2 == 1))
                        {
                            sum += num;
                        }
                        Console.SetCursorPosition(input.Length + 1, Console.CursorTop - 1);
                        Console.WriteLine($"\tСумма нечетных положительных чисел = {sum}");
                    }
                } 
                else
                {
                    Console.SetCursorPosition(input.Length + 1, Console.CursorTop - 1);
                    Console.WriteLine("<< Введены некорректные данные. Попробуйте еще раз.");
                }

            }
            while (clearZero == false);

            Console.Write("Спасибо. Нажмите любую клавишу для выхода.");
            Console.ReadKey();
        }
    }
}
