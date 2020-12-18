using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// Леонид Васильев
/// 2. а) Дописать класс для работы с одномерным массивом. 
/// Реализовать конструктор, создающий массив заданной размерности и заполняющий массив числами от начального значения с заданным шагом. 
/// Создать свойство Sum, которые возвращают сумму элементов массива, 
/// метод Inverse, меняющий знаки у всех элементов массива, 
/// метод Multi, умножающий каждый элемент массива на определенное число, 
/// свойство MaxCount, возвращающее количество максимальных элементов. 
/// В Main продемонстрировать работу класса.
/// б)Добавить конструктор и методы, которые загружают данные из файла и записывают данные в файл.


namespace L4Task2
{   class MyArray
    {
        int[] _a;

        public MyArray(int[] a)
        {
            _a = a;
        }
        public MyArray(int count, int start, int step)

        {
            _a = new int[count];
            int sum = start;
            for (int i = 0; i < count; i++)
            {
                _a[i] = sum;
                sum += step;
            }
        }
        public MyArray(string filename, out int errorCount)
        {
            string text = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + filename);
            var strArray = text.Split(new char[]{' ',',','.','\r','\n'},StringSplitOptions.RemoveEmptyEntries);

            _a = new int[strArray.Length];

            errorCount = 0;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (int.TryParse(strArray[i], out int num))
                    _a[i] = num;
                else
                    errorCount++;
            }
        }

        public void PrintToFile(string filename)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + filename;
            string contents = "";
            for (int i = 0; i < _a.Length; i++)
            {
                contents += _a[i] + " ";
            }
            File.WriteAllText(path, contents);
        }
        public int Sum()
        {
            int sum = 0;
            foreach(var i in _a)
            {
                sum += i;
            }
            return sum;
        }
        public void Inverse()
        {
            for (int i = 0; i < _a.Length; i++)
            {
                _a[i] = -_a[i];
            }
        }
        public void Multi(int multiplier)
        {
            for (int i = 0; i < _a.Length; i++)
            {
                _a[i] = _a[i] * multiplier;
            }
        }
        public int Max()
        {
            int max = int.MinValue;
            for (int i = 0; i < _a.Length; i++)
            {
                if (_a[i] > max)
                    max = _a[i];
            }
            return max;
        }
        public int MaxCount()
        {
            int max = Max();
            int count = 0;
            for (int i = 0; i < _a.Length; i++)
            {
                if (_a[i] == max)
                    count++;
            }
            return count;
        }
        public int[] A
        {
            get { return _a; }
        }

        public int this[int i]
        {
            get
            {
                return _a[i];
            }
            set
            {
                _a[i] = value;
            }
        }
        public override string ToString()
        {
            string text = "";
            foreach(var i in _a)
            {
                text += i + " ";
            }
            return text;
        }
    }

    class Program
    {
        static void Main(string[] args)
        { 
            int count = 10;
            int start = -21;
            int step = 7;
            Console.WriteLine($"Создаем массив из {count} элементов, начиная с {start} и дальше с шагом {step}.");
            MyArray array = new MyArray(count, start, step);
            Console.WriteLine("Получившийся массив:");
            Console.WriteLine(array);
            Console.WriteLine($"Сумма получившихся элементов {array.Sum()}.");
            Console.WriteLine($"Максимальное значение в массиве {array.Max()}, таких значений {array.MaxCount()}.");
            Console.WriteLine();
            
            string inputFile = "ArrayInput.txt";
            string outputFile = "ArrayOutput.txt";
            Console.WriteLine($"Загружаем массив из файла {inputFile}.");            
            MyArray arrayFromFile = new MyArray(inputFile, out int errorCount);
            if (errorCount > 0)
                Console.WriteLine($"Массив загружен с {errorCount} ошибками.");
            else
                Console.WriteLine("Массив загружен без ошибок.");
            Console.WriteLine("Загруженный массив:");
            Console.WriteLine(arrayFromFile);
            Console.WriteLine($"Максимальное значение в массиве {arrayFromFile.Max()}, таких значений {arrayFromFile.MaxCount()}.");
            Console.WriteLine("Инвертируем массив:");
            arrayFromFile.Inverse();
            Console.WriteLine(arrayFromFile);
            int multiplier = 3;
            Console.WriteLine($"Умножаем все элементы массива на {multiplier}:");
            arrayFromFile.Multi(multiplier);
            Console.WriteLine(arrayFromFile);
            Console.WriteLine($"Сохраняем этот массив в файл {outputFile}.") ;
            arrayFromFile.PrintToFile(outputFile);

            Console.Write("\nНажмите любую клавишу");
            Console.ReadKey();


        }
    }
}
