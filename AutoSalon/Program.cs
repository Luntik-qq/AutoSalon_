using System;
using System.Text;

namespace AutoSalon
{
    internal class Program

    {
        // ===== Сховище записів «Автомобіль»: паралельні масиви =====
        const int Capacity = 100;                          // місткість сховища (не змінюється)
        static int[] ids = new int[Capacity];              // номер запису — призначає програма
        static string[] vins = new string[Capacity];       // VIN-код (17 символів)
        static string[] brands = new string[Capacity];     // марка
        static int[] modelCodes = new int[Capacity];       // код моделі з довідника (довідник — у ЛР12)
        static int[] years = new int[Capacity];            // рік випуску
        static int[] mileages = new int[Capacity];         // пробіг, км
        static string[] colors = new string[Capacity];     // колір
        static double[] prices = new double[Capacity];     // ціна, грн
        static bool[] sold = new bool[Capacity];           // чи проданий
        static int count = 0;                              // скільки записів зайнято
        static int nextId = 1;                             // номер для наступного запису

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;     // щоб правильно читалися українські літери (і, ї, є)

            bool running = true;

            while (running)
            {
                ShowMenu(); 
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": ShowAllCars(); Pause(); break;
                    case "2": AddCar(); Pause(); break;
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9": Console.WriteLine("Цей пункт з'явиться в наступних роботах."); Pause(); break;
                    case "10": SeedCars(); Pause(); break;
                    case "11": ShowAbout(); Pause(); break;
                    case "12": RunValueTable(); Pause(); break;
                    case "13": ShowSalesTable(); Pause(); break;
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
            Console.WriteLine("1 — показати всі записи");
            Console.WriteLine("2 — додати запис");
            Console.WriteLine("3 — знайти за умовою");
            Console.WriteLine("4 — змінити запис");
            Console.WriteLine("5 — видалити запис");
            Console.WriteLine("6 — зведена таблиця");
            Console.WriteLine("7 — зберегти у файл");
            Console.WriteLine("8 — завантажити з файлу");
            Console.WriteLine("9 — експорт у CSV");
            Console.WriteLine("10 — заповнити тестовими записами");
            Console.WriteLine("11 — довідка");
            Console.WriteLine("12 — таблиця залишкової вартості авто");
            Console.WriteLine("13 — таблиця продажів за марками");
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
                    total += sold;                        //  у загальну копилку.
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
        // Читає непорожній текст, перепитує при порожньому введенні
        static string ReadText(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();          // Trim прибирає пробіли по краях
                }
                Console.WriteLine("Помилка: значення не може бути порожнім.");
            }
        }
      
        // Читає відповідь «так/ні», повертає true або false
       
            
        
        // Питає межі та крок і будує таблицю залишкової вартості
        static void RunValueTable()
        {
            double from = ReadDouble("Вік від (років): ", 0, 30);
            double to = ReadDouble("Вік до (років): ", from, 30);      // кінець не менший за початок
            double step = ReadDouble("Крок (років, напр. 0,5): ", 0.1, 10);
            Console.WriteLine();
            ShowValueTable(from, to, step);
        }
        // Додає новий автомобіль: збирає поля з перевіркою, номер призначає програма
        static void AddCar()
        {
            if (count == Capacity)                         // перевірка ДО запису
            {
                Console.WriteLine("Сховище заповнене, видаліть щось зайве.");
                return;
            }
            // 1) збираємо всі поля в локальні змінні
            string vin = ReadText("VIN (17 символів): ");
            while (vin.Length != 17)
            {
                Console.WriteLine("Помилка: VIN має містити рівно 17 символів.");
                vin = ReadText("VIN (17 символів): ");
            }
            string brand = ReadText("Марка: ");
            int modelCode = ReadInt("Код моделі (1–99): ", 1, 99);
            int year = ReadInt("Рік випуску (1950–2026): ", 1950, 2026);
            int mileage = ReadInt("Пробіг, км (0–1000000): ", 0, 1000000);
            string color = ReadText("Колір: ");
            double price = ReadDouble("Ціна, грн: ", 1, 100000000);
            bool isSold = ReadYesNo("Чи проданий (так/ні): ");
            // 2) записуємо в масиви
            StoreCar(vin, brand, modelCode, year, mileage, color, price, isSold);

            Console.WriteLine($"Додано авто №{ids[count - 1]}.");   // count уже збільшили, тому -1
        }
        // Записує одне авто в усі масиви під індексом count. Єдине місце, де заповнюються масиви
        static void StoreCar(string vin, string brand, int modelCode, int year, int mileage, string color, double price, bool isSold)
        {
            ids[count] = nextId;
            vins[count] = vin.ToUpper();
            brands[count] = brand;
            modelCodes[count] = modelCode;
            years[count] = year;
            mileages[count] = mileage;
            colors[count] = color;
            prices[count] = price;
            sold[count] = isSold;

            count++;                 // тільки ПІСЛЯ присвоєнь
            nextId++;
        }
        // Виводить усі авто таблицею з вирівняними стовпцями
        static void ShowAllCars()
        {
            if (count == 0)
            {
                Console.WriteLine("Записів поки немає.");
                return;
            }
            Console.WriteLine($"{"№",4} {"VIN",-18}{"Марка",-12}{"Модель",7}{"Рік",6}{"Пробіг",9}  {"Колір",-10}{"Ціна, грн",13}{"Продано",9}");
            Console.WriteLine(new string('-', 92));
            for (int i = 0; i < count; i++)                 // до count, а не до Capacity
            {
                string mark = sold[i] ? "так" : "ні";
                Console.WriteLine($"{ids[i],4} {vins[i],-18}{brands[i],-12}{modelCodes[i],7}{years[i],6}{mileages[i],9}  {colors[i],-10}{prices[i],13:F2}{mark,9}");
            }
            Console.WriteLine(new string('-', 92));
            Console.WriteLine($"Записів: {count}.");
            ShowSummary();
        }
        // Заповнює сховище 6 готовими записами без запитань
        static void SeedCars()
        {
            if (count + 6 > Capacity)
            {
                Console.WriteLine("Недостатньо місця для тестових записів.");
                return;
            }
            StoreCar("WBA3A5C51CF256985", "BMW", 3, 2019, 85000, "чорний", 950000, true);
            StoreCar("WAUZZZ4G7EN123456", "Audi", 6, 2017, 120000, "сірий", 780000, false);
            StoreCar("JTDBR32E720045678", "Toyota", 12, 2021, 40000, "білий", 1150000, false);
            StoreCar("TMBJJ7NE8L0123456", "Skoda", 21, 2020, 65000, "синій", 690000, true);
            StoreCar("VF1RFB00X67123456", "Renault", 33, 2016, 150000, "червоний", 420000, false);
            StoreCar("WBAJA7C50KB654321", "BMW", 5, 2022, 25000, "білий", 1650000, true);

            Console.WriteLine($"Додано 6 тестових записів. Усього записів: {count}.");
        }
        // Середня ціна авто заданої марки. Якщо таких авто немає — 0 (без ділення на нуль)
        static double AveragePriceByBrand(string[] brands, double[] prices, int count, string brand)
        {
            double sum = 0;
            int n = 0;
            for (int i = 0; i < count; i++)
            {
                if (brands[i].ToLower() == brand.ToLower())   // порівнюємо без урахування регістру
                {
                    sum += prices[i];
                    n++;
                }
            }
            return n > 0 ? sum / n : 0;
        }
        // Кількість проданих авто
        static int CountSold(bool[] sold, int count)
        {
            int n = 0;
            for (int i = 0; i < count; i++)
            {
                if (sold[i]) n++;
            }
            return n;
        }
        // Виводить підсумки під таблицею: середня ціна за марками та кількість проданих
        static void ShowSummary()
        {
            Console.WriteLine("Середня ціна за марками:");
            for (int i = 0; i < count; i++)
            {
                bool seenBefore = false;                       // чи зустрічалась ця марка раніше
                for (int j = 0; j < i; j++)
                {
                    if (brands[j].ToLower() == brands[i].ToLower())
                    {
                        seenBefore = true;
                        break;                                 // break у циклі for — вихід із циклу
                    }
                }
                if (!seenBefore)
                {
                    double avg = AveragePriceByBrand(brands, prices, count, brands[i]);
                    Console.WriteLine($"  {brands[i],-12}{avg,15:F2} грн");
                }
            }
            Console.WriteLine($"Продано авто: {CountSold(sold, count)} з {count}.");
        }
        // Читає відповідь «так/ні», повертає true або false
        static bool ReadYesNo(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (input != null)
                {
                    input = input.Trim().ToLower();
                    if (input.StartsWith("т") || input.StartsWith("д") || input.StartsWith("y")) return true;    // так / да / yes
                    if (input.StartsWith("н") || input.StartsWith("n")) return false;                            // ні / нет / no
                }
                Console.WriteLine("Помилка: введіть «так» або «ні».");
            }
        }
    }
}