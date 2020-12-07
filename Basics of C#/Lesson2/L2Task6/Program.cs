using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Васильев Леонид
// 6. * Написать программу подсчета количества «Хороших» чисел в диапазоне от 1 до 1 000 000 000. 
// Хорошим называется число, которое делится на сумму своих цифр. 
// Реализовать подсчет времени выполнения программы, используя структуру DateTime.
namespace L2Task6
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime startTime = DateTime.Now;

            Console.WriteLine("Начинаю подсчет количества «Хороших» чисел в диапазоне от 1 до 1 000 000 000.");
            #region Расчеты
            int count = 0;
            int procent = -1;
            int start = 1;
            // int max = 10000000;
            int max = 1000000000;
            int border = 0;

            IsGoodNumber(100);
            IsGoodNumber(220);
            IsGoodNumber(1);
            IsGoodNumber(0);

            long digitSum = SumOfDigits(start);

            for (long i = start; i <= max;)
            {
                //15 sec without logic in IsGoodNumber
                //variant 1: count 61 574 510 with use only isGoodNumber 5 min 3 sec
                //variant 2: count 61 574 510 with use isGoodNumber on tenth numbers 50 sec
                #region Variant 2
                if (i % digitSum == 0)
                {
                    count++;
                }

                i++;

                if (i % 10 == 0)
                {
                    digitSum = SumOfDigits(i);
                }
                else
                {
                    digitSum += 1;
                }
                #endregion

                #region Variant 1
                //if (IsGoodNumber(i))
                //{
                //    count++;
                //}
                //i++;
                #endregion

                //progree bar does not take many time ~300 mills
                if (i > border)
                {
                    procent++;
                    border = max / 100 * (procent + 1);
                    Console.SetCursorPosition(0, 1);
                    Console.WriteLine($"{procent}%");
                }
            }
            #endregion

            DateTime endTime = DateTime.Now;
            TimeSpan delta = endTime.Subtract(startTime);

            Console.WriteLine($"Количество хороших чисел {count}.");
            Console.WriteLine($"Расчеты заняли {delta.Minutes:d2}:{delta.Seconds:d2}.{delta.Milliseconds:d3} ({delta.Ticks} тиков).");
            Console.Write("Нажмите любую клавишу");
            Console.ReadKey();
        }
                
        /// <summary>
        /// Хорошим называется число, которое делится на сумму своих цифр. 
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        static bool IsGoodNumber(long k)
        {
            k = Math.Abs(k);
            if (k == 0) 
                return false;
            long digitSum = SumOfDigits(k);
            return k % digitSum == 0;
        }

        static long SumOfDigits(long k)
        {
            long digitSum = 0;
            long divider = 1;
            long remains = k;

            while (divider <= k)
            {
                long mod = remains / divider - (remains / (divider * 10)) * 10;
                if (mod > 0)
                {
                    digitSum += mod;
                }
                remains -= mod * divider;
                divider *= 10;
            }

            return digitSum;
        }
    }

}
