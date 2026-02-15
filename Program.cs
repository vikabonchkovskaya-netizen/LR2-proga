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
             
                if (!TryReadMenuChoice(out int menuChoice))
                {
                    Console.WriteLine("\n Некорректный ввод. Введите 1, 2 или 3.\n");
                    continue;
                }

                switch (menuChoice)
                {
                    case 1:
                        SolveTriangleTask();
                        PauseBeforeReturnToMenu(); // Пауза после выполнения задания
                        break;
                    case 2:
                        SolvePointTask();
                        PauseBeforeReturnToMenu(); // Пауза после выполнения задания
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

        // ——————— Пауза перед возвратом в меню ———————
        static void PauseBeforeReturnToMenu()
        {
           Console.ReadKey(true); // true — не выводить символ нажатой клавиши
            Console.WriteLine(); // Перевод строки после нажатия
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
          Console.WriteLine("Введите длины трёх сторон (положительные числа):");

            double a = ReadPositiveDouble("a");
            double b = ReadPositiveDouble("b");
            double c = ReadPositiveDouble("c");

            bool exists = IsTrianglePossible(a, b, c);
            Console.WriteLine($"\nСтороны: a = {a:F2}, b = {b:F2}, c = {c:F2}");
            Console.WriteLine(exists ? " Треугольник существует." : " Треугольник НЕ существует.");
        }

        // Вводит положительное число, повторяет, пока не введено корректное
        static double ReadPositiveDouble(string name)
        {
            double value;
            while (true)
            {
                Console.Write($"Введите сторону {name}: ");
                string? input = Console.ReadLine();
                if (double.TryParse(input, out value) && value > 0)
                {
                    return value;
                }
                else
                {
                    Console.WriteLine(" Ошибка: введите положительное число.");
                }
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

            double x = ReadDouble("x");
            double y = ReadDouble("y");

            string result = CheckPointPosition(x, y);
            Console.WriteLine($"\nТочка ({x:F2}, {y:F2}) → {result}");
        }

        // Вводит число, повторяет, пока не введено корректное
        static double ReadDouble(string name)
        {
            double value;
            while (true)
            {
                Console.Write($"Введите координату {name}: ");
                string? input = Console.ReadLine();
                if (double.TryParse(input, out value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine(" Ошибка: введите число.");
                }
            }
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