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
            int max = 1000000000;
            int border = 0;

            for (var i = 1; i <= max; i++)
            {
                //15 sec without logic in IsGoodNumber
                if (IsGoodNumber(i))
                {
                    count++;
                }
                //progree bar does not take many time ~300 mills
                if (i > border)
                {
                    procent++;
                    border = max / 100 * procent;
                    Console.SetCursorPosition(0, 1);
                    Console.WriteLine($"{procent}%");
                }
            }
            #endregion

            DateTime endTime = DateTime.Now;
            TimeSpan delta = endTime.Subtract(startTime);

            Console.WriteLine($"Расчеты заняли {delta.Minutes:d2}:{delta.Seconds:d2}.{delta.Milliseconds:d3} ({delta.Ticks} тиков).");
            Console.Write("Нажмите любую клавишу");
            Console.ReadKey();
        }
        static bool IsGoodNumber(int k)
        {

            return true;
        }
    }

}
