using System;

// Васильев Леонид
// 5.
// а) Написать программу, которая запрашивает массу и рост человека, вычисляет его индекс массы и сообщает, нужно ли человеку похудеть, набрать вес или все в норме;
// б) *Рассчитать, на сколько кг похудеть или сколько кг набрать для нормализации веса.

namespace L2Task5
{
    enum STATUS {light, normal, heavy }
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write(" Введите вес пациента в килограммах\n> ");
                int weight = StringToIntSafe(Console.ReadLine(), true);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine($"> {weight}кг                ");
                
                Console.Write(" Введите рост пациента в сантиметрах\n> ");
                float height = (float)StringToIntSafe(Console.ReadLine(), true) / 100;
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine($"> {height*100:f0}см                ");

                if (weight > 0 && height > 0)
                {
                    float imt = weight / (height * height);
                    Console.WriteLine($"\nВаш индекс массы тела (ИМТ) равен {imt:F2} кг/м^2.");

                    STATUS status = STATUS.normal;

                    if (imt <= 16)
                    {
                        Console.WriteLine("У вас выраженный дефицит массы тела. ИМТ меньше 16.");
                        status = STATUS.light;
                    }
                    else if (imt <= 18.5f)
                    {
                        Console.WriteLine("У вас недостаточная(дефицит) масса тела. ИМТ от 16 до 18,5.");
                        status = STATUS.light;
                    }
                    else if (imt <= 25f)
                    {
                        Console.WriteLine("У вас нормальная масса тела. ИМТ от 18,5 до 25.");
                        status = STATUS.normal;
                    }
                    else if (imt <= 30f)
                    {
                        Console.WriteLine("У вас избыточная масса тела (предожирение). ИМТ от 25 до 30.");
                        status = STATUS.heavy;
                    }
                    else
                    {
                        Console.WriteLine("У вас ожирение. Вы пухляш. ИМТ больше 30.");
                        status = STATUS.heavy;
                    }

                    float delta = 0;
                    switch (status)
                    {
                        case STATUS.light:
                            Console.WriteLine("\nРекомендация:");
                            delta = 18.5f * height * height - weight;
                            Console.WriteLine($"Вам следует набрать {delta:f1}кг.");
                            break;
                        case STATUS.normal:
                            break;
                        case STATUS.heavy:
                            Console.WriteLine("\nРекомендация:");
                            delta = weight - 25f * height * height;
                            Console.WriteLine($"Вам следует похудеть на {delta:f1}кг.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\nОшибка! Для расчета ваш рост и вес должен быть больше нуля.");
                }            

                Pause(1000);
                Console.Write("\nНажмите любую клавишу, чтобы повторить расчет");
                Console.ReadKey();
                Console.Clear();
            }
        }

        /// <summary>
        /// Убирает все кроме цифр из строки и определяет введенное целое число, если цифр не введено то возвращает 0
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        static int StringToIntSafe(string t, bool onlyPositive = false)
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

        static void Pause(int millsec)
        {
            System.Threading.Thread.Sleep(millsec);
        }
    }
}
