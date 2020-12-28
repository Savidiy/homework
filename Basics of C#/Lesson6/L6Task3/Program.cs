using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// Леонид Васильев
/// 3. Переделать программу «Пример использования коллекций» для решения следующих задач:
/// а) Подсчитать количество студентов учащихся на 5 и 6 курсах;
/// б) подсчитать сколько студентов в возрасте от 18 до 20 лет на каком курсе учатся (частотный массив);
/// в) отсортировать список по возрасту студента;
/// г) *отсортировать список по курсу и возрасту студента;
/// д) разработать единый метод подсчета количества студентов по различным параметрам выбора с помощью делегата и методов предикатов.

namespace L6Task3
{
    class Student
    {
        public string lastName;
        public string firstName;
        public string university;
        public string faculty;
        public int course;
        public string department;
        public int group;
        public string city;
        int age;

        // Создаем конструктор
        public Student(string firstName, string lastName, string university, string faculty, string department, int course, int age, int group, string city)
        {
            this.lastName = lastName;
            this.firstName = firstName;
            this.university = university;
            this.faculty = faculty;
            this.department = department;
            this.course = course;
            this.age = age;
            this.group = group;
            this.city = city;
        }
    }
    class Program
    {
        static int MyDelegat(Student st1, Student st2)          // Создаем метод для сравнения для экземпляров
        {

            return String.Compare(st1.firstName, st2.firstName);          // Сравниваем две строки
        }
        static void Main(string[] args)
        {
            int bakalavr = 0;
            int magistr = 0;
            List<Student> list = new List<Student>();                             // Создаем список студентов
            DateTime dt = DateTime.Now;
            StreamReader sr = new StreamReader("students_1.csv");
            while (!sr.EndOfStream)
            {
                try
                {
                    string[] s = sr.ReadLine().Split(';');
                    // Добавляем в список новый экземпляр класса Student

                    list.Add(new Student(s[0], s[1], s[2], s[3], s[4], int.Parse(s[6]), int.Parse(s[5]), int.Parse(s[7]), s[8]));
                    // Одновременно подсчитываем количество бакалавров и магистров
                    if (int.Parse(s[6]) < 5) bakalavr++; else magistr++;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Ошибка!ESC - прекратить выполнение программы");
                    // Выход из Main
                    if (Console.ReadKey().Key == ConsoleKey.Escape) return;
                }
            }
            sr.Close();
            list.Sort(new Comparison<Student>(MyDelegat));
            Console.WriteLine("Всего студентов:" + list.Count);
            Console.WriteLine("Магистров:{0}", magistr);
            Console.WriteLine("Бакалавров:{0}", bakalavr);
            //foreach (var v in list) Console.WriteLine(v.firstName);
            Console.WriteLine(DateTime.Now - dt);
            Console.ReadKey();
        }
    }
}
