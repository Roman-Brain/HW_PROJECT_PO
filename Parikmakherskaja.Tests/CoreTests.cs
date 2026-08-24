using System;
using Xunit;
using Parikmakherskaja.Core;

namespace Parikmakherskaja.Tests
{
    public class CoreTests
    {
        [Fact]
        public void ClientManager_AddOrFind_DeduplicatesByPhone()
        {
            var clients = new ClientManager();

            var a = clients.AddOrFindByPhone("Иванова Анна", "+7-913-000-11-22");
            var b = clients.AddOrFindByPhone("Иванова А.С.", "+7-913-000-11-22");

            Assert.Equal(a.Id, b.Id);
            Assert.Single(clients.Clients);
        }

        [Fact]
        public void ScheduleManager_Book_ThrowsOnConflict()
        {
            var schedule = new ScheduleManager();
            var day = new DateTime(2026, 8, 24, 10, 0, 0);

            schedule.Book(clientId: 1, masterId: 1, serviceId: 1, startTime: day, durationMinutes: 40);

            Assert.Throws<InvalidOperationException>(() =>
                schedule.Book(clientId: 2, masterId: 1, serviceId: 2, startTime: day.AddMinutes(20), durationMinutes: 30));
        }

        [Fact]
        public void ScheduleManager_Book_AllowsNonOverlappingSlots()
        {
            var schedule = new ScheduleManager();
            var day = new DateTime(2026, 8, 24, 10, 0, 0);

            schedule.Book(clientId: 1, masterId: 1, serviceId: 1, startTime: day, durationMinutes: 40);
            var second = schedule.Book(clientId: 2, masterId: 1, serviceId: 2, startTime: day.AddMinutes(40), durationMinutes: 30);

            Assert.Equal(2, schedule.Bookings.Count);
            Assert.Equal(day.AddMinutes(40), second.StartTime);
        }

        [Fact]
        public void PriceListManager_Crud_Works()
        {
            var priceList = new PriceListManager();
            var service = priceList.AddService("Стрижка", 40, 800m);

            priceList.ChangePrice(service.Id, 900m);
            Assert.Equal(900m, priceList.ActiveServices[0].Price);

            priceList.Deactivate(service.Id);
            Assert.Empty(priceList.ActiveServices);
            Assert.Single(priceList.AllServices);
        }

        [Fact]
        public void DiscountCalculator_AppliesPercentage()
        {
            decimal result = DiscountCalculator.ApplyDiscount(1000m, 10);
            Assert.Equal(900m, result);
        }

        [Fact]
        public void DiscountCalculator_ThrowsOnInvalidPercent()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => DiscountCalculator.ApplyDiscount(1000m, 150));
        }

        [Fact]
        public void CashJournal_RevenueByMaster_GroupsCorrectly()
        {
            var journal = new CashJournal();
            var day = new DateTime(2026, 8, 24);

            journal.Register(clientId: 1, masterId: 1, serviceId: 1, amount: 800m, timestamp: day.AddHours(10), method: PaymentMethod.Cash);
            journal.Register(clientId: 2, masterId: 1, serviceId: 2, amount: 1200m, timestamp: day.AddHours(11), method: PaymentMethod.Card);
            journal.Register(clientId: 3, masterId: 2, serviceId: 1, amount: 800m, timestamp: day.AddHours(12), method: PaymentMethod.Cash);

            var byMaster = journal.GetRevenueByMaster(day, day);

            Assert.Equal(2000m, byMaster[1]);
            Assert.Equal(800m, byMaster[2]);
            Assert.Equal(2800m, journal.GetRevenue(day, day));
        }
    }
}
