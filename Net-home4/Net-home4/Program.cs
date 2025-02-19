using System;

namespace Net_home4
{
    internal class Program
    {


        static void Main(string[] args)
        {
            // Matrix creation
            static int[,] CreateMatrix(int rows, int cols)
            {
                Random rand = new Random();
                int[,] matrix = new int[rows, cols];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        matrix[i, j] = rand.Next(1, 10); // Num generation
                    }
                }
                return matrix;
            }

            // Print
            static void PrintMatrix(int[,] matrix)
            {
                int rows = matrix.GetLength(0);
                int cols = matrix.GetLength(1);
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        Console.Write(matrix[i, j] + "\t");
                    }
                    Console.WriteLine();
                }
            }

            // Multiply matrix on number
            static int[,] MultiplyByNumber(int[,] matrix, int number)
            {
                int rows = matrix.GetLength(0);
                int cols = matrix.GetLength(1);
                int[,] result = new int[rows, cols];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        result[i, j] = matrix[i, j] * number;
                    }
                }
                return result;
            }

            // Add matrices
            static int[,] AddMatrices(int[,] matrix1, int[,] matrix2)
            {
                int rows = matrix1.GetLength(0);
                int cols = matrix1.GetLength(1);
                int[,] result = new int[rows, cols];

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        result[i, j] = matrix1[i, j] + matrix2[i, j];
                    }
                }
                return result;
            }

            // Multiply matrices
            static int[,] MultiplyMatrices(int[,] matrix1, int[,] matrix2)
            {
                int rows1 = matrix1.GetLength(0);
                int cols1 = matrix1.GetLength(1);
                int rows2 = matrix2.GetLength(0);
                int cols2 = matrix2.GetLength(1);

                if (cols1 != rows2)
                {
                    throw new Exception("Can`t multiply matrices: first cols don`t match second rows");
                }

                int[,] result = new int[rows1, cols2];

                for (int i = 0; i < rows1; i++)
                {
                    for (int j = 0; j < cols2; j++)
                    {
                        for (int k = 0; k < cols1; k++)
                        {
                            result[i, j] += matrix1[i, k] * matrix2[k, j];
                        }
                    }
                }
                return result;
            }

            Console.Write("Enter number of matrix rows: ");
            int rows = int.Parse(Console.ReadLine());
            Console.Write("Number of cols: ");
            int cols = int.Parse(Console.ReadLine());

            int[,] matrix1 = CreateMatrix(rows, cols);
            Console.WriteLine("Matrix 1:");
            PrintMatrix(matrix1);

            Console.WriteLine("\nChoose your operation:");
            Console.WriteLine("1 - Multiply on number: ");
            Console.WriteLine("2 - Add matrices");
            Console.WriteLine("3 - Multiply matrices");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                Console.Write("Enter number to multiply on: ");
                int num = int.Parse(Console.ReadLine());
                int[,] result = MultiplyByNumber(matrix1, num);
                Console.WriteLine("Multiplication result:");
                PrintMatrix(result);
            }
            else if (choice == 2)
            {
                int[,] matrix2 = CreateMatrix(rows, cols);
                Console.WriteLine("Matrix 2:");
                PrintMatrix(matrix2);

                int[,] result = AddMatrices(matrix1, matrix2);
                Console.WriteLine("Result of addition:");
                PrintMatrix(result);
            }
            else if (choice == 3)
            {
                Console.Write("Enter number of second matrix rows: ");
                int rows2 = int.Parse(Console.ReadLine());
                Console.Write("Second matrix cols: ");
                int cols2 = int.Parse(Console.ReadLine());

                int[,] matrix2 = CreateMatrix(rows2, cols2);
                Console.WriteLine("Matrix 2:");
                PrintMatrix(matrix2);

                try
                {
                    int[,] result = MultiplyMatrices(matrix1, matrix2);
                    Console.WriteLine("Multiplication result:");
                    PrintMatrix(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Wrong choice, now die.");
            }

        }
    }


}



    

