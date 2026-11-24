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
                Console.WriteLine("=== Автосалон ===");
                Console.WriteLine("1 — довідка");
                Console.WriteLine("2 — таблиця залишкової вартості авто");
                Console.WriteLine("3 — таблиця продажів за марками");
                Console.WriteLine("0 — вихід");
                Console.Write("Ваш вибір: ");
                string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                        ShowAbout(); 
                        Pause();
                        break;
                        case "2":
                        ShowValueTable();
                        Pause();
                        break;
                        case "3":
                        ShowSalesTable();
                        Pause();
                        break;
                        case "0":
                        Console.WriteLine("До побачення.");
                        running = false;
                        break;
                        default:
                        Console.WriteLine("Немає такого пункту. Спробуйте ще раз.");
                        Pause();
                        break;
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
        static void ShowValueTable()
        {
            double price = 1000000;   // ціна нового авто, грн
            double from = 0;          // вік від
            double to = 5;            // вік до
            double step = 0.5;        // крок

            Console.WriteLine($"Залишкова вартість авто ціною {price} грн");
            Console.WriteLine($"{"Вік (років)",12}{"Вартість (грн)",17}");

            for (double age = from; age <= to + 1e-9; age += step)
            {
                double value = price * Math.Pow(0.88, age);   // щороку −12%
                Console.WriteLine($"{age,12:F2}{value,17:F2}");
            }
        }
        // Таблиця з двома входами: продажі авто за марками та місяцями
        static void ShowSalesTable()
        {
            string[] brands = { "BMW", "Audi", "Toyota", "Skoda", "Renault" };
            string[] months = { "Січ", "Лют", "Бер", "Кві", "Тра", "Чер" };
            Random random = new Random(16);   // фіксоване число — однакові дані при кожному запуску

            Console.WriteLine("Кількість проданих авто за місяцями");

            // шапка
            Console.Write($"{"Марка",-10}");
            for (int col = 0; col < months.Length; col++)
            {
                Console.Write($"{months[col],6}");
            }
            Console.WriteLine($"{"Разом",8}");

            // рядки таблиці
            for (int row = 0; row < brands.Length; row++)
            {
                int rowSum = 0;                          // підсумок рядка, обнуляється для кожної марки
                Console.Write($"{brands[row],-10}");

                for (int col = 0; col < months.Length; col++)
                {
                    int sold = random.Next(0, 10);       // продано від 0 до 9 авто
                    Console.Write($"{sold,6}");
                    rowSum += sold;
                }

                Console.WriteLine($"{rowSum,8}");        // останній стовпець — підсумок рядка
            }
        }


    }
}