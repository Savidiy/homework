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

            // propeties
            int start = 1;
            //int start = 75;
            //int max = 50000000;
            int max = 1000000000;

            Console.WriteLine($"Начинаю подсчет количества «Хороших» чисел в диапазоне от {start} до {max:N0}.");

            //Вариант расчетов номер 3, самый быстрый
            for (int i = 3; i <= 3; i++)
            {
                Console.WriteLine($"\nВариант расчета №{i}");
                DateTime startTime = DateTime.Now;

                int count = GoodNumbersVariant(start, max, i);

                DateTime endTime = DateTime.Now;
                TimeSpan delta = endTime.Subtract(startTime);
                Console.WriteLine($"Количество хороших чисел {count:N0}.");
                Console.WriteLine($"Расчеты заняли {delta.Minutes:d2}:{delta.Seconds:d2}.{delta.Milliseconds:d3}.");
            }

            Console.Write("Нажмите любую клавишу");
            Console.ReadKey();
        }

        static int GoodNumbersVariant(int start, int max, int variant)
        {

            int count = 0;
            int procent = -1;
            int border = 0;

            long digitSum = SumOfDigits(start);
            int ten = start % 10;

            for (long i = start; i <= max;)
            {
                //15 sec without logic in IsGoodNumber
                //variant 1: count 61 574 510 with use only isGoodNumber 5 min
                //variant 2: count 61 574 510 with use isGoodNumber on tenth numbers 50 sec
                //variant 3: count 61 574 510 with increment SumOfDigits numbers 20 sec
                #region Variant 3
                if (variant == 3)
                {
                    if (i % digitSum == 0)
                    {
                        count++;
                    }

                    i++;
                    ten++;

                    //особый учет при переходе с 9 на 0
                    if (ten == 10)
                    {
                        long zeroCount = 0;
                        long divider = 1;
                        long remains = i;

                        //расчет количества нулей подряд справа до первой цифры
                        while (divider <= i)
                        {
                            long mod = remains / divider - (remains / (divider * 10)) * 10;
                            if (mod == 0)
                            {
                                zeroCount++;
                            }
                            else
                            {
                                //count zero defore first digit
                                break;
                            }
                            remains -= mod * divider;
                            divider *= 10;
                        }
                        //коррекция суммы чисел
                        digitSum = digitSum - zeroCount * 9 + 1;
                        ten = 0;
                    }
                    else
                    {
                        digitSum += 1;
                    }
                }
                #endregion

                #region Variant 2
                if (variant == 2)
                {
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
                }
                #endregion

                #region Variant 1
                if (variant == 1)
                {
                    if (IsGoodNumber(i))
                    {
                        count++;
                    }
                    i++;
                }
                #endregion

                //progress bar does not take many time ~300 mills
                if (i > border)
                {
                    procent++;
                    border = max / 100 * (procent + 1);
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write($"{procent}%");
                }
            }
            Console.WriteLine();
            return count;
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
