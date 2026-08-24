using System;
using System.Collections.Generic;
using System.Linq;

namespace Parikmakherskaja.Core
{
    /// <summary>
    /// Требование ТЗ п.2.1: "Система должна проверять, не занято ли выбранное время
    /// у мастера, и не должна допускать двойной записи".
    /// </summary>
    public class ScheduleManager
    {
        private readonly List<Booking> _bookings = new List<Booking>();
        private int _nextId = 1;

        public IReadOnlyList<Booking> Bookings => _bookings;

        public Booking Book(int clientId, int masterId, int serviceId, DateTime startTime, int durationMinutes)
        {
            var candidate = new Booking(_nextId, clientId, masterId, serviceId, startTime, durationMinutes);

            if (_bookings.Any(b => b.OverlapsWith(candidate)))
            {
                throw new InvalidOperationException(
                    $"Мастер {masterId} уже занят в интервале {startTime:HH:mm}-{candidate.EndTime:HH:mm}.");
            }

            _nextId++;
            _bookings.Add(candidate);
            return candidate;
        }

        public IEnumerable<Booking> GetScheduleForMaster(int masterId, DateTime day)
        {
            return _bookings.Where(b => b.MasterId == masterId && b.StartTime.Date == day.Date)
                             .OrderBy(b => b.StartTime);
        }
    }
}
