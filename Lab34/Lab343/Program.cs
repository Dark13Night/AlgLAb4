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
