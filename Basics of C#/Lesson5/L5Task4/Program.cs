using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// Леонид Васильев
/// 4. Задача ЕГЭ.
/// *На вход программе подаются сведения о сдаче экзаменов учениками 9-х классов некоторой средней
/// школы. В первой строке сообщается количество учеников N, которое не меньше 10, но не
/// превосходит 100, каждая из следующих N строк имеет следующий формат:
/// < Фамилия > < Имя > < оценки >,
/// где < Фамилия > — строка, состоящая не более чем из 20 символов, <Имя> — строка, состоящая не
/// более чем из 15 символов, <оценки> — через пробел три целых числа, соответствующие оценкам по
/// пятибалльной системе. <Фамилия> и <Имя>, а также <Имя> и <оценки> разделены одним пробелом.
/// Пример входной строки:
/// Иванов Петр 4 5 3
/// Требуется написать как можно более эффективную программу, которая будет выводить на экран
/// фамилии и имена трёх худших по среднему баллу учеников. Если среди остальных есть ученики,
/// набравшие тот же средний балл, что и один из трёх худших, следует вывести и их фамилии и имена.

namespace L5Task4
{
    class Student
    {
        public float AverageScore { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public Student (string firstName, string lastName, int s1, int s2, int s3)
        {
            FirstName = firstName;
            LastName = lastName;
            AverageScore = (s1 + s2 + s3) / 3f;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                string filename = "students1.txt";
                // string filename = "students2.txt"; // всего два варианта среднего бала
                PrintLn($"Будут загружены данные из файла {filename}");
                Print("Нажмите Enter для продолжения или другую клавишу для ввода имени файла.");
                var key = Console.ReadKey(true);
                PrintLn();
                if (key.Key != ConsoleKey.Enter)
                {
                    PrintLn("Введите имя файла:");
                    filename = Console.ReadLine();
                }

                string path = AppDomain.CurrentDomain.BaseDirectory + filename;
                PrintLn($"Загружаю данные из файла {filename}");
                if (File.Exists(path))
                {
                    #region Загрузка данных из файла
                    int count = 0;
                    Student[] students;
                    using (StreamReader sr = new StreamReader(path))
                    {
                        count = int.Parse(sr.ReadLine());
                        students = new Student[count];
                        for (int i = 0; i < count; i++)
                        {
                            string line = sr.ReadLine();
                            var data = line.Split(' ');
                            string lastName = data[0];
                            string firstName = data[1];
                            int s1 = int.Parse(data[2]);
                            int s2 = int.Parse(data[3]);
                            int s3 = int.Parse(data[4]);
                            students[i] = new Student(firstName, lastName, s1, s2, s3);
                        }
                    }
                    PrintLn($"Загружено {count} строк.");
                    #endregion

                    #region Ищем минимальные средние
                    float upLimit = 6;
                    float mins1 = upLimit;
                    float mins2 = upLimit;
                    float mins3 = upLimit;
                    for (int i = 0; i < count; i++)
                    {
                        float aver = students[i].AverageScore;
                        if (aver < mins3)
                        {
                            if (aver < mins2)
                            {
                                if (aver < mins1)
                                {
                                    mins3 = mins2;
                                    mins2 = mins1;
                                    mins1 = aver;
                                }
                                else if (aver != mins1)
                                { 
                                    mins3 = mins2;
                                    mins2 = aver;
                                }
                            }
                            else if (aver != mins2)
                            {
                                mins3 = aver;
                            }
                        }
                    }
                    // коррекция на случай если у всех одинаковый минимальный балл
                    if (mins2 == upLimit) mins2 = mins1;
                    if (mins3 == upLimit) mins3 = mins2;
                    #endregion

                    #region Выводим результаты
                    PrintLn("Три ученика с минимальными средними баллами (больше трех при равенстве баллов):");
                    for (int i = 0; i < count; i++)
                    {
                        float aver = students[i].AverageScore;
                        if (aver <= mins3)
                        {
                            PrintLn($"{students[i].LastName} {students[i].FirstName} {aver:f2}");
                        }
                    }
                    #endregion
                }
                else
                {
                    PrintLn($"Не найден файл {path}");
                }
                PrintLn();
                PrintLn($"Нажмите любую клавишу для повтора");
                Console.ReadKey(true);

            }
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
