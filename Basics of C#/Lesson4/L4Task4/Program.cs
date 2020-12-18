using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// Леонид Васильев
/// 4. *а) Реализовать класс для работы с двумерным массивом. 
/// Реализовать конструктор, заполняющий массив случайными числами. 
/// Создать методы, которые возвращают сумму всех элементов массива, 
/// сумму всех элементов массива больше заданного, 
/// свойство, возвращающее минимальный элемент массива, 
/// свойство, возвращающее максимальный элемент массива, 
/// метод, возвращающий номер максимального элемента массива (через параметры, используя модификатор ref или out)
/// *б) Добавить конструктор и методы, которые загружают данные из файла и записывают данные в файл.
/// Дополнительные задачи
/// в) Обработать возможные исключительные ситуации при работе с файлами.

namespace L4Task4
{
    class Program
    {

        class TwoDimArray
        {
            int[,] _array;
            int _sizeX;
            int _sizeY;
            public int SizeX { get { return _sizeX; } }
            public int SizeY { get { return _sizeY; } }
            
            public TwoDimArray(string fileName)
            {
                _array = new int[1, 1];

                string path = AppDomain.CurrentDomain.BaseDirectory + fileName;
                try
                {
                    StreamReader sr = new StreamReader(path);
                    int column = int.Parse(sr.ReadLine());
                    int row = int.Parse(sr.ReadLine());
                    _sizeX = column;
                    _sizeY = row;
                    _array = new int[column, row];

                    for (int r = 0; r < row; r++)
                    {
                        string line = sr.ReadLine();
                        var div = line.Split(new char[]{' '});
                        for (int c = 0; c < column; c++)
                        {
                            _array[c, r] = int.Parse(div[c]);
                        }
                    }                    
                    sr.Close();
                }
                catch (Exception exc)
                {
                    PrintLn($"Ошибка при загрузке файла:\n{path}.\n{exc.Message}");
                }
            }
            public TwoDimArray(int sizeX, int sizeY, bool randomContent = false)
            {
                if (_sizeX < 1 || _sizeY < 1)
                    throw new System.ArgumentOutOfRangeException($"Размерность массива должна быть больше 0. X={sizeX}, Y={sizeY}.");
                _sizeX = sizeX;
                _sizeY = sizeY;
                _array = new int[_sizeX, _sizeY];
                if (randomContent)
                {
                    Random rnd = new Random();
                    for (int x = 0; x < _sizeX; x++)
                    {
                        for (int y = 0; y < _sizeY; y++)
                        {
                            _array[x, y] = rnd.Next(int.MinValue, int.MaxValue);
                        }
                    }
                }
            }
            public TwoDimArray(int sizeX, int sizeY, int minValue, int maxValue)
            {
                if (sizeX < 1 || sizeY < 1)
                    throw new System.ArgumentOutOfRangeException($"Размерность массива должна быть больше 0. X={sizeX}, Y={sizeY}.");
                _sizeX = sizeX;
                _sizeY = sizeY;
                _array = new int[_sizeX, _sizeY];
                Random rnd = new Random();
                for (int x = 0; x < _sizeX; x++)
                {
                    for (int y = 0; y < _sizeY; y++)
                    {
                        _array[x, y] = rnd.Next(minValue, maxValue);
                    }
                }
            }
            public void GetIndexOfMax(out int findX, out int findY)
            {
                findX = 0;
                findY = 0;
                int max = int.MinValue;
                for (int x = 0; x < _sizeX; x++)
                {
                    for (int y = 0; y < _sizeY; y++)
                    {
                        if (_array[x, y] > max)
                        {
                            findX = x;
                            findY = y;
                            max = _array[x, y];
                        }
                    }
                }
            }
            public long GetSum()
            {
                long sum = 0;
                for (int x = 0; x < _sizeX; x++)
                {
                    for (int y = 0; y < _sizeY; y++)
                    {
                        sum += _array[x, y];
                    }
                }
                return sum;
            }
            public long GetSumOver(int minValue)
            {
                long sum = 0;
                for (int x = 0; x < _sizeX; x++)
                {
                    for (int y = 0; y < _sizeY; y++)
                    {
                        if (_array[x, y] > minValue)
                            sum += _array[x, y];
                    }
                }
                return sum;
            }
            public void SaveToFile(string fileName)
            {
                string t = $"{_sizeX}\n{_sizeY}\n";
                for (int y = 0; y < _sizeY; y++) 
                {
                    for (int x = 0; x < _sizeX; x++)
                    {
                       t += _array[x, y] + " ";
                    }
                    t += "\n";
                }
                string path = AppDomain.CurrentDomain.BaseDirectory + fileName;
                try
                {
                    File.WriteAllText(path, t);
                }
                catch (Exception exc)
                {
                    PrintLn($"Ошибка записи массива в файл:\n{path}\n{exc.Message}");
                }
            }
            public int Min
            {
                get
                {
                    int min = int.MaxValue;
                    for (int x = 0; x < _sizeX; x++)
                    {
                        for (int y = 0; y < _sizeY; y++)
                        {
                            if (_array[x, y] < min)
                                min = _array[x, y];
                        }
                    }
                    return min;
                }
            }
            public int Max
            {
                get
                {
                    int max = int.MinValue;
                    for (int x = 0; x < _sizeX; x++)
                    {
                        for (int y = 0; y < _sizeY; y++)
                        {
                            if (_array[x, y] > max)
                                max = _array[x, y];
                        }
                    }
                    return max;
                }
            }
            public int this[int x, int y]
            {
                get {
                    if (x >= 0 && x < _sizeX && y >= 0 && y < _sizeY)
                        return _array[x, y];
                    else
                        throw new System.ArgumentOutOfRangeException($"Аргументы должны быть неотрицательными. X={x} меньше {_sizeX}, Y={y} меньше {_sizeY}.");
                }
                set {
                    if (x >= 0 && x < _sizeX && y >= 0 && y < _sizeY)
                        _array[x, y] = value;
                    else
                        throw new System.ArgumentOutOfRangeException($"Аргументы должны быть неотрицательными. X={x} меньше {_sizeX}, Y={y} меньше {_sizeY}.");
                }
            }

        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                int sizeX = 10;
                int sizeY = 3;
                int minValue = -100;
                int maxValue = 100;
                PrintLn($"Создaем массив размером {sizeX} на {sizeY}, заполненный случайными числами от {minValue} до {maxValue}:");
                TwoDimArray arrayRnd = new TwoDimArray(sizeX, sizeY, minValue, maxValue);
                arrayRnd.GetIndexOfMax(out int indexMaxX, out int indexMaxY);
                for (int y = 0; y < sizeY; y++) 
                {
                    for (int x = 0; x < sizeX; x++)
                    {
                        if (x == indexMaxX && y == indexMaxY)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Print(String.Format("{0,4:0.#}", arrayRnd[x, y]) + " ");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        } else
                            Print(String.Format("{0,4:0.#}", arrayRnd[x, y]) + " ");
                    }
                    PrintLn();
                }
                PrintLn($"Самое большое число в массиве {arrayRnd.Max} расположено в координатах [{indexMaxX},{indexMaxY}].");
                PrintLn($"Самое маленькое число в массиве {arrayRnd.Min}.");
                PrintLn($"Сумма всех элементов массива равна {arrayRnd.GetSum()}.");
                int border = 90;
                PrintLn($"Сумма всех элементов массива больше {border} равна {arrayRnd.GetSumOver(border)}.");
                string outputFileName = "ArrayOutput.txt";                
                PrintLn($"Попробуем сохранить массив в файл {outputFileName}.");
                arrayRnd.SaveToFile(outputFileName);
                PrintLn($"Попробуем загрузить массив обратно из файла {outputFileName}.");
                arrayRnd = new TwoDimArray(outputFileName);
                PrintLn();
                PrintLn($"Попробуем сохранить массив в файл без имени:");
                arrayRnd.SaveToFile("");

                PrintLn();
                string inputFileName = "ArrayInput.txt";
                PrintLn($"Попробуем загрузить массив из файла {inputFileName}:");
                TwoDimArray arrayFromFile = new TwoDimArray(inputFileName);                
                for (int y = 0; y < arrayFromFile.SizeY; y++)
                {
                    for (int x = 0; x < arrayFromFile.SizeX; x++)
                    {
                        Print(String.Format("{0,4:0.#}", arrayFromFile[x, y]) + " ");
                    }
                    PrintLn();
                }

                Print("\nНажмите любую клавишу для повтора");
                Console.ReadKey();
            }
        }
        static public void Print(string t)
        {
            Console.Write(t);
        }
        static public void PrintLn()
        {
            Console.WriteLine();
        }
        static public void PrintLn(string t)
        {
            Console.WriteLine(t);
        }
    }
}
