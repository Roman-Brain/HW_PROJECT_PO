using System.Collections.Generic;
using System.Linq;

namespace Parikmakherskaja.Core
{
    /// <summary>Требование ТЗ п.2.2: ведение прайс-листа (добавление, изменение цены, снятие с продажи).</summary>
    public class PriceListManager
    {
        private readonly List<Service> _services = new List<Service>();
        private int _nextId = 1;

        public IReadOnlyList<Service> ActiveServices => _services.Where(s => s.IsActive).ToList();
        public IReadOnlyList<Service> AllServices => _services;

        public Service AddService(string name, int durationMinutes, decimal price)
        {
            var service = new Service(_nextId++, name, durationMinutes, price);
            _services.Add(service);
            return service;
        }

        public void ChangePrice(int serviceId, decimal newPrice)
        {
            var service = _services.First(s => s.Id == serviceId);
            service.Price = newPrice;
        }

        public void Deactivate(int serviceId)
        {
            var service = _services.First(s => s.Id == serviceId);
            service.IsActive = false;
        }
    }
}
