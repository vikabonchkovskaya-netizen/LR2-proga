using System;

namespace LR2
{
    class Program
    {
        // Границы прямоугольной области для Задания 2
        private const double MIN_X = 0.0;
        private const double MAX_X = 10.0;
        private const double MIN_Y = 0.0;
        private const double MAX_Y = 5.0;

        static void Main(string[] args)
        {

            bool programRunning = true;
            while (programRunning)
            {
                // Главное меню: выбор задания
                Console.WriteLine("=== МЕНЮ ===");
                Console.WriteLine("1. Задание 1: Существует ли треугольник со сторонами a, b, c?");
                Console.WriteLine("2. Задание 2: Точка (x, y) относительно прямоугольника [0;10]×[0;5]");
                Console.WriteLine("3. Выход");
                Console.Write("Выберите номер задания (1, 2 или 3): ");

                if (!TryReadMenuChoice(out int menuChoice))
                {
                    Console.WriteLine("\n Некорректный ввод. Введите 1, 2 или 3.\n");
                    continue;
                }

                switch (menuChoice)
                {
                    case 1:
                        SolveTriangleTask();
                        break;
                    case 2:
                        SolvePointTask();
                        break;
                    case 3:
                        Environment.Exit(0); // Завершить программу полностью
                        break;
                    default:
                        // Этот случай теперь невозможен благодаря TryReadMenuChoice
                        break;
                }
            }
        }

        // ——————— Чтение выбора меню ———————
        static bool TryReadMenuChoice(out int choice)
        {
            choice = 0;
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int val) && val >= 1 && val <= 3)
            {
                choice = val;
                return true;
            }
            return false;
        }

        // ——————— ЗАДАНИЕ 1: Треугольник ———————
        static void SolveTriangleTask()
        {
            Console.WriteLine("Введите длины трёх сторон:");

            double a, b, c;
            if (!TryReadPositiveDouble("a", out a) ||
                !TryReadPositiveDouble("b", out b) ||
                !TryReadPositiveDouble("c", out c))
            {
                Console.WriteLine(" Ошибка ввода (некорректные данные).\n");
                return; // вернуться в главное меню
            }

            bool exists = IsTrianglePossible(a, b, c);
            Console.WriteLine($"\nСтороны: a = {a:F2}, b = {b:F2}, c = {c:F2}");
            Console.WriteLine(exists ? " Треугольник существует." : " Треугольник НЕ существует.");
        }

        static bool TryReadPositiveDouble(string name, out double value)
        {
            value = 0;
            Console.Write($"Введите сторону {name}: ");
            string? input = Console.ReadLine();
            if (double.TryParse(input, out double v) && v > 0)
            {
                value = v;
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool IsTrianglePossible(double a, double b, double c)
        {
            return (a + b > c) && (a + c > b) && (b + c > a);
        }

        // ——————— ЗАДАНИЕ 2: Точка в прямоугольнике ———————
        static void SolvePointTask()
        {
            Console.WriteLine("Введите координаты точки:");

            double x, y;
            if (!TryReadDouble("x", out x) || !TryReadDouble("y", out y))
            {
                Console.WriteLine(" Ошибка ввода (некорректные данные).\n");
                return; // вернуться в главное меню
            }

            string result = CheckPointPosition(x, y);
            Console.WriteLine($"\nТочка ({x:F2}, {y:F2}) → {result}");
        }

        static bool TryReadDouble(string name, out double value)
        {
            value = 0;
            Console.Write($"Введите координату {name}: ");
            string? input = Console.ReadLine();
            bool success = double.TryParse(input, out value);
            if (!success)
                Console.WriteLine("Ошибка: введите число.");
            return success;
        }

        static string CheckPointPosition(double x, double y)
        {
            bool inside = x > MIN_X && x < MAX_X && y > MIN_Y && y < MAX_Y;

            bool onBoundary =
                (x == MIN_X && y >= MIN_Y && y <= MAX_Y) || // левая сторона
                (x == MAX_X && y >= MIN_Y && y <= MAX_Y) || // правая сторона
                (y == MIN_Y && x >= MIN_X && x <= MAX_X) || // нижняя сторона
                (y == MAX_Y && x >= MIN_X && x <= MAX_X);   // верхняя сторона

            return inside ? "Да" :
                   onBoundary ? "На границе" : "Нет";
        }
    }
}