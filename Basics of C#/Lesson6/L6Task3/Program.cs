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
        public int age;

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

        // предикаты
        public static bool FoundMagistr(Student student)
        {
            if (student.course > 4)
                return true;
            else
                return false;
        }
        public static bool FoundBakalavr (Student student)
        {
            if (student.course < 5)
                return true;
            else
                return false;

        }
        public static bool FoundUnder18Age(Student student)
        {
            if (student.age < 18)
                return true;
            else
                return false;

        }

        // делегаты
        public static bool MyGroupIs(Student student, int value)
        {
            return student.group == value;
        }
        public static bool MyAgeIs(Student student, int value)
        {
            return student.age == value;
        }

        // делегаты для сортировки
        public static int SortByAge(Student st1, Student st2)          // Создаем метод для сравнения для экземпляров
        {
            if (st1.age > st2.age)
                return 1;
            else if (st1.age < st2.age)
                return -1;
            return 0;
        }
        public static int SortByCourseAndAge(Student st1, Student st2)          // Создаем метод для сравнения для экземпляров
        {
            // курс
            if (st1.course > st2.course)
                return 1;
            else if (st1.course < st2.course)
                return -1;
            // возраст
            if (st1.age > st2.age)
                return 1;
            else if (st1.age < st2.age)
                return -1;
            //равенство
            return 0;
        }
    }
    class Program
    {
        delegate bool Finder(Student student, int value);
        static int NumberOfStudens(List<Student> students, Finder finder, int value)
        {
            return NumberOfStudens(students, null, finder, value);
        }
        static int NumberOfStudens(List<Student> students, Predicate<Student> predicate, Finder finder = null, int value = 0)
        {
            int count = 0;
            foreach (var st in students)
            {
                bool ok = true;
                if (finder != null && finder(st, value) == false)
                    ok = false;
                if (predicate != null && predicate(st) == false)
                    ok = false;
                if (ok)
                    count++;
            }
            return count;
        }

        static void Main(string[] args)
        {

            #region чтение списка и сбор статистики
            List<Student> list = new List<Student>();  // Создаем список студентов
            Dictionary<int, int> courseWith18_20 = new Dictionary<int, int>();
            int maxNameLength = 0;
            DateTime dt = DateTime.Now;
            StreamReader sr = new StreamReader("students_1.csv");
            while (!sr.EndOfStream)
            {
                try
                {
                    string[] s = sr.ReadLine().Split(new char[]{ ';',','});
                    // Добавляем в список новый экземпляр класса Student

                    string lastName = s[1];
                    string firstName = s[0];
                    string university = s[2];
                    string faculty = s[3];
                    int course = int.Parse(s[6]);
                    string department = s[4];
                    int group = int.Parse(s[7]);
                    string city = s[8];
                    int age = int.Parse(s[5]);

                    // считаем 18-20 летних студентов
                    if (age >= 18 && age <= 20)
                    {
                        if (courseWith18_20.ContainsKey(course))
                        {
                            courseWith18_20[course]++;
                        } else
                        {
                            courseWith18_20.Add(course, 1);
                        }
                    }

                    // максимальная длина фамилии и имени для форматированного вывода
                    int length = lastName.Length + firstName.Length + 2;
                    if (maxNameLength < length)
                        maxNameLength = length;

                    list.Add(new Student(firstName, lastName, university, faculty, department, course, age, group, city));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Ошибка!ESC - прекратить выполнение программы");
                    // Выход из Main
                    if (Console.ReadKey(true).Key == ConsoleKey.Escape) return;
                }
            }
            sr.Close();
            #endregion

            #region количество студентов с помощью делегатов и предикатов   
            Console.WriteLine("Всего студентов:" + list.Count);

            Console.WriteLine("а)");
            int course5 = NumberOfStudens(list, delegate (Student st){ return st.course == 5; });
            Console.WriteLine($"На пятом курсе учатся {course5} студентов.");
            int course6 = NumberOfStudens(list, delegate (Student st) { return st.course == 6; });
            Console.WriteLine($"На шестом курсе учатся {course6} студентов.");

            Console.WriteLine("б)");
            Console.WriteLine("Обратим внимание на студентов в возрасте от 18 до 20 лет.");
            foreach (var pair in courseWith18_20.OrderBy(p => p.Key))
            {
                Console.WriteLine($"На {pair.Key} курсе таких учится {pair.Value}.");
            }

            Console.WriteLine("д)");
            int newborn = NumberOfStudens(list, Student.FoundUnder18Age);
            Console.WriteLine($"Количество несовершеннолетних студентов равно {newborn}.");
            int age20 = NumberOfStudens(list, Student.MyAgeIs, 20);
            Console.WriteLine($"Количество двадцатилетних студентов равно {age20}.");
            int group3 = NumberOfStudens(list, Student.FoundBakalavr, Student.MyGroupIs, 3);
            Console.WriteLine($"Количество бакалавров из третьих групп равно {group3}.");
            #endregion

            #region сортировки
            Console.WriteLine();
            Console.WriteLine("в)");
            Console.Write("Нажмите любую клавишу, чтобы отсортировать список по возрасту студентов:");
            Console.ReadKey(true);
            Console.WriteLine();
            list.Sort(new Comparison<Student>(Student.SortByAge));
            foreach (var v in list) {
                Console.Write($"{v.firstName} {v.lastName}");
                Console.SetCursorPosition(maxNameLength, Console.CursorTop);
                Console.WriteLine($"возраст: {v.age}");
            }
            Console.WriteLine();
            Console.WriteLine("г)");
            Console.Write("Нажмите любую клавишу, чтобы отсортировать список по группе и возрасту:");
            Console.ReadKey(true);
            Console.WriteLine();
            list.Sort(new Comparison<Student>(Student.SortByCourseAndAge));
            foreach (var v in list)
            {
                Console.Write($"{v.firstName} {v.lastName}");
                Console.SetCursorPosition(maxNameLength, Console.CursorTop);
                Console.WriteLine($"курс: {v.course} \tвозраст: {v.age}");
            }
            #endregion

            // happy end
            Console.WriteLine();
            Console.WriteLine($"Нажмите любую клавишу");
            Console.ReadKey();
        }
    }
}
