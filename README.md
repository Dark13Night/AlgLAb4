# AlgLAb4
# Лабораторная работа №4
«Массивы, интерфейсы IComparable<T> и IEnumerator<T>, оператор yield»

# Цели работы:
Научиться синтаксису и принципам работы с массивами средствами языка C#.
Научиться реализовывать интерфейсы IComparable<T> и IEnumerator<T>.
Получить практические навыки работы с оператором yield.

# Задание№1
Создайте класс MyMatrix, представляющий матрицу m на n.
Создайте конструктор, принимающий число строк и столбцов, заполняющий матрицу случайными числами в диапазоне, который пользователь вводит при запуске программы.
Определите операторы сложения, вычитания и умножения матриц, а также умножения и деления матрицы на число.
Создайте пользовательский индексатор матрицы для доступа к элементам матрицы по номеру строки и столбца.

```
public class MyMatrix
    {
    public int[ ,] matrix;
    public int rows;
    public int cols;
public MyMatrix(int m, int n)
    {
        rows = m;
        cols = n;
        matrix = new int[rows, cols];
        Random random = new Random();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = random.Next();
            }
        }
    }
    public MyMatrix(int m, int n, int minValue, int maxValue)
    {
        rows = m;
        cols = n;
        matrix = new int[rows, cols];
        Random random = new Random();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = random.Next(minValue, maxValue);
            }
        }
    }


    public int this[int i, int j]
    {
        get { return matrix[i, j]; }
        set { matrix[i, j] = value; }
    }

    public static MyMatrix operator +(MyMatrix matrix1, MyMatrix matrix2)
    {
        if (matrix1.rows != matrix2.rows || matrix1.cols != matrix2.cols)
        {
            throw new Exception("Matrices must have the same dimensions!");
        }

        MyMatrix result = new MyMatrix(matrix1.rows, matrix1.cols);
        for (int i = 0; i < result.rows; i++)
        {
            for (int j = 0; j < result.cols; j++)
            {
                result[i, j] = matrix1[i, j] + matrix2[i, j];
            }
        }

        return result;
    }

    public static MyMatrix operator -(MyMatrix matrix1, MyMatrix matrix2)
    {
        if (matrix1.rows != matrix2.rows || matrix1.cols != matrix2.cols)
        {
            throw new Exception("Matrices must have the same dimensions!");
        }

        MyMatrix result = new MyMatrix(matrix1.rows, matrix1.cols);
        for (int i = 0; i < result.rows; i++)
        {
            for (int j = 0; j < result.cols; j++)
            {
                result[i, j] = matrix1[i, j] - matrix2[i, j];
            }
        }

        return result;
    }

    public static MyMatrix operator *(MyMatrix matrix1, MyMatrix matrix2)
    {
        if (matrix1.cols != matrix2.rows)
        {
            throw new Exception("Invalid matrix dimensions for multiplication!");
        }

        MyMatrix result = new MyMatrix(matrix1.rows, matrix2.cols, 0, 0);
        for (int i = 0; i < matrix1.rows; i++)
        {
            for (int j = 0; j < matrix2.cols; j++)
            {
                for (int k = 0; k < matrix1.cols; k++)
                {
                    result[i, j] += matrix1[i, k] * matrix2[k,j];
                }
            }
        }

        return result;
    }

    public static MyMatrix operator *(MyMatrix matrix, int scalar)
    {
        MyMatrix result = new MyMatrix(matrix.rows, matrix.cols);
        for (int i = 0; i < result.rows; i++)
        {
            for (int j = 0; j < result.cols; j++)
            {
                result[i, j] = matrix[i, j] * scalar;
            }
        }

        return result;
    }

    public static MyMatrix operator /(MyMatrix matrix, int scalar)
    {
        MyMatrix result = new MyMatrix(matrix.rows, matrix.cols);
        for (int i = 0; i < result.rows; i++)
        {
            for (int j = 0; j < result.cols; j++)
            {
                result[i, j] = matrix[i, j] / scalar;
            }
        }

        return result;
    }
   
}
public class Program
{
    public static void PrintMatrix(MyMatrix matrix)
    {
        for (int i = 0; i < matrix.rows; i++)
        {
            for (int j = 0; j < matrix.cols; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
    static void Main()
    {
        MyMatrix matrix1 = new MyMatrix(2, 2, 1, 100);
        MyMatrix matrix2 = new MyMatrix(2, 2, 1, 100);

        // Печатаем исходные матрицы
        Console.WriteLine("Matrix 1:");
        PrintMatrix(matrix1);

        Console.WriteLine("Matrix 2:");
        PrintMatrix(matrix2);

        // Складываем матрицы
        MyMatrix sumMatrix = matrix1 + matrix2;
        Console.WriteLine("Sum:");
        PrintMatrix(sumMatrix);

        // Вычитаем матрицы
        MyMatrix diffMatrix = matrix1 - matrix2;
        Console.WriteLine("Difference:");
        PrintMatrix(diffMatrix);

        // Умножаем матрицы
        MyMatrix multiplyMatrix = matrix1 * matrix2;
        Console.WriteLine("Multiplication:");
        PrintMatrix(multiplyMatrix);

        // Умножаем матрицу на скаляр
        MyMatrix scalarMatrix = matrix1 * 5;
        Console.WriteLine("Scalar multiplication:");
        PrintMatrix(scalarMatrix);

        // Делим матрицу на скаляр
        MyMatrix divisionMatrix = matrix1 / 2;
        Console.WriteLine("Scalar division:");
        PrintMatrix(divisionMatrix);
    }
}
```

# Задание№2
Создайте класс Car с тремя авто-свойствами: Name, ProductionYear и MaxSpeed, соответствующими названию, году выпуска и максимальной скорости соответственно.
Создайте класс CarComparer, наследуемый от IComparer<Car> и реализуйте метод Compare таким образом, чтобы можно было сортировать массив элементов Car по названию, году выпуска или максимальной скорости по выбору.
Создайте массив элементов Car и продемонстрируйте сортировку различными способами.

```
using System;
using System.Collections.Generic;

public class Car
{
    public string Name { get; set; }
    public int ProductionYear { get; set; }
    public int MaxSpeed { get; set; }
}

public class CarComparer : IComparer<Car>
{
    private readonly string _sortBy;

    public CarComparer(string sortBy)
    {
        _sortBy = sortBy;
    }

    public int Compare(Car x, Car y)
    {
        switch (_sortBy)
        {
            case "Name":
                return string.Compare(x.Name, y.Name);
            case "ProductionYear":
                return x.ProductionYear.CompareTo(y.ProductionYear);
            case "MaxSpeed":
                return x.MaxSpeed.CompareTo(y.MaxSpeed);
            default:
                throw new ArgumentOutOfRangeException(nameof(_sortBy), $"Invalid sort option: {_sortBy}");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Создание массива элементов Car
        Car[] cars = new Car[]
        {
            new Car { Name = "BMW", ProductionYear = 2021, MaxSpeed = 250 },
            new Car { Name = "Audi", ProductionYear = 2020, MaxSpeed = 240 },
            new Car { Name = "Tesla", ProductionYear = 2022, MaxSpeed = 260 },
            new Car { Name = "Mercedes", ProductionYear = 2019, MaxSpeed = 230 }
        };

        // Сортировка по названию
        Array.Sort(cars, new CarComparer("Name"));
        Console.WriteLine("Сортировка по названию:");
        PrintCars(cars);

        // Сортировка по году выпуска
        Array.Sort(cars, new CarComparer("ProductionYear"));
        Console.WriteLine("Сортировка по году выпуска:");
        PrintCars(cars);

        // Сортировка по максимальной скорости
        Array.Sort(cars, new CarComparer("MaxSpeed"));
        Console.WriteLine("Сортировка по максимальной скорости:");
        PrintCars(cars);
    }

    private static void PrintCars(Car[] cars)
    {
        foreach (var car in cars)
        {
            Console.WriteLine($"Car: {car.Name}, Year: {car.ProductionYear}, Max Speed: {car.MaxSpeed}");
        }
        Console.WriteLine();
    }
}
```

# Задание№3
Используйте класс Car из задания №2, на его основе создайте класс CarCatalor, содержащий массив элементов типа Car. 
Для класса CarCatalog реализуйте возможность итерации по элементам массива Car с помощью оператора foreach различными способами: 
Прямой проход с первого элемента до последнего.
Обратный проход от последнего к первому.
Проход по элементам массива с фильтром по году выпуска.
Проход по элементам массива с фильтром по максимальной скорости.

Примечание: для выполнения задания необходимо реализовать различные итераторы, используя конструкцию yield return. Для п.3 и 4, итератор должен принимать год выпуска и скорость как параметр, чтобы возвращать только те элементы коллекции, которые удовлетворяют условию.

```
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Car
{
    public string Name { get; set; }
    public int ProductionYear { get; set; }
    public int MaxSpeed { get; set; }
}



public class CarCatalog : IEnumerable<Car>
{
    private Car[] _cars;

    public CarCatalog(Car[] cars)
    {
        _cars = cars;
    }

    public IEnumerator<Car> GetEnumerator()
    {
        return _cars.AsEnumerable().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerable<Car> GetReverseIterator()
    {
        for (int i = _cars.Length - 1; i >= 0; i--)
        {
            yield return _cars[i];
        }
    }

    public IEnumerable<Car> GetCarsByProductionYear(int year)
    {
        foreach (Car car in _cars)
        {
            if (car.ProductionYear == year)
            {
                yield return car;
            }
        }
    }

    public IEnumerable<Car> GetCarsByMaxSpeed(int maxSpeed)
    {
        foreach (Car car in _cars)
        {
            if (car.MaxSpeed == maxSpeed)
            {
                yield return car;
            }
        }
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        Car[] cars = new Car[]
        {
            new Car { Name = "BMW", ProductionYear = 2021, MaxSpeed = 250 },
            new Car { Name = "Audi", ProductionYear = 2020, MaxSpeed = 240 },
            new Car { Name = "Tesla", ProductionYear = 2022, MaxSpeed = 260 },
            new Car { Name = "Mercedes", ProductionYear = 2019, MaxSpeed = 230 }
        };

        CarCatalog catalog = new CarCatalog(cars);

        Console.WriteLine("Прямой проход с первого элемента до последнего:");
        foreach (Car car in catalog)
        {
            Console.WriteLine(car.Name);
        }

        Console.WriteLine("Обратный проход от последнего к первому:");
        foreach (Car car in catalog.GetReverseIterator())
        {
            Console.WriteLine(car.Name);
        }

        Console.WriteLine("Проход по элементам массива с фильтром по году выпуска:");
        foreach (Car car in catalog.GetCarsByProductionYear(2022))
        {
            Console.WriteLine(car.Name);
        }

        Console.WriteLine("Проход по элементам массива с фильтром по максимальной скорости:");
        foreach (Car car in catalog.GetCarsByMaxSpeed(260))
        {
            Console.WriteLine(car.Name);
        }
    }
}
```



