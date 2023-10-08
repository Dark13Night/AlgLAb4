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
