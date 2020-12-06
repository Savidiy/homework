using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Васильев Леонид

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Task 1
            //1.Написать программу «Анкета». Последовательно задаются вопросы (имя, фамилия, возраст, рост, вес). В результате вся информация выводится в одну строчку.
            //а) используя склеивание;
            //б) используя форматированный вывод;
            //в) *используя вывод со знаком $.

            Console.WriteLine("=Анкета=");
            Console.Write("Введите ваше имя:\n>");
            string name = Console.ReadLine();
            Console.Write("Введите вашу фамилию:\n>");
            string lastname = Console.ReadLine();
            Console.Write("Введите ваш возраст:\n>");
            string age = Console.ReadLine();
            Console.Write("Введите ваш рост в сантиметрах:\n>");
            string heigth = Console.ReadLine();
            Console.Write("Введите ваш вес в килограммах:\n>");
            string weigth = Console.ReadLine();

            Console.WriteLine();
            Console.Write("Вас зовут " + name + " " + lastname + ".");
            Console.Write(" Ваш возраст {0}.", age);
            Console.WriteLine($" Ваш рост {heigth} см. Ваш вес {weigth} кг.");
            #endregion

            #region Task 2
            //2.Ввести вес и рост человека. Рассчитать и вывести индекс массы тела(ИМТ) по формуле I = m / (h * h); где m — масса тела в килограммах, h — рост в метрах
            float m = float.Parse(weigth);
            float h = float.Parse(heigth) / 100;//конвертируем см в метры
            float imt = m / (h * h);
            Console.WriteLine($"Ваш индекс массы тела (ИМТ) равен {imt:F1}.") ;
            #endregion

            #region Task 3
            //3.
            //а) Написать программу, которая подсчитывает расстояние между точками с координатами x1, y1 и x2,y2 по формуле r = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2).Вывести результат, используя спецификатор формата .2f(с двумя знаками после запятой);
            //б) *Выполните предыдущее задание, оформив вычисления расстояния между точками в виде метода;
            #endregion


            Console.ReadKey();

        }
    }
}
