using System;
using System.Text;

namespace AutoSalon
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            bool running = true;

            while (running)
            {
                ShowMenu();
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": ShowAbout(); Pause(); break;
                    case "2": RunValueTable(); Pause(); break;
                    case "3": ShowSalesTable(); Pause(); break;
                    case "0": Console.WriteLine("До побачення."); running = false; break;
                    default: Console.WriteLine("Немає такого пункту. Спробуйте ще раз."); Pause(); break;
                }
            }
        }
        // Пауза перед поверненням у меню та очищення екрана
        static void Pause()
        {
            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey(true);
            Console.Clear();
        }
        // Виведення головного меню
        static void ShowMenu()
        {
            Console.WriteLine("=== Автосалон ===");
            Console.WriteLine("1 — довідка");
            Console.WriteLine("2 — таблиця залишкової вартості авто");
            Console.WriteLine("3 — таблиця продажів за марками");
            Console.WriteLine("0 — вихід");
            Console.Write("Ваш вибір: ");
        }
        // Довідка: тема, сутність і поля застосунку
        static void ShowAbout()
        {
            Console.WriteLine("Тема: Автосалон");
            Console.WriteLine("Головна сутність: Автомобіль");
            Console.WriteLine("Поля:");
            Console.WriteLine($"  {"VIN",-14}— string");
            Console.WriteLine($"  {"Марка",-14}— string");
            Console.WriteLine($"  {"Код моделі",-14}— int (з довідника)");
            Console.WriteLine($"  {"Рік випуску",-14}— int");
            Console.WriteLine($"  {"Пробіг",-14}— int");
            Console.WriteLine($"  {"Колір",-14}— string");
            Console.WriteLine($"  {"Ціна",-14}— double");
            Console.WriteLine($"  {"Чи проданий",-14}— bool");
            Console.WriteLine("Підсумок: середня ціна за марками, кількість проданих авто за місяць");
        }
        // Табулювання: залишкова вартість авто залежно від віку
        static void ShowValueTable(double from, double to, double step)
        {
            double price = 1000000;   // ціна нового авто, грн
            Console.WriteLine($"Залишкова вартість авто ціною {price} грн");
            Console.WriteLine($"{"Вік (років)",12}{"Вартість (грн)",17}");

            for (double age = from; age <= to + 1e-9; age += step)
            {
                double value = CalculateValue(price, age);    // виклик методу, результат — у змінну
                Console.WriteLine($"{age,12:F2}{value,17:F2}");
            }
        }
        // Обчислює залишкову вартість авто: щороку −12 % від ціни
        static double CalculateValue(double price, double age)
        {
            return price * Math.Pow(0.88, age);
        }
        // Таблиця з двома входами: продажі авто за марками та місяцями
        static void ShowSalesTable()
        {
            string[] brands = { "BMW", "Audi", "Toyota", "Skoda", "Renault" };
            string[] months = { "Січ", "Лют", "Бер", "Кві", "Тра", "Чер" };
            // розміри таблиці питаємо в користувача
            int rows = ReadInt($"Скільки марок показати (1–{brands.Length})? ", 1, brands.Length);
            int cols = ReadInt($"Скільки місяців показати (1–{months.Length})? ", 1, months.Length);
            Random random = new Random(16);               // фіксоване число — однакові дані при кожному запуску
            int[] colSums = new int[cols];                // суми по стовпцях
            int total = 0;                                // загальний підсумок
            Console.WriteLine();
            Console.WriteLine("Кількість проданих авто за місяцями");
            // шапка
            Console.Write($"{"Марка",-10}");
            for (int col = 0; col < cols; col++)
            {                                             // стовпці: стільки місяців, скільки обрав користувач
                Console.Write($"{months[col],6}");
            }
            Console.WriteLine($"{"Разом",8}");
            // рядки таблиці
            for (int row = 0; row < rows; row++)
            {                                             // рядки: стільки марок, скільки обрав користувач
                int rowSum = 0;
                Console.Write($"{brands[row],-10}");

                for (int col = 0; col < cols; col++)
                {
                    int sold = random.Next(0, 10);
                    Console.Write($"{sold,6}");
                    rowSum += sold;
                    colSums[col] += sold;                 //  у копилку стовпця
                    total += sold;                        //  у загальну копилку
                }
                Console.WriteLine($"{rowSum,8}");
            }
            //  рядок підсумків
            Console.Write($"{"Разом",-10}");
            for (int col = 0; col < cols; col++)
            {
                Console.Write($"{colSums[col],6}");
            }
            Console.WriteLine($"{total,8}");              // правий нижній кут
        }
        // Читає ціле число від min до max, перепитує при помилці
        static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int value))
                {
                    Console.WriteLine("Помилка: введіть ціле число.");
                    continue;
                }
                if (value < min || value > max)
                {
                    Console.WriteLine($"Помилка: число має бути від {min} до {max}.");
                    continue;
                }
                return value;
            }
        }
        // Читає дробове число від min до max, перепитує при помилці
        static double ReadDouble(string prompt, double min, double max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!double.TryParse(input, out double value))
                {
                    Console.WriteLine("Помилка: введіть число (дробову частину — через кому).");
                    continue;
                }
                if (value < min || value > max)
                {
                    Console.WriteLine($"Помилка: число має бути від {min} до {max}.");
                    continue;
                }
                return value;
            }
        }
        // Питає межі та крок і будує таблицю залишкової вартості
        static void RunValueTable()
        {
            double from = ReadDouble("Вік від (років): ", 0, 30);
            double to = ReadDouble("Вік до (років): ", from, 30);      // кінець не менший за початок
            double step = ReadDouble("Крок (років, напр. 0,5): ", 0.1, 10);
            Console.WriteLine();
            ShowValueTable(from, to, step);
        }

    }




}