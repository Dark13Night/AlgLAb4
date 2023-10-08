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
