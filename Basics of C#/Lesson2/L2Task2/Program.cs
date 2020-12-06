using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Васильев Леонид
//Урок 2. Задание 2.Написать метод подсчета количества цифр числа.

namespace L2Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("= Задание 2: Сосчитать количество цифр в числе =\n");

            while (true)
            {
                Console.Write("Введите целое число\n>");
                int a = Convert.ToInt32(Console.ReadLine());
                int n = DigitsCount(a);
                string e = n == 1 ? "ы" : "";
                Console.WriteLine($"{a} сотоит из {n} цифр{e}.\n");
            }
        }

        static int DigitsCount(int num)
        {
            if (num < 0)
            {
                num = -num;
            }
            if (num == 0)
            {
                return 1;
            } 
            
            int i = 1;
            int dec = 10;
            while (num >= dec)
            {
                dec = dec * 10;
                i++;
            }
            return i;
        }
    }
}
