using System;
using System.IO;
using System.Linq;

namespace LengthVector
{
    class Program
    {
        static void Main(string[] args)
        {
            // Чтение входных данных из файла
            string inputFile = "port.txt";
            StreamReader reader = new StreamReader(inputFile);

            // Считывание размерности пространства
            int dimension = int.Parse(reader.ReadLine());

            // Считывание матрицы метрического тензора
            double[][] metricTensor = new double[dimension][];
            for (int i = 0; i < dimension; i++)
            {
                metricTensor[i] = reader.ReadLine().Split(' ').Select(double.Parse).ToArray();
            }

            // Считывание вектора
            double[] vector = reader.ReadLine().Split(' ').Select(double.Parse).ToArray();

            reader.Close();

            // Проверка симметричности матрицы метрического тензора
            bool isSymmetric = true;
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    if (metricTensor[i][j] != metricTensor[j][i])
                    {
                        isSymmetric = false;
                        break;
                    }
                }
            }

            if (!isSymmetric)
            {
                Console.WriteLine("Матрица метрического тензора должна быть симметричной!");
                return;
            }

            // Вычисление длины вектора
            double length = Math.Sqrt(VectorDotProduct(vector, matrixMultiplication(metricTensor, vector)));

            // Вывод результата на экран
            Console.WriteLine($"Длина вектора: {length}");
        }

        /// <summary>
        /// Вычисляет скалярное произведение двух векторов
        /// </summary>
        /// <param name="v1">Первый вектор</param>
        /// <param name="v2">Второй вектор</param>
        /// <returns>Скалярное произведение</returns>
        static double VectorDotProduct(double[] v1, double[] v2)
        {
            if (v1.Length != v2.Length)
            {
                throw new ArgumentException("Векторы должны быть одинаковой размерности!");
            }

            double dotProduct = 0;
            for (int i = 0; i < v1.Length; i++)
            {
                dotProduct += v1[i] * v2[i];
            }

            return dotProduct;
        }

        /// <summary>
        /// Вычисляет произведение матрицы и вектора
        /// </summary>
        /// <param name="matrix">Матрица</param>
        /// <param name="vector">Вектор</param>
        /// <returns>Произведение матрицы и вектора</returns>
        static double[] matrixMultiplication(double[][] matrix, double[] vector)
        {
            if (matrix[0].Length != vector.Length)
            {
                throw new ArgumentException("Число столбцов матрицы должно совпадать с размерностью вектора!");
            }

            int rows = matrix.Length;
            int cols = matrix[0].Length;

            double[] result = new double[rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i] += matrix[i][j] * vector[j];
                }
            }

            return result;
        }
    }
}