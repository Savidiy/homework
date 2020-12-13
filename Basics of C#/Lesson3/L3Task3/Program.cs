using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// Леонид Васильев
/// 3. *Описать класс дробей - рациональных чисел, являющихся отношением двух целых чисел. 
/// Предусмотреть методы сложения, вычитания, умножения и деления дробей. Написать программу, демонстрирующую все разработанные элементы класса.
/// **Добавить проверку, чтобы знаменатель не равнялся 0. Выбрасывать исключение
/// ArgumentException("Знаменатель не может быть равен 0");
/// Добавить упрощение дробей.

namespace L3Task3
{
    class Fraction
    {
        int _numerator = 0;
        int _denominator = 1;

        public int Numerator
        {
            get { return _numerator; }
            set { 
                _numerator = value; 
            }
        }
        public int Denominator
        {
            get { return _denominator; }
            set
            {
                if (value != 0)
                {
                    if (value > 0)
                    {
                        _denominator = value;
                    }
                    else
                    {
                        _denominator = -value;
                        _numerator = -_numerator;
                    }
                }
                else
                {
                    throw new ArgumentException("Знаменатель не может быть равен 0");
                }
            }
        }

        public Fraction(): this(0, 1) { }
        public Fraction(int integer) : this(integer, 1) { }
        public Fraction(int numerator, int denominator)
        {
            _numerator = numerator;
            Denominator = denominator;
            Simplify();
        }

        public Fraction Plus(Fraction a)
        {
            return new Fraction(Numerator * a.Denominator +  a.Numerator * Denominator, Denominator * a.Denominator);
        }
        public Fraction Minus (Fraction a)
        {
            return new Fraction(Numerator * a.Denominator - a.Numerator * Denominator, Denominator * a.Denominator);
        }
        public Fraction Multiply(Fraction a)
        {
            return new Fraction(Numerator * a.Numerator, Denominator * a.Denominator);
        }
        public Fraction Divide(Fraction a)
        {
            return new Fraction(Numerator * a.Denominator, Denominator * a.Numerator);
        }

        void Simplify()
        {
            if (_numerator == 0)
            {
                Denominator = 1;
            }
            else
            {
                int i = 2;
                int n = _numerator > 0 ? _numerator : -_numerator;
                int min = (n < _denominator) ? n : _denominator;
                while (i <= min)
                {
                    if (_numerator % i == 0 && _denominator % i == 0)
                    {
                        _numerator = _numerator / i;
                        _denominator = _denominator / i;
                        min = min / i;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        public override string ToString()
        {
            Simplify();
            if (_denominator == 1)
                return $"{_numerator}";
            else 
                return $"{_numerator}/{_denominator}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("=== Демонстрация работы с классом дробей ===");
                Console.Write("Введите числитель первой дроби: ");
                int.TryParse(Console.ReadLine(), out int n1);
                Console.Write("Введите знаменатель первой дроби: ");
                int.TryParse(Console.ReadLine(), out int d1);
                Console.Write("Введите числитель второй дроби: ");
                int.TryParse(Console.ReadLine(), out int n2);
                Console.Write("Введите знаменатель второй дроби: ");
                int.TryParse(Console.ReadLine(), out int d2);

                Fraction f1 = new Fraction(n1, d1);
                Fraction f2 = new Fraction(n2, d2);

                Console.WriteLine();
                Console.WriteLine($"Первая дробь равна {f1}.");
                Console.WriteLine($"Вторая дробь равна {f2}.");
                Console.WriteLine($"Их сумма равна {f1.Plus(f2)}.");
                Console.WriteLine($"Их разность равна {f1.Minus(f2)}.");
                Console.WriteLine($"Их произведение равно {f1.Multiply(f2)}.");
                Console.WriteLine($"Их частное равно {f1.Divide(f2)}.");

                Console.Write("\nНажмите любую клавишу для повтора");
                Console.ReadKey();
            }
        }
    }
}
