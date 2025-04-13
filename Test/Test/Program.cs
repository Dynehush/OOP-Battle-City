using System;
using System.Timers; // Бібліотека для роботи з таймером

class Program
{
    private static System.Timers.Timer _timer; // Оголошуємо змінну для таймера

    static void Main()
    {
        _timer = new System.Timers.Timer(2000); // Створюємо таймер з інтервалом 2000 мс (2 сек)
        _timer.Elapsed += ExecuteCode; // Додаємо подію, яка спрацьовуватиме кожні 2 секунди
        _timer.AutoReset = true; // Таймер перезапускається після кожного спрацьовування
        _timer.Enabled = true; // Увімкнути таймер

        Console.WriteLine("Натисніть Enter для виходу...");
        Console.ReadLine(); // Програма чекатиме введення, щоб не завершитися відразу
    }

    private static void ExecuteCode(object sender, ElapsedEventArgs e)
    {
        Console.WriteLine($"Код виконано: {DateTime.Now}");
    }
}
