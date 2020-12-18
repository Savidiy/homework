using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// Леонид Васильев
/// 3. Решить задачу с логинами из предыдущего урока, только логины и пароли считать из файла в массив. Создайте структуру Account, содержащую Login и Password.
/// Урок 2
/// 4. Реализовать метод проверки логина и пароля. На вход подается логин и пароль. 
/// На выходе истина, если прошел авторизацию, и ложь, если не прошел (Логин: root, Password: GeekBrains).
/// Используя метод проверки логина и пароля, написать программу: пользователь вводит логин и пароль, программа пропускает его дальше или не пропускает. 
/// С помощью цикла do while ограничить ввод пароля тремя попытками.

namespace L4Task3
{
    class Program
    {
        struct Account
        {
            string _login;
            string _password;
            public Account(string login, string password)
            {
                _login = login;
                _password = password;
            }

            public bool Checkout(string login, string password)
            {
                return (_login == login && _password == password);
            }
        }

        static Account[] accounts;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                string filename = "passwords.txt";
                Console.WriteLine($"Загружаем пароли из файла {filename}.");
                StreamReader str = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + filename);
                int num = int.Parse(str.ReadLine());
                accounts = new Account[num];

                for (int i = 0; i < num; i++)
                {
                    var pair = str.ReadLine().Split(new char[] { ' ' });
                    accounts[i] = new Account(pair[0], pair[1]);
                }
                Console.WriteLine($"Загружено {num} аккаунтов.\n");

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

                    if (CheckoutAll(login, pass))
                    {
                        checkout = true;
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine($" Неверный логин или пароль. Осталось попыток: {maxCount - count}\n");
                    }
                    Pause(500);

                } while (count < maxCount && checkout == false);

                if (checkout)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Добро пожаловать на rutracker.org");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Вы вввели неправильные данные {maxCount} раз. Это очень подозрительно. Приходите завтра.");
                }

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($"Нажмите любую клавишу для повтора");
                Console.ReadKey();
            }
        }

        static bool CheckoutAll(string login, string pass)
        {
            foreach (var ac in accounts)
            {
                if (ac.Checkout(login, pass))
                {
                    return true;
                }
            }
            return false;
        }

        static void Pause(int millsec)
        {
            System.Threading.Thread.Sleep(millsec);
        }
    }
}
