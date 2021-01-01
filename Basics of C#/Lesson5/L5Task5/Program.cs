using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// Леонид Васильев
/// 5. **Написать игру «Верю. Не верю». В файле хранятся вопрос и ответ, правда это или нет. 
/// Например: «Шариковую ручку изобрели в древнем Египте», «Да». 
/// Компьютер загружает эти данные, случайным образом выбирает 5 вопросов и задаёт их игроку. 
/// Игрок отвечает Да или Нет на каждый вопрос и набирает баллы за каждый правильный ответ. 
/// Список вопросов ищите во вложении или воспользуйтесь интернетом.

namespace L5Task5
{
    class Question
    {
        public bool IsTrue { get; private set; }
        public string Text { get; private set; }
        public Question (bool isTrue, string text)
        {
            IsTrue = isTrue;
            Text = text;
        }
    }
    class TrueFalse
    {
        int howManyQuestionsInGame;
        int[] selectedQuestions;
        Question[] questions;
        bool isLoaded = false;
        public int Scores { get; private set; } = 0;
        int currentNumber;
        Random random;

        public TrueFalse(string filename)
        {
            random = new Random();
            LoadQuestions(filename);
        }

        public bool LoadQuestions(string filename)
        {
            isLoaded = false;
            string path = AppDomain.CurrentDomain.BaseDirectory + filename;
            if (File.Exists(path))
            {
                isLoaded = true;
                using (StreamReader sr = new StreamReader(path))
                {
                    int count;
                    int iter = 0;
                    if (int.TryParse(sr.ReadLine(), out count))
                    {
                        questions = new Question[count];

                        while (sr.EndOfStream == false)
                        {
                            if (iter >= count)
                            {
                                Console.WriteLine("В файле больше вопросов, чем указано в первой строке");
                                return true;
                            }
                            string text = sr.ReadLine();
                            if (text.Length > 3)
                            {
                                bool isTrue = text[0] == 'T';
                                questions[iter] = new Question(isTrue, text.Substring(2, text.Length - 2));
                                iter++;
                            }
                            else
                            {
                                isLoaded = false;
                                return false;
                            }
                        }
                    } else
                    {
                        isLoaded = false;
                        return false;
                    }
                }
            }
            return isLoaded;
        }

        public bool NewGame(int howManyQuestions)
        {
            if (isLoaded && howManyQuestions < questions.Length)
            {
                howManyQuestionsInGame = howManyQuestions;
                selectedQuestions = new int[howManyQuestionsInGame];
                currentNumber = 0;
                Scores = 0;
                return true;
            }
            else
                return false;
        }

        public int NextQuestionNumber()
        {
            // повторять пока не найдешь вопрос, которого еще не было
            bool repeat;
            int nextNumber;
            if (currentNumber < howManyQuestionsInGame)
            {

                do
                {
                    repeat = false;
                    nextNumber = random.Next(questions.Length);
                    for (int i = 0; i < currentNumber; i++)
                    {
                        if (nextNumber == selectedQuestions[i])
                        {
                            repeat = true;
                            break;
                        }
                    }
                } while (repeat);
                selectedQuestions[currentNumber] = nextNumber;
                currentNumber++;

                return nextNumber;
            } else
            {
                throw new Exception("Хочешь слишком много вопросов, мы так не договаривались!");
            }
        }

        public string this[int questionNumber]
        {
            get
            {
                if (isLoaded && questionNumber >= 0 && questionNumber < questions.Length)
                {
                    return questions[questionNumber].Text;
                }
                else
                {
                    throw new Exception("Недопустимое значение индекса массива вопросов.");
                }
            }
        }

        public bool TakeAnswer(int questionNumber, bool answer)
        {
            if (isLoaded && questionNumber >= 0 && questionNumber < questions.Length)
            {
                if (questions[questionNumber].IsTrue == answer)
                {
                    Scores++;
                    return true;
                } else
                {
                    return false;
                }
            }
            else
            {
                throw new Exception("Недопустимое значение индекса массива вопросов");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string selText =    "        ПРАВДА    <   >    ЛОЖЬ      ";
            string trueText =   "        ПРАВДА                       ";
            string falseText =  "                           ЛОЖЬ      ";
            int centerOfText = 20;

            while (true)
            {
                Console.Clear();
                int questionsCount = 5;
                PrintLn("Давайте сыграем в игру \"Правда или ложь\".");
                PrintLn($"Вам будет задано {questionsCount} вопросов");
                PrintLn("Выбирайте ответ клавишами влево или вправо.");
                PrintLn();

                var game = new TrueFalse("questions.txt");
                if (game.NewGame(questionsCount))
                {
                    for (int i = 0; i < questionsCount; i++)
                    {
                        // пишем вопрос
                        int qNum = game.NextQuestionNumber();
                        string text = game[qNum];
                        PrintLn($"{i + 1}. {text}");
                        Print(selText);
                        Console.SetCursorPosition(centerOfText, Console.CursorTop);

                        // получаем ответ от клавиш
                        bool? myAnswer = null;
                        do 
                        {
                            var key = Console.ReadKey(true);
                            if (key.Key == ConsoleKey.LeftArrow)
                                myAnswer = true;
                            else if (key.Key == ConsoleKey.RightArrow)
                                myAnswer = false;
                        } while (myAnswer == null);

                        // подсвечиваем выбранные ответ, цветом покажем правильный он или нет
                        bool isCorrect = game.TakeAnswer(qNum, (bool)myAnswer); 
                        Console.SetCursorPosition(0, Console.CursorTop);
                        if ((bool)myAnswer) 
                            PrintLnWithColor(trueText, ConsoleColor.DarkGreen, ConsoleColor.Red, isCorrect);
                        else
                            PrintLnWithColor(falseText, ConsoleColor.DarkGreen, ConsoleColor.Red, isCorrect);
                        PrintLn();
                    }

                    if (game.Scores <= questionsCount / 3) 
                        PrintLn($"Сочувствую, вы набрали всего {game.Scores} из {questionsCount} очков!");
                    else if (game.Scores <= questionsCount * 2 / 3)
                        PrintLn($"Хороший результат. Вы набрали {game.Scores} из {questionsCount} очков!");
                    else
                        PrintLn($"Поздравляем! Вы набрали {game.Scores} из {questionsCount} очков!");

                } else
                {
                    PrintLn("Не удалось запустить игру");
                }

                PrintLn();
                Print("Нажмите любую клавишу для повторной игры");
                Console.ReadKey(true);
            }
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
