using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Леонид Васильев
// 1. а) Дописать структуру Complex, добавив метод вычитания комплексных чисел. Продемонстрировать работу структуры;
// б) Дописать класс Complex, добавив методы вычитания и произведения чисел. Проверить работу класса;

namespace L3Task1
{
    struct ComplexStr
    {
        public double _re;
        public double _im;

        public ComplexStr(double re, double im)
        {
            _re = re;
            _im = im;
        }

        public ComplexStr Plus (ComplexStr a)
        {
            ComplexStr r;
            r._re = _re + a._re;
            r._im = _im + a._im;
            return r;
        }
        public ComplexStr Minus (ComplexStr a)
        {
            ComplexStr r;
            r._re = _re - a._re;
            r._im = _im - a._im;
            return r;
        }
        public ComplexStr Multi (ComplexStr a)
        {
            ComplexStr r;
            r._re = _re * a._re - _im * a._im;
            r._im = _re * a._im + _im * a._re;
            return r;
        }

        public override string ToString()
        {
            string t = "";
            if (_re != 0)
            {
                t += _re;
            } 
            else
            {
                if (_im != 0)
                {
                    return $"{_im}i";
                } 
                else
                {
                    return "0";
                }                        
            }

            if (_im != 0)
            {
                t += (_im > 0) ? $"+{_im}i" : $"-{-_im}i";
            }

            return t;
        }
    }

    class ComplexCl
    {
        private double _re;
        private double _im;

        public double Re
        {
            get { return _re; }
            set { _re = value; }
        }
        public double Im
        {
            get { return _im; }
            set { _im = value; }
        }

        public ComplexCl()
        {
            _re = _im = 0;
        }
        public ComplexCl(double re, double im)
        {
            _re = re;
            _im = im;
        }

        public ComplexCl Plus(ComplexCl a)
        {                
            return new ComplexCl(_re + a._re, _im + a._im);
        }
        public ComplexCl Minus(ComplexCl a)
        {
            return new ComplexCl(_re - a._re, _im - a._im);
        }
        public ComplexCl Multi(ComplexCl a)
        {
            return new ComplexCl(_re * a._re - _im * a._im, _re * a._im + _im * a._re);
        }

        public override string ToString()
        {
            string t = "";
            if (_re != 0) // ex.: 7...
            {
                t += _re;
            }
            else
            {
                if (_im != 0) // ex.: 5i or -5i
                {
                    return $"{_im}i";
                }
                else // ex.: 0
                {
                    return "0";
                }
            }

            if (_im != 0) // ex.: 7+5i or 7-5i
            {
                t += (_im > 0) ? $"+{_im}i" : $"{_im}i";
            }

            return t;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Демонстрация работы структуры и класса комплексного числа ===");
                Console.WriteLine("Введите два комплексных числа.");
                Console.Write("Введите действительную часть первого числа: ");
                double.TryParse(Console.ReadLine(), out double re1);
                Console.Write("Введите мнимую часть первого числа: ");
                double.TryParse(Console.ReadLine(), out double im1);
                Console.Write("Введите действительную часть второго числа: ");
                double.TryParse(Console.ReadLine(), out double re2);
                Console.Write("Введите мнимую часть второго числа: ");
                double.TryParse(Console.ReadLine(), out double im2);

                ComplexStr complexStruct1 = new ComplexStr(re1, im1);
                ComplexStr complexStruct2 = new ComplexStr(re2, im2);
                ComplexCl complexClass1 = new ComplexCl(re1, im1);
                ComplexCl complexClass2 = new ComplexCl(re2, im2);

                Console.WriteLine("");
                Console.WriteLine("Вы задали два комплексных числа.");
                Console.WriteLine("Выведем их на экран:");
                Console.WriteLine($"Классы:\t\t{complexClass1}\t\t{complexClass2}");
                Console.WriteLine($"Структуры:\t{complexStruct1}\t\t{complexStruct2}");

                Console.WriteLine("\nСумма этих чисел:");
                Console.WriteLine($"Классы:\t\t{complexClass1.Plus(complexClass2)}");
                Console.WriteLine($"Структуры:\t{complexStruct1.Plus(complexStruct2)}");

                Console.WriteLine("\nРазность этих чисел:");
                Console.WriteLine($"Классы:\t\t{complexClass1.Minus(complexClass2)}");
                Console.WriteLine($"Структуры:\t{complexStruct1.Minus(complexStruct2)}");

                Console.WriteLine("\nПроизведение этих чисел:");
                Console.WriteLine($"Классы:\t\t{complexClass1.Multi(complexClass2)}");
                Console.WriteLine($"Структуры:\t{complexStruct1.Multi(complexStruct2)}");

                Console.Write("\nДля ввода новой пары чисел нажмите любую клавишу");
                Console.ReadKey();                
            }
        }
    }
}
