using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// Леонид Васильев
/// 2. Разработать класс Message, содержащий следующие статические методы для обработки текста:
/// а) Вывести только те слова сообщения, которые содержат не более n букв. 
///     (Словами считаю набор из букв и цифр. Скобки, кавычки - не часть слова)
///     (Слова разделяются пробелами, переходом на новую строки, знаками препинания (в то числе . без последующего пробела), а служебными символами (скобками, кавычками и т.д.)
///     (Считаю, что оставляем только слова от сообщения, фильтруя все остальное. Выводим слова через пробел)
/// б) Удалить из сообщения все слова, которые заканчиваются на заданный символ. 
///     (Удаляем только слова (см. выше, остальные знаки должны остаться)
/// в) Найти самое длинное слово сообщения.
/// г) Сформировать строку с помощью StringBuilder из самых длинных слов сообщения.
/// Продемонстрируйте работу программы на текстовом файле с вашей программой.

namespace L5Task2
{
    class Program
    {
        class Message
        {
            static public string GetWordsShorterThanN (string text, int maxLength)
            {
                StringBuilder result = new StringBuilder();
                StringBuilder word = new StringBuilder();
                bool wordIsStarted = false;
                int wordLength = 0;
                foreach(var c in text)
                {
                    if (wordIsStarted)
                    {
                        if (char.IsLetterOrDigit(c))
                        {
                            if (wordLength <= maxLength)
                            {
                                wordLength++;
                                word.Append(c);
                            }
                        }
                        else
                        {
                            if (wordLength <= maxLength)
                            {
                                word.Append(" ");
                                result.Append(word);
                            }
                            word.Clear();
                            wordLength = 0;
                            wordIsStarted = false;
                        }
                    }
                    else
                    {
                        if (char.IsLetterOrDigit(c))
                        {
                            word.Append(c);
                            wordLength++;
                            wordIsStarted = true;
                        }
                    }
                }
                if (result.Length > 0)
                {
                    result.Remove(result.Length - 1, 1); // убираем последний пробел 
                }
                return result.ToString();
            }
            static public string RemoveWordsWithLastCharN (string text, char cursedChar)
            {
                StringBuilder result = new StringBuilder();
                StringBuilder word = new StringBuilder();
                bool wordIsStarted = false;
                foreach (var c in text)
                {
                    if (wordIsStarted)
                    {
                        if (char.IsLetterOrDigit(c))
                        {
                            word.Append(c);
                        }
                        else
                        {
                            if (word[word.Length - 1] != cursedChar )
                            {                              
                                result.Append(word);
                            }
                            word.Clear();
                            result.Append(c);
                            wordIsStarted = false;
                        }
                    }
                    else
                    {
                        if (char.IsLetterOrDigit(c))
                        {
                            word.Append(c);
                            wordIsStarted = true;
                        } else
                        {
                            result.Append(c);
                        }
                    }
                }
                return result.ToString();
            }
        }

        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                string fileName = "Program.cs";
                string path = AppDomain.CurrentDomain.BaseDirectory + fileName;
                PrintLn($"Демонстрация работы класса Message.");
                PrintLn($"Читаем файл {fileName} из рабочей директории.");
                if (File.Exists(path))
                {
                    string text = File.ReadAllText(path);
                    PrintLn($"Прочитано сообщение длиной {text.Length} символов.\n");
                    #region Задание А
                    {
                        PrintLnWithColor("a) Вывод только слов сообщения короче N символов.", ConsoleColor.DarkGreen);
                        bool isInt;
                        int maxLength = 0;
                        do
                        {
                            Print("Введите максимальное количество символов в слове: ");
                            string numString = Console.ReadLine();
                            isInt = int.TryParse(numString, out maxLength);
                            if (isInt == false)
                            {
                                PrintLnWithColor($"\"{numString}\" - не является целым число, попробуй еще раз.", ConsoleColor.Red);
                            }
                        } while (isInt == false);
                        PrintLnWithColor("Результат работы метода:", ConsoleColor.DarkGreen);
                        PrintLn(Message.GetWordsShorterThanN(text, maxLength));
                        PrintLn();
                    }
                    #endregion
                    #region Задание Б
                    {
                        PrintLnWithColor("б) Удалить из сообщения слова, заканчивающиеся на заданный символ.", ConsoleColor.DarkGreen);
                        bool isOneLetterOrDigit = false;
                        char cursedChar = ' ';
                        do
                        {
                            Print("Введите проклятый символ (цифру или букву): ");
                            var key = Console.ReadLine();
                            if (key.Length == 1)
                            {
                                isOneLetterOrDigit = char.IsLetterOrDigit(key[0]);
                            } 
                            else
                            {
                                isOneLetterOrDigit = false;
                            }
                            if (isOneLetterOrDigit == false)
                            {
                                PrintLnWithColor($"\"{key}\" - не является буквой или числом, попробуй еще раз.", ConsoleColor.Red);
                            } else
                            {
                                cursedChar = key[0];
                            }
                        } while (isOneLetterOrDigit == false);
                        PrintLnWithColor("Результат работы метода:", ConsoleColor.DarkGreen);
                        PrintLn(Message.RemoveWordsWithLastCharN(text, cursedChar));
                        PrintLn();
                    }
                    #endregion

                }
                else
                {
                    PrintLnWithColor("Не удалось прочитать файл!", ConsoleColor.Red);
                }


                Print("Нажмите любую клавишу для повтора");
                Console.ReadKey(true);

            } while (true);
        }

        static public void PrintLnWithColor(string text, ConsoleColor color)
        {
                ConsoleColor oldColor = Console.ForegroundColor;
                Console.ForegroundColor = color;
                PrintLn(text);
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
