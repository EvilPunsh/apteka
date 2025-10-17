using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба1Технологии
{
    internal class Program
    {
        static void BuildFibonacci(int a, int b, int m, List<int> list)
        {
            if (a > m)
                return;

            list.Add(a);
            BuildFibonacci(b, a + b, m, list);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Задание 1.1");
            Console.WriteLine("Введите число n:");
            double n = double.Parse(Console.ReadLine());

            if (n == 0 || n == 1)
                Console.WriteLine(1);
            else
            {
                if (n < 0)
                    Console.WriteLine("Факториал может быть определён только для неотрицательных чисел");
                else
                {
                    long result = 1;
                    for (int i = 2; i <= n; i++)
                    {
                        result *= i;
                    }
                    Console.WriteLine(result);

                }

            }

            Console.WriteLine("Задание 1.2");

            Console.WriteLine("Введите число");
            double x = double.Parse(Console.ReadLine());
            double A;
            A = Math.Sin(5 / x) * Math.Cosh(Math.Sqrt(x - 1)) + Math.Pow(Math.E, 5 * x);
            Console.WriteLine(A);
            Console.WriteLine("Задание 1.3");

            Console.Write("Введите число m: ");
            if (int.TryParse(Console.ReadLine(), out int m) && m >= 0)
            {
                var result = new List<int>();
                BuildFibonacci(0, 1, m, result);
                Console.WriteLine($"Числа Фибоначчи от 0 до {m}:");
                Console.WriteLine(string.Join(", ", result));
            }
            else
            {
                Console.WriteLine("Пожалуйста, введите корректное неотрицательное целое число.");
            }


        }
    }
}
