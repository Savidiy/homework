using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// Леонид Васильев
/// 1. Дан целочисленный массив из 20 элементов. 
/// Элементы массива могут принимать целые значения от –10 000 до 10 000 включительно. 
/// Написать программу, позволяющую найти и вывести количество пар элементов массива, в которых хотя бы одно число делится на 3. 
/// В данной задаче под парой подразумевается два подряд идущих элемента массива. 
/// Например, для массива из пяти элементов: 6; 2; 9; –3; 6 – ответ: 4.

namespace L4Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                int min = -10000;
                int max = 10000;
                Random random = new Random();
                int[] nums = new int[20];
                Console.Write("\nДан массив чисел:\n");
                for (int i = 0; i < nums.Length; i++)
                {
                    nums[i] = random.Next(min, max);
                    if (nums[i] % 3 == 0)
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    else
                        Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"{nums[i]} ");
                }

                int counter = 0;
                for (int i = 1; i < nums.Length; i++)
                {
                    if ((nums[i] % 3 == 0) || (nums[i - 1] % 3 == 0))
                    {
                        counter++;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($"\n\nЗдесь есть ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(counter);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($" пар соседних чисел, где хотя бы одно число делится на 3.");
                Console.WriteLine("Нажмите любую клавишу для повтора");
                Console.ReadKey();
            }
        }
    }
}
