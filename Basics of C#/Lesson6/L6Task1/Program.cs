using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// Леонид Васильев
/// 1. Изменить программу вывода функции так, чтобы можно было передавать функции типа double (double,double). 
/// Продемонстрировать работу на функции с функцией a*x^2 и функцией a*sin(x).

namespace L6Task1
{
    public delegate double FunX(double x);
    public delegate double FunAX(double a, double x);

    class Program
    {
        public static void Table(FunX F, double x, double b)
        {
            Console.WriteLine("----- X ----- Y -----");
            while (x <= b)
            {
                Console.WriteLine("| {0,8:0.000} | {1,8:0.000} |", x, F(x));
                x += 1;
            }
            Console.WriteLine("---------------------");
        }
        public static void TableAX(FunAX F, double a, double startX, double stepX, double maxX)
        {
            Console.WriteLine("----- X ----- Y -----");
            double x = startX;
            while (x <= maxX)
            {
                Console.WriteLine("| {0,8:0.000} | {1,8:0.000} |", x, F(a, x));
                x += stepX;
            }
            Console.WriteLine("---------------------");
        }

        // Создаем метод для передачи его в качестве параметра в Table
        public static double MyFunc(double x)
        {
            return x * x * x;
        }
        public static double FuncAXSqrt(double a, double x)
        {
            return a * x * x;
        }
        public static double FuncAsinX(double a, double x)
        {
            return a * Math.Sin(x);
        }



            static void Main(string[] args)
        {
            // Создаем новый делегат и передаем ссылку на него в метод Table
            double a = 5;
            Console.WriteLine($"Таблица функции {a}*x^2:");
            TableAX(FuncAXSqrt, a, 0, 1, 6);

            
            a = 2;
            Console.WriteLine($"\nТаблица функции {a}*sin(x):");
            TableAX(FuncAsinX, a, -Math.PI, Math.PI / 4, Math.PI);

            #region код из примера
            //// Параметры метода и тип возвращаемого значения, должны совпадать с делегатом
            //Table(new FunX(MyFunc), -2, 2);
            //Console.WriteLine("Еще раз та же таблица, но вызов организован по новому");
            //// Упрощение(c C# 2.0).Делегат создается автоматически.            
            //Table(MyFunc, -2, 2);
            //Console.WriteLine("Таблица функции Sin:");
            //Table(Math.Sin, -2, 2);      // Можно передавать уже созданные методы
            //Console.WriteLine("Таблица функции x^2:");
            //// Упрощение(с C# 2.0). Использование анонимного метода
            //Table(delegate (double x) { return x * x; }, 0, 3);
            #endregion

            Console.Write("Нажмите любую клавишу для выхода");
            Console.ReadKey(true);
        }
    }
}
