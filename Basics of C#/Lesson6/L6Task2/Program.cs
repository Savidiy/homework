using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// Леонид Васильев
/// 2. Модифицировать программу нахождения минимума функции так, чтобы можно было передавать функцию в виде делегата.
/// а) Сделайте меню с различными функциями и предоставьте пользователю выбор, для какой функции и на каком отрезке находить минимум.
/// б) Используйте массив(или список) делегатов, в котором хранятся различные функции.
/// в) *Переделайте функцию Load, чтобы она возвращала массив считанных значений. Пусть она возвращает минимум через параметр.

namespace L6Task2
{
    class Program
    {
        class FuncWithName
        {
            public string name;
            public Func<double, double> func;

            public FuncWithName(string n, Func<double, double> f)
            {
                name = n;
                func = f;
            }
        }

        public static void SaveFunc(string fileName, Func<double,double> F, double startX, double maxX, double stepX)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            double x = startX;
            while (x <= maxX)
            {
                bw.Write(F(x));
                x += stepX;
            }
            bw.Close();
            fs.Close();
        }
        public static double[] LoadFuncAndGetMin(string fileName, out double min, out long step)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader bw = new BinaryReader(fs);
            min = double.MaxValue;
            step = 0;
            double d;
            long size = fs.Length / sizeof(double);
            double[] array = new double[size];
            for (int i = 0; i < size; i++)
            {
                // Считываем значение и переходим к следующему
                d = bw.ReadDouble();
                array[i] = d;
                if (d < min)
                {
                    min = d;
                    step = i;
                }
            }
            bw.Close();
            fs.Close();
            return array;
        }
        static void Main(string[] args)
        {

            List<FuncWithName> funcs = new List<FuncWithName>
            {
                new FuncWithName("2X + 5", delegate (double x) { return 2*x+5; }),
                new FuncWithName("Sin(X)", delegate (double x) { return Math.Sin(x); }),
                new FuncWithName("(X - 5)^2", delegate (double x) { return (x - 5) * (x - 5); }),
                new FuncWithName("X^2 - 20X + 10", delegate (double x) { return x * x - 20 * x + 10; }),
                new FuncWithName("(X + 10)^0.5", delegate (double x) { return Math.Pow(x + 10, 0.5); })
            };

            int targetItem = 0;
            while (true)
            {
                Console.Clear();
                bool enter = false;
                PrintLn("Программа нахождения минимума функции на выбранном диапазоне.");
                // выбор функции
                PrintLn("Выберите функцию:");
                do
                {
                    Console.SetCursorPosition(0, 2);
                    for (int i = 0; i < funcs.Count; i++)
                    {
                        PrintLnWithColor($"{i + 1}. {funcs[i].name}", ConsoleColor.Green, ConsoleColor.Gray, i == targetItem);
                    }
                    var key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.Spacebar:
                        case ConsoleKey.Enter:
                            enter = true;
                            break;
                        case ConsoleKey.Escape:
                            Environment.Exit(0);
                            break;
                        case ConsoleKey.UpArrow:
                            targetItem--;
                            if (targetItem < 0) targetItem += funcs.Count;
                            break;
                        case ConsoleKey.DownArrow:
                            targetItem++;
                            if (targetItem >= funcs.Count)
                                targetItem -= funcs.Count;
                            break;
                    }

                } while (enter == false);
                PrintLn();

                // получение параметров
                double startX = GetParFromDialog("Введите начальное значение диапазона: ");
                double maxX = GetParFromDialog("Введите конечное значение диапазона: ");
                double stepX;
                do
                {
                    stepX = GetParFromDialog("Введите шаг инкремента параметра функции: ");
                    if (stepX == 0)
                    {
                        PrintLnWithColor("Шаг инкремента не может быть равен нулю.",ConsoleColor.Red);
                    }
                } while (stepX == 0);


                // работа с файлами
                PrintLn();
                string fileName = "data.bin";
                PrintLn($"Сохраняю значения функции в файл {fileName}.");
                SaveFunc(fileName, funcs[targetItem].func, startX, maxX, stepX);
                PrintLn($"Загружаю значения функции из файла {fileName}.");
                double[] array = LoadFuncAndGetMin(fileName, out double min, out long step);
                PrintLn($"Загружено {array.Length} значений функции.");
                PrintLn();

                // вывод результатов
                PrintLn($"Минимум функции {funcs[targetItem].name} на отрезке [{startX}:{maxX}] равен {string.Format("{0:0.###}", min)} при Х = {string.Format("{0:0.###}", startX + step * stepX)}.");
                PrintLn();
                Print("Нажмите любую клавишу для повтора");
                Console.ReadKey(true);
            }
        }
        static public double GetParFromDialog(string message)
        {
            bool ok;
            double d;
            do
            {
                Print(message);
                string text = Console.ReadLine();
                ok = double.TryParse(text, out d);
                if (ok == false)
                {
                    PrintLnWithColor($"\"{text}\" не является числом.", ConsoleColor.Red);
                }
            } while (ok == false);
            return d;
        }
        static public void PrintLnWithColor(string text, ConsoleColor trueColor, ConsoleColor falseColor, bool trueFalse)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = trueFalse ? trueColor : falseColor;
            PrintLn(text);
            Console.ForegroundColor = oldColor;
        }
        static public void PrintLnWithColor(string text, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            PrintLn(text);
            Console.ForegroundColor = oldColor;
        }
        static public void PrintWithColor(string text, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Print(text);
            Console.ForegroundColor = oldColor;
        }
        static public void Print(string t)
        {
            Console.Write(t);
        }
        static public void PrintLn()
        {
            Console.WriteLine();
        }
        static public void PrintLn(string t)
        {
            Console.WriteLine(t);
        }
    }
}
