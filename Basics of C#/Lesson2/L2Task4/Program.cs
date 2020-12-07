using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Васильев Леонид
// 4. Реализовать метод проверки логина и пароля. На вход подается логин и пароль. 
// На выходе истина, если прошел авторизацию, и ложь, если не прошел (Логин: root, Password: GeekBrains). 
// Используя метод проверки логина и пароля, написать программу: 
// пользователь вводит логин и пароль, программа пропускает его дальше или не пропускает. 
// С помощью цикла do while ограничить ввод пароля тремя попытками.

namespace L2Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            int maxCount = 3;
            bool checkout = false;
            do
            {
                Console.Write(" Введите логин:\n>");
                string login = Console.ReadLine();
                Console.Write(" Введите пароль:\n>");
                string pass = Console.ReadLine();
                count++;

                if (Checkout(login, pass))
                {
                    checkout = true;
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($" Неверный логин или пароль.\n Осталось попыток: {maxCount - count}\n");                    
                }
                Pause(500);

            } while (count < maxCount && checkout == false);

            if (checkout)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Добро пожаловать на rutracker.org");
            } else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Вы вввели неправильные данные {maxCount} раза. Это очень подозрительно. Приходите завтра.");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Нажмите любую клавишу");
            Console.ReadKey();
        }

        static bool Checkout(string login, string pass)
        {
            if (login == "root" && pass == "GeekBrains")
            {
                return true;
            }
            return false;
        }

        static void Pause(int millsec)
        {
            System.Threading.Thread.Sleep(millsec);
        }
    }
}
