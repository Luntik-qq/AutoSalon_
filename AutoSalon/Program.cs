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
                        Console.WriteLine("Поки не зроблено.");
                        Pause();
                        break;
                        case "3":
                        Console.WriteLine("Поки не зроблено.");
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


    }
}