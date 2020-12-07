using System;

// Васильев Леонид
// 5.
// а) Написать программу, которая запрашивает массу и рост человека, вычисляет его индекс массы и сообщает, нужно ли человеку похудеть, набрать вес или все в норме;
// б) *Рассчитать, на сколько кг похудеть или сколько кг набрать для нормализации веса.

namespace L2Task5
{

    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write(" Введите вес пациента в килограммах\n>");
                int weight = StringToIntSafe(Console.ReadLine());
                Console.Write(" Введите рост пациента в сантиметрах\n>");
                float height = (float)StringToIntSafe(Console.ReadLine()) / 100;

                float imt = weight / (height * height);
                Console.WriteLine($"Ваш индекс массы тела (ИМТ) равен {imt:F1}.");
            }
        }

        /// <summary>
        /// Убирает все кроме цифр из строки и определяет введенное целое число, если цифр не введено то возвращает 0
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        static int StringToIntSafe(string t)
        {
            string res = "";
            if (t.Length == 0)
                return 0;

            foreach (var c in t)
            {
                switch (c)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        res += c;
                        break;
                }
            }

            if (res.Length > 0)
            {
                //there are digits, chech negative number
                if (t[0] == '-')
                    res = "-" + res;
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
