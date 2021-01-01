using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L8Task3
{
    [Serializable]
    public class Student
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

        public Student()
        {
        }

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

        static public List<Student> LoadStudentsFromCSV(string filename)
        {
            List<Student> list = new List<Student>();  // Создаем список студентов
            StreamReader sr = new StreamReader(filename);
            while (!sr.EndOfStream)
            {
                try
                {
                    string[] s = sr.ReadLine().Split(new char[] { ';', ',' });
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

                    list.Add(new Student(firstName, lastName, university, faculty, department, course, age, group, city));
                }
                catch
                {
                    ;
                }
            }
            sr.Close();
            return list;
        }
    }
}
