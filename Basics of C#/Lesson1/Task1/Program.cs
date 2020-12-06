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
            Console.ReadKey();
            #endregion

            #region Task 2
            //2.Ввести вес и рост человека. Рассчитать и вывести индекс массы тела(ИМТ) по формуле I = m / (h * h); где m — масса тела в килограммах, h — рост в метрах


            #endregion

        }
    }
}
