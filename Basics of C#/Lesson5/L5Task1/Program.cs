using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/// Леонид Васильев
/// 1. Создать программу, которая будет проверять корректность ввода логина. 
/// Корректным логином будет строка от 2 до 10 символов, содержащая только буквы латинского алфавита или цифры, при этом цифра не может быть первой:
/// а) без использования регулярных выражений;
/// б) с использованием регулярных выражений.

namespace L5Task1
{
    

    class LoginChecker
    {
        public delegate bool CheckLogin(string login, out List<CheckExtention> extList);

        public const int minLength = 2;
        public const int maxLength = 10;
        const string availableChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public enum CheckExtention {tooLong, tooShort, notEnglish, firstDigit }
        public static bool CheckLoginWithoutRegex(string login, out List<CheckExtention> extList)
        {
            extList = new List<CheckExtention>();
            bool isCorretLogin = true;
            if (login.Length < minLength)
            {
                isCorretLogin = false;
                extList.Add(CheckExtention.tooShort);
            }
            else if (login.Length > maxLength)
            {
                isCorretLogin = false;
                extList.Add(CheckExtention.tooLong);
            }
            if (login.Length > 0 && char.IsDigit(login[0]))
            {
                isCorretLogin = false;
                extList.Add(CheckExtention.firstDigit);
            }
            foreach (var c in login)
            {
                if (availableChars.Contains(c) == false)
                {
                    isCorretLogin = false;
                    extList.Add(CheckExtention.notEnglish);
                    break;
                }
            }
            return isCorretLogin;
        }
        public static bool CheckLoginWithRegex(string login, out List<CheckExtention> extList)
        {
            extList = new List<CheckExtention>();

            if (new Regex(@"^.{0,"+ (minLength - 1) + "}$").IsMatch(login)) extList.Add(CheckExtention.tooShort); // ^.{0,1}$
            if (new Regex(@".{"+(maxLength + 1)+",}").IsMatch(login)) extList.Add(CheckExtention.tooLong); // .{11,}
            if (new Regex(@"^[0-9]").IsMatch(login)) extList.Add(CheckExtention.firstDigit);
            if (new Regex(@"[^A-Za-z0-9]").IsMatch(login)) extList.Add(CheckExtention.notEnglish);

            Regex regex = new Regex(@"^((?!([0-9]))[A-Za-z0-9]{" + minLength + "," + maxLength + "})$"); // ^((?!([0-9]))[A-Za-z0-9]{2,10})$
            return regex.IsMatch(login);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            LoginChecker.CheckLogin checkLoginMethod;
            bool useRegex = true;
            if (useRegex)
                checkLoginMethod = LoginChecker.CheckLoginWithRegex;
            else
                checkLoginMethod = LoginChecker.CheckLoginWithoutRegex;

            int minCursorPos = 2;
            int currentCursorPos = 2;
            string login = "";
            bool updateDescription = true;
            bool showErrorMessage = false;
            List<LoginChecker.CheckExtention> checkExt = new List<LoginChecker.CheckExtention>();
            
            bool isCorrectLogin = checkLoginMethod(login, out checkExt);

            // input repeater
            bool editLogin = true;
            do {
                #region Description
                if (updateDescription)
                {
                    Console.Clear();
                    PrintLn("Выберите желаемый логин, учитывая следующие требования:");
                    PrintLnWithColorAndIf(
                        $"- Он должен быть длиной от {LoginChecker.minLength} символов.",
                        ConsoleColor.DarkGreen,
                        checkExt.Contains(LoginChecker.CheckExtention.tooShort) == false);
                    PrintLnWithColorAndIf(
                        $"- Он должен быть длиной до {LoginChecker.maxLength} символов.",
                        ConsoleColor.DarkGreen,
                        checkExt.Contains(LoginChecker.CheckExtention.tooLong) == false);
                    PrintLnWithColorAndIf(
                        "- Содержать только буквы латинского алфавита или цифры.",
                        ConsoleColor.DarkGreen,
                        login.Length > 0 && checkExt.Contains(LoginChecker.CheckExtention.notEnglish) == false);
                    PrintLnWithColorAndIf(
                        "- При этом цифра не может быть первой.",
                        ConsoleColor.DarkGreen,
                        login.Length > 0 && checkExt.Contains(LoginChecker.CheckExtention.firstDigit) == false);
                    
                    if (useRegex)
                    {
                        Print("Проверка С помощью регулярных выражений. ");
                    } else
                    {
                        Print("Проверка БЕЗ помощи регулярных выражений. ");

                    }
                    PrintLn("Нажмите TAB для переключения.");

                    if (showErrorMessage == true)
                    {
                        PrintLnWithColorAndIf("Ваш логин не соответствует всем требованиям! Попробуйте еще раз:", ConsoleColor.Red, true);
                    }
                    else
                    {
                        PrintLn("Введите ваш логин:");
                    }
                    updateDescription = false;
                } 
                else
                {
                    Console.SetCursorPosition(0, Console.CursorTop);
                }
                #endregion

                #region input char
                Print($"> {login}");                                
                int maxCursorPos = minCursorPos + login.Length;
                Console.SetCursorPosition(currentCursorPos, Console.CursorTop);
                bool needCheckLogin = false;
                var input = Console.ReadKey(true);
                switch (input.Key)
                {
                    #region arrows, delete-backspace, home-end
                    case ConsoleKey.Delete:
                        if (currentCursorPos < maxCursorPos)
                        {
                            string firstString = login.Substring(0, currentCursorPos - minCursorPos);
                            string lastString = login.Substring(currentCursorPos - minCursorPos + 1);
                            login = firstString + lastString;
                            needCheckLogin = true;
                        }
                        break;
                    case ConsoleKey.Backspace:
                        if (currentCursorPos > minCursorPos)
                        {
                            string firstString = login.Substring(0, currentCursorPos - minCursorPos - 1);
                            string lastString = login.Substring(currentCursorPos - minCursorPos);
                            login = firstString + lastString;
                            currentCursorPos--;
                            needCheckLogin = true;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (currentCursorPos > minCursorPos)
                            currentCursorPos--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (currentCursorPos < maxCursorPos)
                            currentCursorPos++;
                        break;
                    case ConsoleKey.Home:
                        currentCursorPos = minCursorPos;
                        break;
                    case ConsoleKey.End:
                        currentCursorPos = maxCursorPos;
                        break;
                    #endregion
                    #region tab & enter
                    case ConsoleKey.Tab:
                        useRegex = !useRegex;
                        needCheckLogin = true;
                        break;
                    case ConsoleKey.Enter:
                        if (isCorrectLogin)
                        {
                            editLogin = false;
                            showErrorMessage = false;
                        }
                        else
                        {
                            updateDescription = true;
                            showErrorMessage = true;
                        }
                        break;
                    #endregion
                    #region input char
                    default: // input char
                        if (char.IsLetterOrDigit(input.KeyChar)
                            || char.IsWhiteSpace(input.KeyChar)
                            || char.IsPunctuation(input.KeyChar)
                            || char.IsSeparator(input.KeyChar))// не пропускает № # $ ^ +       не стал вылавливать
                        {
                            string firstString = login.Substring(0, currentCursorPos - minCursorPos);
                            string lastString = login.Substring(currentCursorPos - minCursorPos);
                            login = firstString + input.KeyChar.ToString() + lastString;
                            currentCursorPos++;
                            needCheckLogin = true;
                        }
                        break;
                    #endregion
                }
                #endregion

                #region login check
                if (needCheckLogin == true)
                {

                    if (useRegex)
                        checkLoginMethod = LoginChecker.CheckLoginWithRegex;
                    else
                        checkLoginMethod = LoginChecker.CheckLoginWithoutRegex;

                    isCorrectLogin = checkLoginMethod(login, out checkExt);
                    if (showErrorMessage == true && isCorrectLogin == true)
                        showErrorMessage = false;
                    updateDescription = true;
                }
                #endregion

            } while (editLogin);

            Print("\n\nВаш логин принят. Нажмите любую клавишу для выхода из программы.");
            Console.ReadKey(true);


        }
        static public void PrintLnWithColorAndIf(string text, ConsoleColor color, bool useColor)
        {
            if (useColor)
            {
                ConsoleColor oldColor = Console.ForegroundColor;
                Console.ForegroundColor = color;
                PrintLn(text);
                Console.ForegroundColor = oldColor;
            } else
            {
                PrintLn(text);
            }
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
