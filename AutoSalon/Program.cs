using System;
using System.Text;

namespace AutoSalon
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            const int Capacity = 100;                    // місткість сховища
            double[] prices = new double[Capacity];      // ціни авто
            int count = 0;                               // скільки цін уже записано
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
                    case "4": AddPrice(prices, ref count, ReadDouble("Ціна авто (грн): ", 1, 100000000)); Console.WriteLine($"Лічильник у Main: {count}"); Pause(); break;
                    case "5": ShowPrices(prices, count); Pause(); break;
                    case "6": ShowPrices(prices, count); RemoveAt(prices, ref count, ReadInt("Номер для видалення: ", 1, Capacity) - 1); Pause(); break;
                    case "7": ShowStatistics(prices, count); Pause(); break;
                    case "8": RunTypeExperiment(); Pause(); break;
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
            Console.WriteLine("4 — додати ціну авто");
            Console.WriteLine("5 — показати ціни");
            Console.WriteLine("6 — видалити ціну за номером");
            Console.WriteLine("7 — статистика цін");
            Console.WriteLine("8 — дослід: значимий і посилальний тип");
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
        // Додає ціну в масив. Лічильник іде через ref, щоб його зміна була видна в Main
        static void AddPrice(double[] prices, ref int count, double newPrice)
        {
            if (count == prices.Length)
            {
                Console.WriteLine("Місця більше немає.");
                return;
            }
            prices[count] = newPrice;
            count++;
            Console.WriteLine("Ціну додано.");
        }
        // Виводить ціни таблицею з номерами (до count)
        static void ShowPrices(double[] prices, int count)
        {
            if (count == 0)
            {
                Console.WriteLine("Цін ще немає.");
                return;
            }
            Console.WriteLine($"{"№",-5}{"Ціна (грн)",15}");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"{i + 1,-5}{prices[i],15:F2}");
            }
            Console.WriteLine($"Усього записів: {count}");
        }
        // Знаходить найбільшу ціну та її індекс. Для порожнього набору index = -1
        static void FindMax(double[] prices, int count, out double max, out int index)
        {
            if (count == 0)                  // порожній набір — обробляємо окремо
            {
                max = 0;
                index = -1;
                return;
            }
            max = prices[0];
            index = 0;
            for (int i = 1; i < count; i++)
            {
                if (prices[i] > max)
                {
                    max = prices[i];
                    index = i;
                }
            }
        }
        // Обчислює суму та середню ціну через out-параметри
        static void Statistics(double[] prices, int count, out double sum, out double average)
        {
            sum = 0;
            for (int i = 0; i < count; i++)
            {
                sum += prices[i];
            }
            average = count > 0 ? sum / count : 0;   // без перевірки — ділення на нуль
        }
        // Виводить статистику цін: суму, середню та найдорожче авто
        static void ShowStatistics(double[] prices, int count)
        {
            if (count == 0)
            {
                Console.WriteLine("Цін ще немає, статистику не порахувати.");
                return;
            }
            Statistics(prices, count, out double sum, out double average);
            FindMax(prices, count, out double max, out int index);
            Console.WriteLine($"Сума цін:       {sum:F2} грн");
            Console.WriteLine($"Середня ціна:   {average:F2} грн");
            Console.WriteLine($"Найдорожче авто: №{index + 1}, {max:F2} грн");
        }
        // Намагається змінити int-параметр без ref — змінює лише копію
        static void TryChangeNumber(int number)
        {
            number = number + 100;
            Console.WriteLine($"  усередині методу: number = {number}");
        }
        // Змінює перший елемент масиву — зміна видна ззовні
        static void ChangeFirstPrice(double[] prices)
        {
            prices[0] = prices[0] + 100;
            Console.WriteLine($"  усередині методу: prices[0] = {prices[0]}");
        }
        // Дослід: значимий тип (int) передається копією, масив — посиланням
        static void RunTypeExperiment()
        {
            Console.WriteLine("1) Значимий тип int, без ref:");
            int number = 50;
            Console.WriteLine($"  до виклику:  number = {number}");
            TryChangeNumber(number);
            Console.WriteLine($"  після виклику: number = {number}   ← не змінилось");
            Console.WriteLine();
            Console.WriteLine("2) Посилальний тип — масив:");
            double[] testPrices = { 50, 70, 90 };
            Console.WriteLine($"  до виклику:  prices[0] = {testPrices[0]}");
            ChangeFirstPrice(testPrices);
            Console.WriteLine($"  після виклику: prices[0] = {testPrices[0]}   ← змінилось");
        }
        // Видаляє елемент за індексом зі зсувом решти вліво; лічильник зменшується через ref
        static void RemoveAt(double[] values, ref int count, int index)
        {
            if (index < 0 || index >= count)
            {
                Console.WriteLine("Помилка: запису з таким номером немає. Нічого не змінено.");
                return;
            }
            for (int i = index; i < count - 1; i++)
            {
                values[i] = values[i + 1];      // кожен наступний стає на місце попереднього
            }
            count--;
            Console.WriteLine("Запис видалено.");
        }

    }




}