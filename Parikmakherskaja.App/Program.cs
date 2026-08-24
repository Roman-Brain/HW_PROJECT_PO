using System;
using System.Globalization;
using Parikmakherskaja.Core;

// Демонстрационное консольное приложение "Парикмахерская: клиенты, прайс услуг,
// сотрудники, кассовый журнал" (вариант 6). Используется как рабочий код команды
// в практических заданиях №3 (разработка + анализ кода), №4 (сборка),
// №8 (автоматическое тестирование) контрольной работы по УК ПО.

var culture = CultureInfo.GetCultureInfo("ru-RU");

Console.WriteLine("================================================================");
Console.WriteLine("Парикмахерская: демонстрация основных сценариев (вариант 6)");
Console.WriteLine("================================================================\n");

var clients = new ClientManager();
var priceList = new PriceListManager();
var schedule = new ScheduleManager();
var journal = new CashJournal();

var haircut = priceList.AddService("Стрижка", 40, 800m);
var coloring = priceList.AddService("Окрашивание", 90, 2500m);
var manicure = priceList.AddService("Маникюр", 60, 1200m);
Console.WriteLine("Прайс-лист:");
foreach (var s in priceList.ActiveServices)
{
    Console.WriteLine($"  [{s.Id}] {s.Name} — {s.DurationMinutes} мин, {s.Price:0.00} руб.");
}

Console.WriteLine("\nРегистрация клиентов:");
var client1 = clients.AddOrFindByPhone("Иванова Анна Сергеевна", "+7-913-000-11-22");
var client1Again = clients.AddOrFindByPhone("Иванова А.С.", "+7-913-000-11-22");
var client2 = clients.AddOrFindByPhone("Петров Олег Викторович", "+7-913-000-33-44");
Console.WriteLine($"  client1.Id={client1.Id}, client1Again.Id={client1Again.Id} (совпадают: {client1.Id == client1Again.Id})");
Console.WriteLine($"  Всего уникальных клиентов: {clients.Clients.Count}");

int masterId = 1;
var day = new DateTime(2026, 8, 24);
Console.WriteLine("\nЗапись клиентов к мастеру:");
var booking1 = schedule.Book(client1.Id, masterId, haircut.Id, day.AddHours(10), haircut.DurationMinutes);
Console.WriteLine($"  Запись #{booking1.Id}: клиент {client1.Id} к мастеру {masterId} на {booking1.StartTime:HH:mm}-{booking1.EndTime:HH:mm}");

try
{
    schedule.Book(client2.Id, masterId, coloring.Id, day.AddHours(10).AddMinutes(20), coloring.DurationMinutes);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"  Ожидаемое исключение (двойная запись): {ex.Message}");
}

var booking2 = schedule.Book(client2.Id, masterId, manicure.Id, day.AddHours(11), manicure.DurationMinutes);
Console.WriteLine($"  Запись #{booking2.Id}: клиент {client2.Id} к мастеру {masterId} на {booking2.StartTime:HH:mm}-{booking2.EndTime:HH:mm}");

Console.WriteLine("\nОформление оплаты (касса):");
decimal amountWithDiscount = DiscountCalculator.ApplyDiscount(haircut.Price, 10);
journal.Register(client1.Id, masterId, haircut.Id, amountWithDiscount, day.AddHours(10).AddMinutes(40), PaymentMethod.Cash);
journal.Register(client2.Id, masterId, manicure.Id, manicure.Price, day.AddHours(12), PaymentMethod.Card);
Console.WriteLine($"  Чек 1: услуга '{haircut.Name}', скидка 10%, к оплате {amountWithDiscount:0.00} руб.");
Console.WriteLine($"  Чек 2: услуга '{manicure.Name}', без скидки, к оплате {manicure.Price:0.00} руб.");

Console.WriteLine("\nОтчёт по выручке за день:");
decimal totalRevenue = journal.GetRevenue(day, day);
Console.WriteLine($"  Итого за {day:dd.MM.yyyy}: {totalRevenue:0.00} руб.");
foreach (var kv in journal.GetRevenueByMaster(day, day))
{
    Console.WriteLine($"  Мастер {kv.Key}: {kv.Value:0.00} руб.");
}

Console.WriteLine("\nДемонстрация завершена.");
